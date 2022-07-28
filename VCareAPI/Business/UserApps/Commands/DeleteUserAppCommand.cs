using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserAppRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Commands
{
    public class DeleteUserAppCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public class DeleteUserAppCommandHandler : IRequestHandler<DeleteUserAppCommand, IResult>
        {
            private readonly IUserAppRepository _userAppRepository;

            public DeleteUserAppCommandHandler(IUserAppRepository userAppRepository)
            {
                _userAppRepository = userAppRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteUserAppCommand request, CancellationToken cancellationToken)
            {
                var UserToDelete = await _userAppRepository.GetAsync(x => x.UserId == request.Id && x.IsDeleted != true);
                UserToDelete.IsDeleted = true;
                _userAppRepository.Update(UserToDelete);
                await _userAppRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
