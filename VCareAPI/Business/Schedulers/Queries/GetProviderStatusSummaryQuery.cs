using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Dtos.SchedulerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetProviderStatusSummaryQuery : BasePaginationQuery<IDataResult<IEnumerable<ProviderStatusSummaryDto>>>
    {
        public int? ProviderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public class GetProviderStatusSummaryQueryHandler : IRequestHandler<GetProviderStatusSummaryQuery, IDataResult<IEnumerable<ProviderStatusSummaryDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetProviderStatusSummaryQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProviderStatusSummaryDto>>> Handle(GetProviderStatusSummaryQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerRepository.GetProviderStatusSummary(request.ProviderId, request.FromDate, request.ToDate);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ProviderStatusSummaryDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
