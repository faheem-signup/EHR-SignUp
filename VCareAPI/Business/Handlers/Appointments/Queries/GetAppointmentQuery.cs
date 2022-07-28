using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IAppointmentRepository;
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

            public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

           // [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AppointmentDTO>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
            {
                // var appointment = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
               var appointment= await _appointmentRepository.GetAppointmentDetailById(request.AppointmentId);
                return new SuccessDataResult<AppointmentDTO>(appointment);
            }
        }
    }
}
