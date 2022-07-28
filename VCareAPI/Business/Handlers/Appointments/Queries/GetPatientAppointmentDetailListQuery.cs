using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using Entities.Concrete.AppointmentEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Queries
{
    public class GetPatientAppointmentDetailListQuery : BasePaginationQuery<IDataResult<IEnumerable<Appointment>>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public int AppointmentId { get; set; }
        public int ProviderId { get; set; }

        public class GetPatientAppointmentDetailListQueryHandler : IRequestHandler<GetPatientAppointmentDetailListQuery, IDataResult<IEnumerable<Appointment>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;

            public GetPatientAppointmentDetailListQueryHandler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Appointment>>> Handle(GetPatientAppointmentDetailListQuery request, CancellationToken cancellationToken)
            {
                var appointmentList = await _appointmentRepository.GetListAsync(x => x.AppointmentId == request.AppointmentId && x.ProviderId == request.ProviderId);
                if (appointmentList.Count() > 0)
                {
                    appointmentList = appointmentList.ToList().OrderByDescending(x => x.AppointmentDate);
                }

                var pagedData = Paginate(appointmentList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Appointment>>(pagedData, appointmentList.Count(), request.PageNumber);
            }
        }
    }
}
