using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoleToPermissionRepository;
using DataAccess.Abstract.ITBLPageRepository;
using Entities.Concrete.TBLPageEntity;
using Entities.Dtos.UserPermissionsDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RoleToPermissions.Queries
{
    public class GetUserToPermissionListQuery : IRequest<IDataResult<IEnumerable<GetUserToPermissionDto>>>
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public class GetUserToPermissionListQueryHandler : IRequestHandler<GetUserToPermissionListQuery, IDataResult<IEnumerable<GetUserToPermissionDto>>>
        {
            private readonly IRoleToPermissionRepository _roleToPermissionRepository;

            public GetUserToPermissionListQueryHandler(IRoleToPermissionRepository roleToPermissionRepository)
            {
                _roleToPermissionRepository = roleToPermissionRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<GetUserToPermissionDto>>> Handle(GetUserToPermissionListQuery request, CancellationToken cancellationToken)
            {
                var list = await _roleToPermissionRepository.GetUserToPermissionList(request.RoleId, request.UserId);
                return new SuccessDataResult<IEnumerable<GetUserToPermissionDto>>(list);
            }
        }
    }
}
