using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserWorkHourConfigs.Queries
{
    public class GetUserWorkHourConfigListQuery : BasePaginationQuery<IDataResult<IEnumerable<UserWorkHour>>>
    {
        public class GetUserWorkHourConfigListQueryHandler : IRequestHandler<GetUserWorkHourConfigListQuery, IDataResult<IEnumerable<UserWorkHour>>>
        {
            private readonly IUserWorkHourRepository _userWorkHourRepository;
            public GetUserWorkHourConfigListQueryHandler(IUserWorkHourRepository userWorkHourRepository)
            {
                _userWorkHourRepository = userWorkHourRepository;
            }

           // [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
           // [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<UserWorkHour>>> Handle(GetUserWorkHourConfigListQuery request, CancellationToken cancellationToken)
            {
                var userWorkHourList = await _userWorkHourRepository.GetListAsync();
                var pagedData = Paginate(userWorkHourList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<UserWorkHour>>(pagedData, userWorkHourList.Count(), request.PageNumber);
            }
        }
    }
}
