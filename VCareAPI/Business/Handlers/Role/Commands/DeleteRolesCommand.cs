using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRolesRepository;
using DataAccess.Abstract.IRoleToPermissionRepository;
using DataAccess.Abstract.IUserAppRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Role.Commands
{
    public class DeleteRolesCommand : IRequest<IResult>
    {
        public int RoleId { get; set; }

        public class DeleteRolesCommandHandler : IRequestHandler<DeleteRolesCommand, IResult>
        {
            private readonly IRolesRepository _rolesRepository;
            private readonly IUserAppRepository _userAppRepository;
            private readonly IRoleToPermissionRepository _roleToPermissionRepository;

            public DeleteRolesCommandHandler(IRolesRepository rolesRepository, IUserAppRepository userAppRepository, IRoleToPermissionRepository roleToPermissionRepository)
            {
                _rolesRepository = rolesRepository;
                _userAppRepository = userAppRepository;
                _roleToPermissionRepository = roleToPermissionRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteRolesCommand request, CancellationToken cancellationToken)
            {
                var checkRole = await _userAppRepository.GetAsync(x => x.RoleId == request.RoleId);
                if (checkRole != null)
                {
                    return new ErrorResult(Messages.CantDelete);
                }

                var checkRoleToPermission = await _roleToPermissionRepository.GetAsync(x => x.RoleId == request.RoleId);
                if (checkRoleToPermission != null)
                {
                    return new ErrorResult(Messages.CantDelete);
                }

                var roleToDelete = await _rolesRepository.GetAsync(x => x.RoleId == request.RoleId);

                _rolesRepository.Delete(roleToDelete);
                await _rolesRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
