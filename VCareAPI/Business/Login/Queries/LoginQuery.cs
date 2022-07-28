using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Jwt.UserAppToken;
using DataAccess.Abstract;
using DataAccess.Abstract.IUserAppRepository;
using Entities.Dtos.LoginAccessTokenDto;
using Entities.Dtos.UesrAppDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Login.Queries
{
    public class LoginQuery : IRequest<IDataResult<UserAppCore>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, IDataResult<UserAppCore>>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IUserAppTokenHelper _tokenHelper;
            public LoginQueryHandler(IUserAppRepository userAppRepository, IMediator mediator, IMapper mapper, IUserAppTokenHelper tokenHelper)
            {
                _userAppRepository = userAppRepository;
                _mediator = mediator;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<UserAppCore>> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userAppRepository.GetUserQuery(request.Email);
                if (user == null)
                {
                    return new ErrorDataResult<UserAppCore>("User not found");
                }

                if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordSalt, user.PasswordHash))
                {
                    return new ErrorDataResult<UserAppCore>("Invalid username/Password.Please Try again.");
                }

                var data = await _userAppRepository.GetUserAppPermission(user.UserId);
                if (data == null)
                {
                    data.UserId = user.UserId;
                    data.FirstName = user.FirstName;
                    data.LastName = user.LastName;
                    data.RoleId = user.RoleId;
                }

                if (data.UserId == 0)
                {
                    data.UserId = user.UserId;
                    data.FirstName = user.FirstName;
                    data.LastName = user.LastName;
                    data.RoleId = user.RoleId;
                }

                var datatoken=_tokenHelper.CreateUserAppToken<UserAppCore>(new UserAppCore { UserId = data.UserId, UserName = data.FirstName + ' ' + data.LastName });

                datatoken.UserId = data.UserId;
                datatoken.UserName = data.FirstName + ' ' + data.LastName;
                datatoken.ResponseData = data;
                return new SuccessDataResult<UserAppCore>(datatoken, Messages.SuccessfulLogin);
            }
        }
    }
}
