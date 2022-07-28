using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPermissionRepository;
using Entities.Concrete;
using Entities.Concrete.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Permission.Commands
{
    public class UpdatePermissionCommand : IRequest<IResult>
    {
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }
        public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, IResult>
        {
            private readonly IPermissionRepository _permissionRepository;

            public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository)
            {
                _permissionRepository = permissionRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
            {
                Permissions permissionsObj = new Permissions
                {
                    PermissionId = request.PermissionId,
                    PermissionDescription = request.PermissionDescription,
                };

                _permissionRepository.Update(permissionsObj);
                await _permissionRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
