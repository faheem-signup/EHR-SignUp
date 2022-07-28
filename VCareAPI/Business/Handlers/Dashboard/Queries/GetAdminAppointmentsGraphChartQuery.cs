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
    public class GetAdminAppointmentsGraphChartQuery : IRequest<IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>>
    {
        public class GetAdminAppointmentsGraphChartQueryHandler : IRequestHandler<GetAdminAppointmentsGraphChartQuery, IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            public GetAdminAppointmentsGraphChartQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>> Handle(GetAdminAppointmentsGraphChartQuery request, CancellationToken cancellationToken)
            {
                var data = await _schedulerRepository.GetAdminAppointmentsGraphChartDashboard();

                return new SuccessDataResult<IEnumerable<AdminAppointmentsGraphChartDto>>(data);
            }
        }
    }
}
