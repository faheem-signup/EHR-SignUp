using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Dtos.DashboardDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Dashboard.Queries
{
    public class GetProviderAppointmentsGraphChartQuery : IRequest<IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>>
    {
        public int ProviderId { get; set; }

        public class GetProviderAppointmentsGraphChartQueryHandler : IRequestHandler<GetProviderAppointmentsGraphChartQuery, IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            public GetProviderAppointmentsGraphChartQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>> Handle(GetProviderAppointmentsGraphChartQuery request, CancellationToken cancellationToken)
            {
                var data = await _schedulerRepository.GetProviderAppointmentsGraphChartDashboard(request.ProviderId);

                return new SuccessDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>(data);
            }
        }
    }
}
