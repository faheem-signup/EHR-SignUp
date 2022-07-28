using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providers.Commands
{
    public class DeleteProviderCommand : IRequest<IResult>
    {
        public int ProviderId { get; set; }
        public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand, IResult>
        {
            private readonly IProviderRepository _providerRepository;

            public DeleteProviderCommandHandler(IProviderRepository providerRepository)
            {
                _providerRepository = providerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
            {
                var providerToDelete = await _providerRepository.GetAsync(x => x.ProviderId == request.ProviderId && x.IsDeleted != true);
                providerToDelete.IsDeleted = true;
                _providerRepository.Update(providerToDelete);
                await _providerRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
