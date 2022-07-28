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
    public class GetProviderStatusSummaryByIdQuery : IRequest<IDataResult<ProviderStatusSummaryDto>>
    {
        public int ProviderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public class GetProviderStatusSummaryByIdQueryHandler : IRequestHandler<GetProviderStatusSummaryByIdQuery, IDataResult<ProviderStatusSummaryDto>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetProviderStatusSummaryByIdQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<ProviderStatusSummaryDto>> Handle(GetProviderStatusSummaryByIdQuery request, CancellationToken cancellationToken)
            {
                var providerStatusSummaryObj = await _schedulerRepository.GetProviderStatusSummaryById(request.ProviderId, request.FromDate, request.ToDate);

                return new SuccessDataResult<ProviderStatusSummaryDto>(providerStatusSummaryObj);
            }
        }
    }
}
