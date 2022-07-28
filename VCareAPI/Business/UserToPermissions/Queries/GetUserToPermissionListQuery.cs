using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserToPermissionRepository;
using Entities.Concrete.UserToPermissions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserToPermissions.Queries
{
    public class GetUserToPermissionListQuery : IRequest<IDataResult<IEnumerable<UserToPermission>>>
    {
        public int UserId { get; set; }

        public class GetUserToPermissionListQueryHandler : IRequestHandler<GetUserToPermissionListQuery, IDataResult<IEnumerable<UserToPermission>>>
        {
            private readonly IUserToPermissionRepository _userToPermissionRepository;

            public GetUserToPermissionListQueryHandler(IUserToPermissionRepository userToPermissionRepository)
            {
                _userToPermissionRepository = userToPermissionRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<UserToPermission>>> Handle(GetUserToPermissionListQuery request, CancellationToken cancellationToken)
            {
                var userToPermissionlist = await _userToPermissionRepository.GetListAsync(x => x.UserId == request.UserId);

                return new SuccessDataResult<IEnumerable<UserToPermission>>(userToPermissionlist.ToList());
            }
        }
    }
}
