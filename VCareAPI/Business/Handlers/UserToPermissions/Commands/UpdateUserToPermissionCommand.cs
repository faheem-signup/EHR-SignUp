using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IRolesRepository;
using DataAccess.Abstract.IUserToPermissionRepository;
using Entities.Concrete;
using Entities.Concrete.Role;
using Entities.Concrete.UserToPermissions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserToPermissions.Commands
{
    public class UpdateUserToPermissionCommand : IRequest<IResult>
    {
        public int UserToPermissionId { get; set; }
        public int RoleToPermissionId { get; set; }
        public int UserId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanDelete { get; set; }
        public class UpdateUserToPermissionCommandHandler : IRequestHandler<UpdateUserToPermissionCommand, IResult>
        {
            private readonly IUserToPermissionRepository _userToPermissionRepository;

            public UpdateUserToPermissionCommandHandler(IUserToPermissionRepository userToPermissionRepository)
            {
                _userToPermissionRepository = userToPermissionRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserToPermissionCommand request, CancellationToken cancellationToken)
            {
                UserToPermission userToPermissionObj = new UserToPermission
                {
                    UserToPermissionId = request.UserToPermissionId,
                    RoleToPermissionsId = request.RoleToPermissionId,
                    UserId = request.UserId,
                    CanView = request.CanView,
                    CanEdit = request.CanEdit,
                    CanAdd = request.CanAdd,
                    CanSearch = request.CanSearch,
                    CanDelete = request.CanDelete
                };

                _userToPermissionRepository.Update(userToPermissionObj);
                await _userToPermissionRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
