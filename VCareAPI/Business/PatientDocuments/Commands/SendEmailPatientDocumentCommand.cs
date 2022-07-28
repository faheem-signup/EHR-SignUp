using AutoMapper;
using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Mail;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentRepository;
using DataAccess.Abstract.ISendEmailRepository;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Concrete.Role;
using Entities.Concrete.SendEmailEntity;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocuments.Commands
{

    public class SendEmailPatientDocumentCommand : IRequest<IResult>
    {
        public string RecieverName { get; set; }
        public string Subject { get; set; }
        public string ToAddress { get; set; }
        public string Message { get; set; }
        public string DocumentPath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string DocumentData { get; set; }
        public int? EmailStatusId { get; set; }

        public class SendEmailPatientDocumentCommandHandler : IRequestHandler<SendEmailPatientDocumentCommand, IResult>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IMailService _mailService;
            private readonly ISendEmailRepository _sendEmailRepository;

            public SendEmailPatientDocumentCommandHandler(IMediator mediator,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                IMailService mailService,
                ISendEmailRepository sendEmailRepository)
            {
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _mailService = mailService;
                _sendEmailRepository = sendEmailRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(SendEmailPatientDocumentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                try
                {
                    if (request.ToAddress == string.Empty && request.DocumentPath == string.Empty)
                    {
                        return new ErrorResult("Please enter required data");
                    }

                    EmailMessage emailObj = new EmailMessage
                    {
                        RecieverName = request.RecieverName,
                        ToAddresses = request.ToAddress,
                        Content = request.Message,
                        Subject = request.Subject,
                        path = request.DocumentPath,
                    };

                    MimeMessage emailMessage = new MimeMessage();
                    MailboxAddress emailFrom = new MailboxAddress("Vcare", "info@ehr-testingdev.signupdemo.com");
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(emailObj.RecieverName, emailObj.ToAddresses);
                    emailMessage.To.Add(emailTo);
                    emailMessage.Subject = emailObj.Subject;
                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    
                    //byte[] bytes1 = Convert.FromBase64String(request.DocumentData);
                  //  var bytes=  Encoding.ASCII.GetBytes(request.DocumentData);

                    //var documentPath = GetFileViaHttp("D:\\Inetpub\\httpdocs\\UploadedFiles\\PatientDocuments\\patientnameMargala-637922134210851290.jpg");

                    byte[] bytes = Convert.FromBase64String(request.DocumentData);
                    //byte[] bytes1 = Convert.ToBase64String(request.DocumentData);
                    var fileName1 = request.FileName+"." + request.FileType;

                    emailBodyBuilder.Attachments.Add(fileName1, bytes, new ContentType("application", request.FileType));


                    //var directoryDocPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Uploads/" + item;
                    //emailBodyBuilder.Attachments.Add(directoryDocPath);

                    //if (emailDocumentList.Count() > 0)
                    //{
                    //    foreach (var item in emailDocumentList)
                    //    {
                    //        // builder.TextBody=

                    //        var directoryDocPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Uploads/" + item;
                    //        builder.Attachments.Add(directoryDocPath);
                    //    }
                    //}

                    SendEmail sendEmailObj = new SendEmail
                    {
                        EmailFrom = emailObj.ToAddresses.ToString(),
                        EmailTo = emailObj.ToAddresses,
                        EmailSubject = emailObj.Subject,
                        EmailBody = emailObj.Content,
                        EmailStatusId = (int)request.EmailStatusId,
                        Attachments = request.DocumentPath,
                        UserId = int.Parse(userId)
                    };

                    _sendEmailRepository.Add(sendEmailObj);
                    await _sendEmailRepository.SaveChangesAsync();

                    //  mail.Attachments.Add(new Attachment(new MemoryStream(resultBytes), fileName));


                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    SmtpClient emailClient = new SmtpClient();
                    emailClient.Connect("smtp.hostinger.com", 465, true);
                    emailClient.Authenticate("info@ehr-testingdev.signupdemo.com", "Azuredev@001");
                    emailClient.Send(emailMessage);
                    emailClient.Disconnect(true);
                    emailClient.Dispose();


                    return new SuccessResult(Messages.Added);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

