using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete;
using Entities.Concrete.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserWorkHourConfigs.Queries
{
    public class GetUserWorkHourConfigQuery : IRequest<IDataResult<UserWorkHour>>
    {
        public int Id { get; set; }

        public class GetUserWorkHourConfigQueryHandler : IRequestHandler<GetUserWorkHourConfigQuery, IDataResult<UserWorkHour>>
        {
            private readonly IUserWorkHourRepository _userWorkHourRepository;

            public GetUserWorkHourConfigQueryHandler(IUserWorkHourRepository userWorkHourRepository)
            {
                _userWorkHourRepository = userWorkHourRepository;
            }

           // [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<UserWorkHour>> Handle(GetUserWorkHourConfigQuery request, CancellationToken cancellationToken)
            {
                var userWorkHour = await _userWorkHourRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<UserWorkHour>(userWorkHour);
            }
        }
    }
}
