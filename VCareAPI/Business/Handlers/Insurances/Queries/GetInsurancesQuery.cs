using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IInsuranceRepository;
using Entities.Concrete.InsuranceEntity;
using Entities.Dtos.InsuranceDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Queries
{
    public class GetInsurancesQuery : BasePaginationQuery<IDataResult<IEnumerable<InsuranceDto>>>
    {
        public int PracticeId { get; set; }
        public int? PayerId { get; set; }
        public string Name { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public class GetInsurancesQueryHandler : IRequestHandler<GetInsurancesQuery, IDataResult<IEnumerable<InsuranceDto>>>
        {
            private readonly IInsuranceRepository _insuranceRepository;
            public GetInsurancesQueryHandler(IInsuranceRepository insuranceRepository)
            {
                _insuranceRepository = insuranceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<InsuranceDto>>> Handle(GetInsurancesQuery request, CancellationToken cancellationToken)
            {
                var list = await _insuranceRepository.GetInsuranceSearchParams(request.PracticeId, request.PayerId, request.Name, request.City, request.State, request.ZIP);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<InsuranceDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
