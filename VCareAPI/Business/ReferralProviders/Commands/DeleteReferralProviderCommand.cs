using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReferralProviderRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReferralProviders.Commands
{
    public class DeleteReferralProviderCommand : IRequest<IResult>
    {
        public int ReferralProviderId { get; set; }
        public class DeleteReferralProviderCommandHandler : IRequestHandler<DeleteReferralProviderCommand, IResult>
        {
            private readonly IReferralProviderRepository _referralProviderRepository;

            public DeleteReferralProviderCommandHandler(IReferralProviderRepository referralProviderRepository)
            {
                _referralProviderRepository = referralProviderRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteReferralProviderCommand request, CancellationToken cancellationToken)
            {
                var referralProviderRepository = await _referralProviderRepository.GetAsync(x => x.ReferralProviderId == request.ReferralProviderId);

                _referralProviderRepository.Delete(referralProviderRepository);
                await _referralProviderRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
