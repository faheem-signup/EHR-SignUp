using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticesRepository;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.PracticeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticeQuery : BasePaginationQuery<IDataResult<IEnumerable<PracticeDto>>>
    {
        public string LegalBusinessName { get; set; }
        public string TaxIDNumber { get; set; }
        public string BillingNPI { get; set; }
        public int StatusId { get; set; }
        public class GetDiagnosisQueryHandler : IRequestHandler<GetPracticeQuery, IDataResult<IEnumerable<PracticeDto>>>
        {
            private readonly IPracticesRepository _practicesRepository;
            public GetDiagnosisQueryHandler(IPracticesRepository practicesRepository)
            {
                _practicesRepository = practicesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PracticeDto>>> Handle(GetPracticeQuery request, CancellationToken cancellationToken)
            {
                var list = await _practicesRepository.GetPracticeSearchParams(request.LegalBusinessName, request.TaxIDNumber, request.BillingNPI, request.StatusId);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PracticeDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
