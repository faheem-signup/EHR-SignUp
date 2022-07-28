using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.IProviderRepository;
using DataAccess.Abstract.ISendEmailRepository;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using DataAccess.Abstract.IUserToProviderAssignRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete;
using Entities.Concrete.Location;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.User;
using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.UserToProviderAssignEnity;
using Entities.Dtos.SendEmailDto;
using Entities.Dtos.UesrAppDto;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Commands
{

    public class CreateUserAppCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MI { get; set; }
        public string UserSSN { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string PersonalEmail { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? IsProvider { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public DateTime? DOB { get; set; }
        public int? UserTypeId { get; set; }
        public int? AutoLockTime { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
        public int? PracticeId { get; set; }
        public string Password { get; set; }
        public List<UserWorkHourDto> UserWorkHourList { get; set; }
        public int[] UserToProviderAssignIds { get; set; }
        public int[] UserToLocationAssignIds { get; set; }

        public class CreateUserAppCommandHandler : IRequestHandler<CreateUserAppCommand, IResult>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IUserWorkHourRepository _userWorkHourRepository;
            private readonly IUserToProviderAssignRepository _userToProviderAssignRepository;
            private readonly IUserToLocationAssignRepository _userToLocationAssignRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IProviderRepository _providerRepository;
            private readonly ISendEmailRepository _sendEmailRepository;

            public CreateUserAppCommandHandler(IUserAppRepository userAppRepository,
                IMediator mediator,
                IMapper mapper,
                IUserWorkHourRepository userWorkHourRepository,
                IUserToProviderAssignRepository userToProviderAssignRepository,
                IUserToLocationAssignRepository userToLocationAssignRepository,
                IProviderRepository providerRepository,
                ISendEmailRepository sendEmailRepository,
                IHttpContextAccessor contextAccessor)
            {
                _userAppRepository = userAppRepository;
                _mediator = mediator;
                _mapper = mapper;
                _userWorkHourRepository = userWorkHourRepository;
                _userToProviderAssignRepository = userToProviderAssignRepository;
                _userToLocationAssignRepository = userToLocationAssignRepository;
                _contextAccessor = contextAccessor;
                _providerRepository = providerRepository;
                _sendEmailRepository = sendEmailRepository;
            }

            [ValidationAspect(typeof(ValidatorUserApp), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateUserAppCommand request, CancellationToken cancellationToken)
            {
                var userEmail = await _userAppRepository.GetAsync(x => x.PersonalEmail == request.PersonalEmail && x.IsDeleted != true);
                if (userEmail != null)
                {
                    return new ErrorResult(Messages.EmailAlreadyExist);
                }

                var loginUserId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                loginUserId = string.IsNullOrEmpty(loginUserId) ? "1" : loginUserId;
                int userId = int.Parse(loginUserId);
                HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
                UserApp userAppObj = new UserApp
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MI = request.MI,
                    UserSSN = request.UserSSN,
                    CellNumber = request.CellNumber,
                    Address = request.Address,
                    PersonalEmail = request.PersonalEmail,
                    HourlyRate = request.HourlyRate,
                    IsProvider = request.IsProvider,
                    State = request.State,
                    City = request.City,
                    DOB = request.DOB,
                    UserTypeId = request.UserTypeId,
                    AutoLockTime = request.AutoLockTime,
                    RoleId = request.RoleId,
                    PracticeId = request.PracticeId,
                    StatusId = request.StatusId,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _userAppRepository.Add(userAppObj);
                await _userAppRepository.SaveChangesAsync();

                if (request.IsProvider == true)
                {
                    Provider providerObj = new Provider
                    {
                        FirstName = request.FirstName,
                        MI = request.MI,
                        LastName = request.LastName,
                        Address = request.Address,
                        City = request.City,
                        State = request.State,
                        ProviderEmail = request.PersonalEmail,
                        SSN = request.UserSSN,
                        DOB = request.DOB,
                        CellNumber = request.CellNumber,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = userId,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        UserId = userAppObj.UserId,
                    };

                    _providerRepository.Add(providerObj);
                    await _userAppRepository.SaveChangesAsync();
                }

                if (request.UserWorkHourList != null && request.UserWorkHourList.Count() > 0)
                {
                    List<UserWorkHour> _userWorkHourList = request.UserWorkHourList.ConvertAll(a =>
                    {
                        return new UserWorkHour()
                        {
                            LocationId = a.LocationId,
                            Days = a.Days,
                            FirstShiftWorkFrom = a.FirstShiftWorkFrom,
                            FirstShiftWorkTo = a.FirstShiftWorkTo,
                            SecondShiftWorkFrom = a.SecondShiftWorkFrom,
                            SecondShiftWorkTo = a.SecondShiftWorkTo,
                            SlotSize = a.SlotSize,
                            IsBreak = a.IsBreak,
                        };
                    });

                    _userWorkHourList.ToList().ForEach(x => x.UserId = userAppObj.UserId);
                    var existingList = await _userWorkHourRepository.GetListAsync(x => x.UserId == userAppObj.UserId);
                    _userWorkHourRepository.BulkInsert(existingList, _userWorkHourList);
                    await _userWorkHourRepository.SaveChangesAsync();
                }

                if (request.UserToProviderAssignIds != null && request.UserToProviderAssignIds.Length > 0)
                {
                    var userToProviderAssignList = request.UserToProviderAssignIds.Select(x => new UserToProviderAssign()
                    {
                        UserId = userAppObj.UserId,
                        ProviderId = x,
                    });

                    var existingUserToProviderList = await _userToProviderAssignRepository.GetListAsync(x => x.UserId == userAppObj.UserId);
                    _userToProviderAssignRepository.BulkInsert(existingUserToProviderList, userToProviderAssignList);
                    await _userToProviderAssignRepository.SaveChangesAsync();
                }

                if (request.UserToLocationAssignIds != null && request.UserToLocationAssignIds.Length > 0)
                {
                    var userToLocationAssignList = request.UserToLocationAssignIds.Select(x => new UserToLocationAssign()
                    {
                        UserId = userAppObj.UserId,
                        LocationId = x,
                    });

                    var existingUserToLocationList = await _userToLocationAssignRepository.GetListAsync(x => x.UserId == userAppObj.UserId);
                    _userToLocationAssignRepository.BulkInsert(existingUserToLocationList, userToLocationAssignList);
                    await _userToLocationAssignRepository.SaveChangesAsync();
                }

                EmailData emailData = new EmailData
                {
                    EmailBody = "Your account has been activated with this email " + request.PersonalEmail + " . " + " Your system generated password:" + request.Password,
                    EmailSubject = "Account Activation Detail",
                    EmailToName = request.FirstName + " " + request.LastName,
                    EmailToId = request.PersonalEmail,
                    EmailToPassword = request.Password,
                };

                SendEmail(emailData);

                return new SuccessResult(Messages.Added);
            }

            public bool SendEmail(EmailData emailData)
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
    <meta name='viewport' content='width=device-width, initial-scale=1'>
</head>
<body>
    <table cellpadding='0' cellspacing='0' width='650'
        style='background - color:#f5f5f5;margin:0 auto;color:#666465;min-width:650px'>
        <tbody>
            <tr style='background-color:#00a8da; height:61px'>

                <td style='vertical-align:middle'>

                    <table width='100%' cellpadding='0' cellspacing='0' height='60'>
                        <tbody>
                            <tr>
                                <td style='width:5%'></td>
                                <td style='color:#fff;width:72%;font-size:14px;letter-spacing:1px'><b
                                        style='font-size: 17px;'> New Account Details</b></td>
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
                                    <img style='width:30%; background-color: #00a8da;'
                                        src='http://ehr-testingdev.signupdemo.com/Logo/logo1.png'>
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
                                    Congratulations on opening your account successfully at VCare(EHR).Your account is
                                    now active and you may login from our home page.<br>
                                    <br>Within the members area you can submit and review your listings, view detailed
                                    statistics, edit your account details and much more. <br>
                                    <br> <b> Login Details:</b><br><b>Email:</b>##userName<br>
                                    <b> Password:</ b> ##userPassword<br>
                                        <a href='http://ehr-testingdev.signupdemo.com/' target='_blank'
                                            data-saferedirecturl='http://ehr-testingdev.signupdemo.com/'>
                                            http://ehr-testingdev.signupdemo.com/</a><br>
                                        Should you require any help related to your account then do not hesitate to
                                        contact us.<br>
                                        <br>Regards<br><br>Support Team<br>
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
                                <td> <a href='http://twitter.com' target='_blank' alt='Twitter' class='CToWUd'><img style='width:26px;' src='http://ehr-testingdev.signupdemo.com/_nuxt/img/twitter.8b2d16d.png'></a> &nbsp; <a href='https://www.facebook.com' target='_blank' alt='Facebook' class='CToWUd'><img style='width:28px ;' src='http://ehr-testingdev.signupdemo.com/_nuxt/img/facebook.b54903f.png'></a></td>
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
                    var email = @"##userName";
                    var pass = @"##userPassword";
                    var userFirstName = @"##UserFirstName";
                    var dateTime = DateTime.Now.ToString("dd MMM , yyyy");
                    var emailHtmlData = htmlData.Replace(email.ToString(), emailData.EmailToId);
                    var emailHtmlData1 = emailHtmlData.Replace(pass.ToString(), emailData.EmailToPassword);
                    var emailHtmlData2 = emailHtmlData1.Replace(userFirstName.ToString(), emailData.EmailToName).Replace(@"##Date", dateTime);

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

            // Generate a random string with a given size and case.
            // If second parameter is true, the return string is lowercase.
            public string RandomString(int size, bool lowerCase)
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                    builder.Append(ch);
                }

                if (lowerCase)
                {
                    return builder.ToString().ToLower();
                }

                return builder.ToString();
            }
        }
    }
}