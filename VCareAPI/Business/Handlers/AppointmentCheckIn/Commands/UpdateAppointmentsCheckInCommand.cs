using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.FollowUpAppointmentEntity;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Entities.Concrete.RecurringAppointmentsEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentCheckIn.Commands
{
    public class UpdateAppointmentsCheckInCommand : IRequest<IResult>
    {
        public int AppointmentsCheckInId { get; set; }
      //  public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
     //   public DateTime? AppointmentCheckInDateTime { get; set; }
        public DateTime? AppointmentCheckOutDateTime { get; set; }
        public class UpdateAppointmentsCheckInCommandHandler : IRequestHandler<UpdateAppointmentsCheckInCommand, IResult>
        {
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;

            public UpdateAppointmentsCheckInCommandHandler(IAppointmentsCheckInRepository appointmentCheckInRepository)
            {
                _appointmentCheckInRepository = appointmentCheckInRepository;
            }

            // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateAppointmentsCheckInCommand request, CancellationToken cancellationToken)
            {
                var appointmentCheckInObj = await _appointmentCheckInRepository.GetAsync(x=>x.AppointmentsCheckInId == request.AppointmentsCheckInId);
                appointmentCheckInObj.AppointmentCheckOutDateTime = request.AppointmentCheckOutDateTime;
                appointmentCheckInObj.AppointmentStatusId = request.AppointmentStatusId;

                _appointmentCheckInRepository.Update(appointmentCheckInObj);
                await _appointmentCheckInRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
