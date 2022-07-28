using AutoMapper;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IBillingClaimRepository;
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
    public class GetBillingClaimListQuery : BasePaginationQuery<IDataResult<IEnumerable<BillingClaimDto>>>
    {
        public class GetBillingClaimListQueryHandler : IRequestHandler<GetBillingClaimListQuery, IDataResult<IEnumerable<BillingClaimDto>>>
        {
            private readonly IBillingClaimRepository _billingClaimRepository;
            private readonly IMapper _mapper;

            public GetBillingClaimListQueryHandler(IBillingClaimRepository billingClaimRepository, IMapper mapper)
            {
                _billingClaimRepository = billingClaimRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<BillingClaimDto>>> Handle(GetBillingClaimListQuery request, CancellationToken cancellationToken)
            {
                BillingClaimDto obj = new BillingClaimDto();
                var list = await _billingClaimRepository.GetBillingClaimList();

                var dataList = Paginate(list, request.PageNumber, request.PageSize);

                return new PagedDataResult<IEnumerable<BillingClaimDto>>(dataList, list.Count(), request.PageNumber);
            }
        }
    }
}
