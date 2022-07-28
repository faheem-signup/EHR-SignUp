using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using Entities.Dtos.UesrAppDto;
using MediatR;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Queries
{
    public class GetUserAppListQuery : BasePaginationQuery<IDataResult<IEnumerable<GetAllUserAppDto>>>
    {
        public int? PracticeId { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
        public int? LocationId { get; set; }

        public class GetUserAppListQueryHandler : IRequestHandler<GetUserAppListQuery, IDataResult<IEnumerable<GetAllUserAppDto>>>
        {
            private readonly IUserAppRepository _userAppRepository;
            private readonly IUserToLocationAssignRepository _userToLocationAssignRepository;

            public GetUserAppListQueryHandler(IUserAppRepository userAppRepository, IUserToLocationAssignRepository userToLocationAssignRepository)
            {
                _userAppRepository = userAppRepository;
                _userToLocationAssignRepository = userToLocationAssignRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<GetAllUserAppDto>>> Handle(GetUserAppListQuery request, CancellationToken cancellationToken)
            {
                var list = await _userAppRepository.GetUserAppBySearchParams(request.Name, request.UserId, request.RoleId, request.StatusId, request.LocationId, request.PracticeId);

                List<GetAllUserAppDto> allUserApplist = new List<GetAllUserAppDto>();

                var dataList = list.GroupBy(x => x.UserId);

                dataList.ToList().ForEach(x =>
                {
                    GetAllUserAppDto obj = new GetAllUserAppDto();
                    obj = x.FirstOrDefault();
                    obj.LocationName = string.Join(", ", x.Select(a => a.LocationName));
                    allUserApplist.Add(obj);
                });

                if (allUserApplist.Count() > 0)
                {
                    allUserApplist = allUserApplist.OrderByDescending(x => x.UserId).ToList();
                }

                var pagedData = Paginate(allUserApplist, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<GetAllUserAppDto>>(pagedData, allUserApplist.Count(), request.PageNumber);
            }
        }
    }
}
