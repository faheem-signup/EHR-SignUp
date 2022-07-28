using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using DataAccess.Abstract.IUserToProviderAssignRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete;
using Entities.Concrete.User;
using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.UserToProviderAssignEnity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Commands
{
    public class UpdateUserAppCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MI { get; set; }
        public string UserSSN { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string PersonalEmail { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsProvider { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public DateTime? DOB { get; set; }
        public int? UserTypeId { get; set; }
        public int? AutoLockTime { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
        public int? PracticeId { get; set; }
        public string Password { get; set; }
        public List<UserWorkHour> UserWorkHourList { get; set; }
        public int[] UserToProviderAssignIds { get; set; }
        public int[] UserToLocationAssignIds { get; set; }
        public class UpdateUserAppCommandHandler : IRequestHandler<UpdateUserAppCommand, IResult>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IUserWorkHourRepository _userWorkHourRepository;
            private readonly IUserToProviderAssignRepository _userToProviderAssignRepository;
            private readonly IUserToLocationAssignRepository _userToLocationAssignRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            public UpdateUserAppCommandHandler(IUserAppRepository userAppRepository, IUserWorkHourRepository userWorkHourRepository, IUserToProviderAssignRepository userToProviderAssignRepository, IUserToLocationAssignRepository userToLocationAssignRepository, IHttpContextAccessor contextAccessor)
            {
                _userAppRepository = userAppRepository;
                _userWorkHourRepository = userWorkHourRepository;
                _userToProviderAssignRepository = userToProviderAssignRepository;
                _userToLocationAssignRepository = userToLocationAssignRepository;
                _contextAccessor = contextAccessor;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [ValidationAspect(typeof(ValidatorUpdateUserApp), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserAppCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var userAppObj = await _userAppRepository.GetAsync(x => x.UserId == request.UserId && x.IsDeleted != true);
                if (userAppObj != null)
                {
                    userAppObj.UserId = request.UserId;
                    userAppObj.FirstName = request.FirstName;
                    userAppObj.LastName = request.LastName;
                    userAppObj.MI = request.MI;
                    userAppObj.UserSSN = request.UserSSN;
                    userAppObj.CellNumber = request.CellNumber;
                    userAppObj.Address = request.Address;
                    userAppObj.PersonalEmail = request.PersonalEmail;
                    userAppObj.HourlyRate = request.HourlyRate;
                    userAppObj.IsProvider = request.IsProvider;
                    userAppObj.State = request.State;
                    userAppObj.City = request.City;
                    userAppObj.DOB = request.DOB;
                    userAppObj.UserTypeId = request.UserTypeId;
                    userAppObj.AutoLockTime = request.AutoLockTime;
                    userAppObj.RoleId = request.RoleId;
                    userAppObj.PracticeId = request.PracticeId;
                    userAppObj.StatusId = request.StatusId;
                    userAppObj.ModifiedBy = userId == null ? 1 : int.Parse(userId);
                    userAppObj.ModifiedDate = DateTime.Now;
                    //userAppObj.Password = request.Password;

                    _userAppRepository.Update(userAppObj);
                    await _userAppRepository.SaveChangesAsync();

                    if (request.UserWorkHourList != null && request.UserWorkHourList.Count() > 0)
                    {
                        request.UserWorkHourList.ToList().ForEach(x => x.UserId = userAppObj.UserId);  // Adding UserId in UserWorkHourConfig List

                        var existingList = await _userWorkHourRepository.GetListAsync(x => x.UserId == userAppObj.UserId);
                        _userWorkHourRepository.BulkInsert(existingList, request.UserWorkHourList);
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

                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
