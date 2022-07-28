using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete.ReferralProviderEntity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReferralProviders.Queries
{
    public class GetReferralProviderQuery : IRequest<IDataResult<ReferralProvider>>
    {
        public int ReferralProviderId { get; set; }

        public class GetReferralProviderQueryHandler : IRequestHandler<GetReferralProviderQuery, IDataResult<ReferralProvider>>
        {
            private readonly IReferralProviderRepository _referralProviderRepository;

            public GetReferralProviderQueryHandler(IReferralProviderRepository referralProviderRepository)
            {
                _referralProviderRepository = referralProviderRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ReferralProvider>> Handle(GetReferralProviderQuery request, CancellationToken cancellationToken)
            {
                var referralProvider = await _referralProviderRepository.GetAsync(x=>x.ReferralProviderId == request.ReferralProviderId);
                return new SuccessDataResult<ReferralProvider>(referralProvider);
            }
        }
    }
}
