using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.AppointmentDto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Queries
{
    public class GetAppointmentQuery : IRequest<IDataResult<AppointmentDTO>>
    {
        public int AppointmentId { get; set; }

        public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, IDataResult<AppointmentDTO>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IGroupPatientAppointmentRepository _groupPatientAppointmentRepository;
            private readonly IFollowUpAppointmentRepository _followUpAppointmentRepository;
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;

            public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IGroupPatientAppointmentRepository groupPatientAppointmentRepository, IFollowUpAppointmentRepository followUpAppointmentRepository, IRecurringAppointmentsRepository recurringAppointmentsRepository)
            {
                _appointmentRepository = appointmentRepository;
                _groupPatientAppointmentRepository = groupPatientAppointmentRepository;
                _followUpAppointmentRepository = followUpAppointmentRepository;
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
            }

            // [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<AppointmentDTO>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
            {
                // var appointment = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
              // var appointment= await _appointmentRepository.GetAppointmentDetailById(request.AppointmentId);
               var appointment= await _appointmentRepository.GetAppointemntById(request.AppointmentId);
                if((bool)appointment.AllowGroupPatient)
                {
                  appointment.GroupPatientAppointmentList = await _groupPatientAppointmentRepository.GetListAsync(x => x.AppointmentId == request.AppointmentId);
                }
                else if ((bool)appointment.IsFollowUpAppointment)
                {
                    appointment.FollowUpAppointments = await _followUpAppointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
                }
                else if ((bool)appointment.IsRecurringAppointment)
                {
                    appointment.RecurringAppointments = await _recurringAppointmentsRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
                }
                return new SuccessDataResult<AppointmentDTO>(appointment);
            }
        }
    }
}
