using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt.UserAppToken;
using DataAccess.Abstract.IUserAppRepository;
using Entities.Dtos.AuthDto;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Queries
{
    public class GetUserUpdatedDataQuery : IRequest<IDataResult<UserAppCore>>
    {
        public string AccessToken { get; set; }
        public int UserId { get; set; }
        public class GetUserUpdatedDataQueryHandler : IRequestHandler<GetUserUpdatedDataQuery, IDataResult<UserAppCore>>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IUserAppTokenHelper _tokenHelper;
            private readonly IHttpContextAccessor _contextAccessor;
            public GetUserUpdatedDataQueryHandler(IUserAppRepository userAppRepository, IMediator mediator, IMapper mapper, IUserAppTokenHelper tokenHelper, IHttpContextAccessor contextAccessor)
            {
                _userAppRepository = userAppRepository;
                _mediator = mediator;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _contextAccessor = contextAccessor;
            }

            // [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<UserAppCore>> Handle(GetUserUpdatedDataQuery request, CancellationToken cancellationToken)
            {

                var accessToken = await _contextAccessor.HttpContext.GetTokenAsync("access_token");// HttpContext.GetTokenAsync("access_token");
                var userID = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                if(request.UserId == int.Parse(userID) && request.AccessToken == accessToken)
                {
                    var user = await _userAppRepository.GetUserByIdQuery(request.UserId);
                    if (user == null)
                    {
                        return new ErrorDataResult<UserAppCore>(Messages.UserNotFound);
                    }

                    var data = await _userAppRepository.GetUserAppPermission(user.UserId);

                    //   var datatoken = _tokenHelper.CreateUserAppToken<UserAppCore>(new UserAppCore { UserId = data.UserId, UserName = data.FirstName + ' ' + data.LastName });

                    UserAppCore datatoken = new UserAppCore();
                    datatoken.Token = accessToken;
                    datatoken.UserId = data.UserId;
                    datatoken.UserName = data.FirstName + ' ' + data.LastName;
                    datatoken.ResponseData = data;
                    return new SuccessDataResult<UserAppCore>(datatoken, Messages.SuccessfulLogin);
                }
                else
                {
                    return new ErrorDataResult<UserAppCore>("Invalid User");
                }

                
            }
        }
    }
}
