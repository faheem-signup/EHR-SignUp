using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Dtos.ReferralProviderDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReferralProviders.Queries
{
    public class GetReferralProviderListQuery : BasePaginationQuery<IDataResult<IEnumerable<ReferralProviderDto>>>
    {
        public string ReferralProviderName { get; set; }
        public string NPI { get; set; }
        public string Phone { get; set; }
        public int? ZipCode { get; set; }

        public class GetReferralProviderListQueryHandler : IRequestHandler<GetReferralProviderListQuery, IDataResult<IEnumerable<ReferralProviderDto>>>
        {
            private readonly IReferralProviderRepository _referralProviderRepository;

            public GetReferralProviderListQueryHandler(IReferralProviderRepository referralProviderRepository)
            {
                _referralProviderRepository = referralProviderRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ReferralProviderDto>>> Handle(GetReferralProviderListQuery request, CancellationToken cancellationToken)
            {
                var list = await _referralProviderRepository.GetReferralProviderBySearchParams(request.ReferralProviderName, request.NPI, request.Phone,request.ZipCode);

                return new PagedDataResult<IEnumerable<ReferralProviderDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
