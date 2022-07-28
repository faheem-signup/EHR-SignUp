using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserWorkHourConfigs.Commands
{
    public class DeleteUserWorkHourConfigCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public class DeleteUserWorkHourConfigCommandHandler : IRequestHandler<DeleteUserWorkHourConfigCommand, IResult>
        {
            private readonly IUserWorkHourRepository _userWorkHourRepository;

            public DeleteUserWorkHourConfigCommandHandler(IUserWorkHourRepository userWorkHourRepository)
            {
                _userWorkHourRepository = userWorkHourRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteUserWorkHourConfigCommand request, CancellationToken cancellationToken)
            {
                var UserWorkHourToDelete = await _userWorkHourRepository.GetAsync(x => x.Id == request.Id);

                _userWorkHourRepository.Delete(UserWorkHourToDelete);
                await _userWorkHourRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
