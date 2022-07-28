namespace Business.Handlers.Appointments.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.BusinessAspects;
    using Business.Constants;
    using Business.Helpers;
    using Business.Helpers.Validators;
    using Core.Aspects.Autofac.Logging;
    using Core.Aspects.Autofac.Transaction;
    using Core.Aspects.Autofac.Validation;
    using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
    using Core.Utilities.Results;
    using DataAccess.Abstract.IAppointmentRepository;
    using DataAccess.Abstract.IAppointmentsCheckInRepository;
    using DataAccess.Abstract.IFollowUpAppointmentRepository;
    using DataAccess.Abstract.IGroupPatientAppointmentRepository;
    using DataAccess.Abstract.IProviderWorkConfigRepository;
    using DataAccess.Abstract.IRecurringAppointmentsRepository;
    using DataAccess.Abstract.IRolesRepository;
    using DataAccess.Abstract.ISchedulerRepository;
    using DataAccess.Abstract.IUserAppRepository;
    using Entities.Concrete.AppointmentEntity;
    using Entities.Concrete.AppointmentsCheckInEntity;
    using Entities.Concrete.FollowUpAppointmentEntity;
    using Entities.Concrete.GroupPatientAppointmentEntity;
    using Entities.Concrete.RecurringAppointmentsEntity;
    using Entities.Dtos.SendEmailDto;
    using MailKit.Net.Smtp;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using MimeKit;

    public class SendEmailToSchedulerUsersCommand : IRequest<IResult>
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        //[TransactionScopeAspectAsync]
        public class SendEmailToSchedulerUsersCommandHandler : IRequestHandler<SendEmailToSchedulerUsersCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IUserAppRepository _userAppRepository;
            private readonly IRolesRepository _rolesRepository;

            public SendEmailToSchedulerUsersCommandHandler(IAppointmentRepository appointmentRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor,
                ISchedulerRepository schedulerRepository, IUserAppRepository userAppRepository, IRolesRepository rolesRepository)
            {
                _appointmentRepository = appointmentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _schedulerRepository = schedulerRepository;
                _userAppRepository = userAppRepository;
                _rolesRepository = rolesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(SendEmailToSchedulerUsersCommand request, CancellationToken cancellationToken)
            {
                var loginUserId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var roleData = await _schedulerRepository.GetRoleByUserId(Convert.ToInt32(loginUserId));

                //if (roleData.RoleName == Enum.GetName(typeof(Enums.Roles), 4))
                //{
                //    var userData = await _userAppRepository.GetAsync(x => x.UserId == Convert.ToInt32(loginUserId));

                //}

                var userData = await _userAppRepository.GetAsync(x => x.UserId == Convert.ToInt32(loginUserId));
                if (userData != null)
                {
                    var userList = await _userAppRepository.GetListAsync(x => x.PracticeId == userData.PracticeId && x.IsDeleted != true);
                    var ProviderId = 0;
                    var list = await _schedulerRepository.GetProviderStatusSummary(ProviderId, request.FromDate, request.ToDate);

                    var html = "";
                    foreach (var obj in list)
                    {
                        html += "<tr style='border: 2px solid;'><td style = 'border: 2px solid;'>" + obj.ProviderName + "</td><td style = 'border: 2px solid;'>" + obj.TotalProvierAppointment + "</td> <td style = 'border: 2px solid;'>" + obj.TotalProvierCancelledAppointment + "</td> <td style = 'border: 2px solid;'>" + obj.TotalProvierCompletedAppointment + "</td><td style = 'border: 2px solid;' >" + obj.TotalProvierScheduledAppointment + "</td ><td style = 'border: 2px solid;' >" + obj.TotalProvierCheckedInAppointment + "</td ><td style = 'border: 2px solid;' > 0</td><td style = 'border: 2px solid;' >" + obj.TotalProvierRescheduledAppointment + "</td> </tr> ";
                    }

                    foreach (var item in userList)
                    {
                        if (item.PersonalEmail != null && item.PersonalEmail != string.Empty && item.IsProvider == false && item.UserId != Convert.ToInt32(loginUserId))
                        {
                            var role = await _rolesRepository.GetAsync(p => p.RoleId == item.RoleId);
                            if (role != null && role.RoleName == "Scheduler")
                            {
                                EmailData emailObj = new EmailData
                                {
                                    EmailBody = "Appointment Detail",
                                    EmailSubject = "Appointment Detail",
                                    EmailToName = item.FirstName + " " + item.LastName,
                                    EmailToId = item.PersonalEmail,
                                };
                                SendEmail(emailObj, html);
                            }

                        }

                    }


                    foreach (var data in list)
                    {
                        html = "";
                        html += "<tr style='border: 2px solid;'><td style = 'border: 2px solid;'>" + data.ProviderName + "</td><td style = 'border: 2px solid;'>" + data.TotalProvierAppointment + "</td> <td style = 'border: 2px solid;'>" + data.TotalProvierCancelledAppointment + "</td> <td style = 'border: 2px solid;'>" + data.TotalProvierCompletedAppointment + "</td><td style = 'border: 2px solid;' >" + data.TotalProvierScheduledAppointment + "</td ><td style = 'border: 2px solid;' >" + data.TotalProvierCheckedInAppointment + "</td ><td style = 'border: 2px solid;' > 0</td><td style = 'border: 2px solid;' >" + data.TotalProvierRescheduledAppointment + "</td> </tr> ";

                        EmailData emailObj = new EmailData
                        {
                            EmailBody = "Appointment Detail",
                            EmailSubject = "Appointment Detail",
                            EmailToName = data.ProviderName,
                            EmailToId = data.ProviderEmail,
                        };
                        SendEmail(emailObj, html);
                    }

                }


                return new SuccessResult(Messages.Added);
            }

            public bool SendEmail(EmailData emailData, string html)
            {
                try
                {
                    MimeMessage emailMessage = new MimeMessage();
                    MailboxAddress emailFrom = new MailboxAddress("Vcare", "info@ehr-testingdev.signupdemo.com");
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                    emailMessage.To.Add(emailTo);
                    emailMessage.Subject = emailData.EmailSubject;
                    BodyBuilder emailBodyBuilder = new BodyBuilder();


                    var htmlData = @"<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
</head>
<body>
    <table cellpadding='0' cellspacing='0' width='650'
        style='background-color:#f5f5f5;margin:0 auto;color:#666465;min-width:650px'>
        <tbody>
            <tr style='background-color:#00a8da; height:61px'>

                <td style='vertical-align:middle'>

                    <table width='100%' cellpadding='0' cellspacing='0' height='60'>
                        <tbody>
                            <tr>
                                <td style='width:5%'></td>
                                <td style='color:#fff;width:72%;font-size:14px;letter-spacing:1px'><b
                                        style='font-size: 17px;'> Appointment Details</b></td>
                                <td align='right'>
                                    <a style='text-decoration:none;color:#ffffff;font-size:13px'><span
                                            style='color:#ffffff'> ##Date </span></a>
                                </td>
                                <td style='width:5%'></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td style='padding-top:20px'>
                    <table style='padding-left:25px;width:100%;'>
                        <tbody>
                            <tr style='line-height:0px;'>
                                <td>
                                    <img style='width:30%; background-color: #00a8da;' src='http://ehr-testing.signupdemo.com/logo/logo1.png'>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr style='line-height:0px;'>
                <td>
                    <table style='padding-left:25px; padding-right: 30px;'>
                        <tbody>
                            <tr style='line-height:0px'>
                                <td style='font-size:14px;color:#666465;line-height:22px'><br /><b> Dear
                                        ##UserFirstName,</b><br /><br />
                                    
<h3>Provider Appointments Summary</h3>
                                        <table style='border: 2px solid; border-collapse: collapse'>
                                            <tr style='border: 2px solid; border-collapse: collapse'>
                                                <th style='border: 2px solid; border-collapse: collapse'>Provider Name</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Total</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Cancelled</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Completed</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Scheduled</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Checked In</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Checked Out</th>
                                                <th style='border: 2px solid; border-collapse: collapse'>Rescheduled</th>
                                            </tr>
                                            <tbody>"
                                                        + html +
                                            @"</tbody>
                                        </table>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr style='background-color:#00a8da'>
                <td style='padding:10px 0px 10px 27px'>
                    <table>
                        <tbody>
                            <tr style='line-height:0px'>
                                <td>
                            <tr>
                                <td width='22%'
                                    style='color: #fff;line-height:22px;vertical-align:middle;font-family:Open Sans,sans-serif'>
                                    Let's Connect!</td>
                                    <td></td>
                                <td> </td>
                                <td width ='66%'></td>
                                <td><a href='http://twitter.com' target='_blank' alt='Twitter' class='CToWUd'><img style='width:26px ;' src='http://ehr-testingdev.signupdemo.com/_nuxt/img/twitter.8b2d16d.png'></a> &nbsp; <a href='https://www.facebook.com' target='_blank' alt='Facebook' class='CToWUd'><img style='width:28px ;' src='http://ehr-testingdev.signupdemo.com/_nuxt/img/facebook.b54903f.png'></a></td>
                            </tr>
                </td>
            </tr>
        </tbody>
    </table>
    </td>
    </tr>
    </tbody>
    </table>
</body>
</html>";
                    var userFirstName = @"##UserFirstName";
                    var dateTime = DateTime.Now.ToString("dd MMM , yyyy");
                    var emailHtmlData2 = htmlData.Replace(userFirstName.ToString(), emailData.EmailToName).Replace(@"##Date", dateTime);

                    //str.Close();

                    emailBodyBuilder.HtmlBody = emailHtmlData2;
                    // emailBodyBuilder.TextBody = MailText;// emailData.EmailBody;
                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    SmtpClient emailClient = new SmtpClient();
                    emailClient.Connect("smtp.hostinger.com", 465, true);
                    emailClient.Authenticate("info@ehr-testingdev.signupdemo.com", "Azuredev@001");
                    emailClient.Send(emailMessage);
                    emailClient.Disconnect(true);
                    emailClient.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    //Log Exception Details
                    return false;
                }
            }
        }
    }
}