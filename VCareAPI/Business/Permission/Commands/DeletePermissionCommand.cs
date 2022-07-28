using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPermissionRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Permission.Commands
{
    public class DeletePermissionCommand : IRequest<IResult>
    {
        public int PermissionId { get; set; }
        public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, IResult>
        {
            private readonly IPermissionRepository _permissionRepository;

            public DeletePermissionCommandHandler(IPermissionRepository permissionRepository)
            {
                _permissionRepository = permissionRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
            {
                var permissionToDelete = await _permissionRepository.GetAsync(x => x.PermissionId == request.PermissionId);
                _permissionRepository.Delete(permissionToDelete);
                await _permissionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
