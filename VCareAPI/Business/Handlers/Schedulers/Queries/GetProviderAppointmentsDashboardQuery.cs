using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Dtos.SchedulerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetProviderAppointmentsDashboardQuery : IRequest<IDataResult<ProviderStatusSummaryDto>>
    {
        public int ProviderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public class GetProviderAppointmentsDashboardQueryHandler : IRequestHandler<GetProviderAppointmentsDashboardQuery, IDataResult<ProviderStatusSummaryDto>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            public GetProviderAppointmentsDashboardQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<ProviderStatusSummaryDto>> Handle(GetProviderAppointmentsDashboardQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerRepository.GetProviderAppointmentsDashboard(request.ProviderId, request.FromDate, request.ToDate);
                return new SuccessDataResult<ProviderStatusSummaryDto>(list);
            }
        }
    }
}
