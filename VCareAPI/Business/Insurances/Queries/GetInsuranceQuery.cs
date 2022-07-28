using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract.IInsuranceRepository;
using Entities.Concrete.InsuranceEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Queries
{
    public class GetInsuranceQuery : IRequest<IDataResult<Insurance>>
    {
        public int InsuranceId { get; set; }

        public class GetInsuranceQueryHandler : IRequestHandler<GetInsuranceQuery, IDataResult<Insurance>>
        {
            private readonly IInsuranceRepository _insuranceRepository;
            public GetInsuranceQueryHandler(IInsuranceRepository insuranceRepository)
            {
                _insuranceRepository = insuranceRepository;
            }

            public async Task<IDataResult<Insurance>> Handle(GetInsuranceQuery request, CancellationToken cancellationToken)
            {
                var insurance = await _insuranceRepository.GetAsync(x => x.InsuranceId == request.InsuranceId);

                return new SuccessDataResult<Insurance>(insurance);
            }
        }
    }
}
