using AutoMapper;
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using DataAccess.Abstract.IUserToProviderAssignRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete.User;
using Entities.Dtos.UesrAppDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Queries
{
    public class GetUserAppQuery : IRequest<IDataResult<UsersAppDto>>
    {
        public int UserId { get; set; }

        public class GetUserAppQueryHandler : IRequestHandler<GetUserAppQuery, IDataResult<UsersAppDto>>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IMapper _mapper;
            private readonly IUserToProviderAssignRepository _userToProviderAssignRepository;
            private readonly IUserToLocationAssignRepository _userToLocationAssignRepository;
            private readonly IUserWorkHourRepository _userWorkHourRepository;
            public GetUserAppQueryHandler(IUserAppRepository userAppRepository, 
                IMapper mapper, 
                IUserToProviderAssignRepository userToProviderAssignRepository, 
                IUserToLocationAssignRepository userToLocationAssignRepository, 
                IUserWorkHourRepository userWorkHourRepository)
            {
                _userAppRepository = userAppRepository;
                _mapper = mapper;
                _userToProviderAssignRepository = userToProviderAssignRepository;
                _userToLocationAssignRepository = userToLocationAssignRepository;
                _userWorkHourRepository = userWorkHourRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<UsersAppDto>> Handle(GetUserAppQuery request, CancellationToken cancellationToken)
            {
                var user = await _userAppRepository.GetAsync(x => x.UserId == request.UserId && x.IsDeleted != true);
                var list = _mapper.Map<UsersAppDto>(user);

                var userToLocationAssignList = await _userToLocationAssignRepository.GetListAsync(x => x.UserId == request.UserId);
                var userToProvderAssignList = await _userToProviderAssignRepository.GetListAsync(x => x.UserId == request.UserId);
                var userWorkHourList = await _userWorkHourRepository.GetListAsync(x => x.UserId == request.UserId);
                if(userToProvderAssignList != null && userToProvderAssignList.Count() > 0)
                {
                    list.UserToProvderAssignList = _mapper.Map<IEnumerable<UserToProviderAssignDto>>(userToProvderAssignList);
                }
                
                if (userToLocationAssignList != null && userToLocationAssignList.Count() > 0)
                {
                    list.UserToLocationAssignList = _mapper.Map<IEnumerable<UserToLocationAssignDto>>(userToLocationAssignList);
                }
               
                if (userWorkHourList != null && userWorkHourList.Count() > 0)
                {
                    list.UserWorkHourList = _mapper.Map<IEnumerable<UserWorkHourDto>>(userWorkHourList);
                }
                else
                {
                    list.UserWorkHourList = _mapper.Map<IEnumerable<UserWorkHourDto>>(userWorkHourList);
                }

                return new SuccessDataResult<UsersAppDto>(list);
            }
        }
    }
}
