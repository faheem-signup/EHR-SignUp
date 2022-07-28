using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Mail;
using Core.Utilities.Results;
using DataAccess.Abstract.ISendEmailRepository;
using Entities.Concrete.SendEmailEntity;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.SendEmails.Commands
{
    public class CreateSendEmailCommand : IRequest<IResult>
    {
        public string ToAddresses { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int MessageStatusId { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public int EmailStatusId { get; set; }
        public class CreateSendEmailCommandHandler : IRequestHandler<CreateSendEmailCommand, IResult>
        {
            private readonly ISendEmailRepository _sendEmailRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IMailService _mailService;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreateSendEmailCommandHandler(ISendEmailRepository sendEmailRepository,
                IMediator mediator,
                IMapper mapper,
                IMailService mailService,
                IHttpContextAccessor contextAccessor)
            {
                _sendEmailRepository = sendEmailRepository;
                _mediator = mediator;
                _mapper = mapper;
                _mailService = mailService;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateSendEmailCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                EmailMessage emailObj = new EmailMessage
                {
                    //RecieverName = request.RecieverName,
                    ToAddresses = request.ToAddresses,
                    Content = request.Message,
                    Subject = request.Subject,
                    //path = request.DocumentPath,
                };

                MailMessage mail = new MailMessage();
                System.Net.Mail.SmtpClient smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("waleedkhansignup@gmail.com");
                mail.To.Add(emailObj.ToAddresses);
                mail.Subject = emailObj.Subject;
                mail.Body = "Email send with attachment";

                //var url = new Uri(emailObj.path);
                //var fileName = Path.GetFileName(url.LocalPath);
                //var resultBytes = GetFileViaHttp(emailObj.path);
                //mail.Attachments.Add(emailObj.path);
                //mail.Attachments.Add(new Attachment(new MemoryStream(resultBytes), fileName));

                smtpServer.EnableSsl = true;
                smtpServer.UseDefaultCredentials = false;
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("waleedkhansignup@gmail.com", "waleedsignup4");
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtpServer.SendMailAsync(mail);

                //_mailService.Send(emailObj);

                SendEmail sendEmailObj = new SendEmail
                {
                    //EmailFrom = mail.From.ToString(),
                    EmailTo = request.ToAddresses,
                    EmailSubject = request.Subject,
                    EmailBody = request.Message,
                    EmailCC = request.EmailCC,
                    EmailBCC = request.EmailBCC,
                    MessageStatusId = request.MessageStatusId,
                    EmailStatusId = request.EmailStatusId,
                    //Attachments = url.LocalPath,
                    UserId = int.Parse(userId),
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _sendEmailRepository.Add(sendEmailObj);
                await _sendEmailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }

            public byte[] GetFileViaHttp(string url)
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadData(url);
                }
            }
        }
    }
}
