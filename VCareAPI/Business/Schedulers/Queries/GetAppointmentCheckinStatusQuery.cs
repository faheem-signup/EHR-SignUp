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
    public class GetAppointmentCheckinStatusQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientAppointmentStatusDto>>>
    {
        public int ProviderId { get; set; }

        public class GetAppointmentCheckinStatusQueryHandler : IRequestHandler<GetAppointmentCheckinStatusQuery, IDataResult<IEnumerable<PatientAppointmentStatusDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetAppointmentCheckinStatusQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientAppointmentStatusDto>>> Handle(GetAppointmentCheckinStatusQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerRepository.GetAppointmentCheckinStatus(request.ProviderId);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PatientAppointmentStatusDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
