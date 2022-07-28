using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPermissionRepository;
using Entities.Concrete.Permission;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Permission.Queries
{
    public class GetPermissionListQuery : BasePaginationQuery<IDataResult<IEnumerable<Permissions>>>
    {

        public class GetPermissionListQueryHandler : IRequestHandler<GetPermissionListQuery, IDataResult<IEnumerable<Permissions>>>
        {
            private readonly IPermissionRepository _permissionRepository;

            public GetPermissionListQueryHandler(IPermissionRepository permissionRepository)
            {
                _permissionRepository = permissionRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Permissions>>> Handle(GetPermissionListQuery request, CancellationToken cancellationToken)
            {
                var permissionlist = await _permissionRepository.GetListAsync();
                if (permissionlist.Count() > 0)
                {
                    permissionlist = permissionlist.OrderByDescending(x => x.PermissionId).ToList();
                }

                var pagedData = Paginate(permissionlist, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Permissions>>(pagedData, permissionlist.Count(), request.PageNumber);
            }
        }
    }
}
