using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPermissionRepository;
using Entities.Concrete.Permission;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Permission.Queries
{
    public class GetPermissionQuery : IRequest<IDataResult<Permissions>>
    {
        public int PermissionId { get; set; }

        public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery , IDataResult<Permissions>>
        {
            private readonly IPermissionRepository _permissionRepository;

            public GetPermissionQueryHandler(IPermissionRepository permissionRepository)
            {
                _permissionRepository = permissionRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Permissions>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
            {
                var permission = await _permissionRepository.GetAsync(p => p.PermissionId == request.PermissionId);
                return new SuccessDataResult<Permissions>(permission);
            }
        }
    }
}
