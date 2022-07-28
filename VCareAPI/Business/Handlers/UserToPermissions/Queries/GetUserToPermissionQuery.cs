using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IUserToPermissionRepository;
using Entities.Concrete.UserToPermissions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserToPermissions.Queries
{
    public class GetUserToPermissionQuery : IRequest<IDataResult<UserToPermission>>
    {
        public int UserToPermissionId { get; set; }

        public class GetUserToPermissionQueryHandler : IRequestHandler<GetUserToPermissionQuery, IDataResult<UserToPermission>>
        {
            private readonly IUserToPermissionRepository _userToPermissionRepository;

            public GetUserToPermissionQueryHandler(IUserToPermissionRepository userToPermissionRepository)
            {
                _userToPermissionRepository = userToPermissionRepository;
            }

            public async Task<IDataResult<UserToPermission>> Handle(GetUserToPermissionQuery request, CancellationToken cancellationToken)
            {
                var userToPermission = await _userToPermissionRepository.GetAsync(x => x.UserToPermissionId == request.UserToPermissionId);
                return new SuccessDataResult<UserToPermission>(userToPermission);
            }
        }
    }
}
