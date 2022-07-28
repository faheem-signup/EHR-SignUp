using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IBillingClaimRepository;
using DataAccess.Abstract.IBillingClaimsPaymentDetailRepository;
using Entities.Concrete.BillingClaim;
using Entities.Dtos.BillingClaimsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BillingClaims.Queries
{
    public class GetBillingClaimByIdQuery : IRequest<IDataResult<BillingClaimDto>>
    {
        public int BillingClaimId { get; set; }

        public class GetBillingClaimQueryHandler : IRequestHandler<GetBillingClaimByIdQuery, IDataResult<BillingClaimDto>>
        {
            private readonly IBillingClaimRepository _BillingClaimRepository;
            private readonly IBillingClaimsPaymentDetailRepository _billingClaimsPaymentDetailRepository;
            public GetBillingClaimQueryHandler(IBillingClaimRepository BillingClaimRepository, IBillingClaimsPaymentDetailRepository billingClaimsPaymentDetailRepository)
            {
                _BillingClaimRepository = BillingClaimRepository;
                _billingClaimsPaymentDetailRepository = billingClaimsPaymentDetailRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<BillingClaimDto>> Handle(GetBillingClaimByIdQuery request, CancellationToken cancellationToken)
            {
                var billingClaimObj = await _BillingClaimRepository.GetBillingClaimById(request.BillingClaimId);
                if(billingClaimObj !=null)
                {
                    billingClaimObj.billingClaimsPaymentDetailList = await _billingClaimsPaymentDetailRepository.GetListAsync(x => x.BillingClaimsId == request.BillingClaimId);
                }
                return new SuccessDataResult<BillingClaimDto>(billingClaimObj);
            }
        }
    }
}
