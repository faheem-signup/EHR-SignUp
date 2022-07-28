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
    public class GetAdminAppointmentsPieChartQuery : IRequest<IDataResult<IEnumerable<AdminAppointmentsPieChartDto>>>
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public class GetAdminAppointmentsPieChartQueryHandler : IRequestHandler<GetAdminAppointmentsPieChartQuery, IDataResult<IEnumerable<AdminAppointmentsPieChartDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            public GetAdminAppointmentsPieChartQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AdminAppointmentsPieChartDto>>> Handle(GetAdminAppointmentsPieChartQuery request, CancellationToken cancellationToken)
            {
                var data = await _schedulerRepository.GetAdminAppointmentsPieChartDashboard(request.FromDate, request.ToDate);
                return new SuccessDataResult<IEnumerable<AdminAppointmentsPieChartDto>>(data);
            }
        }
    }
}
