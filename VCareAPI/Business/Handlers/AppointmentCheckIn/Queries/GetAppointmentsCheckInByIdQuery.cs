using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.AppointmentsCheckInEntity;
using Entities.Dtos.AppointmentDto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentCheckIn.Queries
{
    public class GetAppointmentsCheckInByIdQuery : IRequest<IDataResult<AppointmentsCheckIn>>
    {
        public int AppointmentsCheckInId { get; set; }

        public class GetAppointmentsCheckInByIdQueryHandler : IRequestHandler<GetAppointmentsCheckInByIdQuery, IDataResult<AppointmentsCheckIn>>
        {
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;

            public GetAppointmentsCheckInByIdQueryHandler(IAppointmentsCheckInRepository appointmentCheckInRepository)
            {
                _appointmentCheckInRepository = appointmentCheckInRepository;
            }

            // [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<AppointmentsCheckIn>> Handle(GetAppointmentsCheckInByIdQuery request, CancellationToken cancellationToken)
            {
                // var appointment = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
                var appointment = await _appointmentCheckInRepository.GetAsync(x => x.AppointmentsCheckInId == request.AppointmentsCheckInId);
                return new SuccessDataResult<AppointmentsCheckIn>(appointment);
            }
        }
    }
}
