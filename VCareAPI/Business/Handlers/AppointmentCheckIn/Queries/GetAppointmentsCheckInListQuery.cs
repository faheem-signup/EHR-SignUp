using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.AppointmentDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentCheckIn.Queries
{
    public class GetAppointmentsCheckInListQuery : BasePaginationQuery<IDataResult<IEnumerable<AppointmentsCheckInDto>>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public class GetAppointmentListQueryHandler : IRequestHandler<GetAppointmentsCheckInListQuery, IDataResult<IEnumerable<AppointmentsCheckInDto>>>
        {
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;

            public GetAppointmentListQueryHandler(IAppointmentsCheckInRepository appointmentCheckInRepository)
            {
                _appointmentCheckInRepository = appointmentCheckInRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AppointmentsCheckInDto>>> Handle(GetAppointmentsCheckInListQuery request, CancellationToken cancellationToken)
            {
                var appointmentCheckInList = await _appointmentCheckInRepository.GetAppointmentsCheckInList();
                var pagedData = Paginate(appointmentCheckInList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<AppointmentsCheckInDto>>(pagedData, appointmentCheckInList.Count(), request.PageNumber);
            }
        }
    }
}
