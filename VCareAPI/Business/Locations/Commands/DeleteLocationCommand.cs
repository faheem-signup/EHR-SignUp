using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Locations.Commands
{
    public class DeleteLocationCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, IResult>
        {
            private readonly ILocationRepository _locationRepository;

            public DeleteLocationCommandHandler(ILocationRepository locationRepository)
            {
                _locationRepository = locationRepository;
            }

           // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
            {
                var LocationToDelete = await _locationRepository.GetAsync(x => x.LocationId == request.Id);

                _locationRepository.Delete(LocationToDelete);
                await _locationRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
