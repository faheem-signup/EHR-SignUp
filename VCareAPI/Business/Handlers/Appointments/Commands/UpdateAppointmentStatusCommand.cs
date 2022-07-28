using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using Entities.Concrete.AppointmentsCheckInEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Commands
{
    public class UpdateAppointmentStatusCommand : IRequest<IResult>
    {
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }

        public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;

            public UpdateAppointmentStatusCommandHandler(IAppointmentRepository appointmentRepository,
                IAppointmentsCheckInRepository appointmentCheckInRepository,
                IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = appointmentRepository;
                _appointmentCheckInRepository = appointmentCheckInRepository;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var appointmentObj = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
                if (appointmentObj != null)
                {
                    appointmentObj.AppointmentId = request.AppointmentId;
                    appointmentObj.AppointmentStatus = request.AppointmentStatusId;
                    appointmentObj.ModifiedBy = int.Parse(userId);
                    appointmentObj.ModifiedDate = DateTime.Now;

                    _appointmentRepository.Update(appointmentObj);
                    await _appointmentRepository.SaveChangesAsync();

                    if (request.AppointmentStatusId != null && request.AppointmentStatusId != 0)
                    {
                        var appointmentStatus = await _appointmentRepository.GetAppointmentStatusById(request.AppointmentStatusId);

                        if (appointmentStatus != null)
                        {
                            AppointmentsCheckIn appointmentCheckin = new AppointmentsCheckIn
                            {
                                AppointmentId = appointmentObj.AppointmentId,
                                AppointmentStatusId = (int)request.AppointmentStatusId,
                                CreatedBy = int.Parse(userId),
                                CreatedDate = DateTime.Now,
                                ModifiedBy = int.Parse(userId),
                                ModifiedDate = DateTime.Now,
                                IsDeleted = false,
                            };

                            if (appointmentStatus.AppointmentStatusName == "Checked In")
                            {
                                appointmentCheckin.AppointmentCheckInDateTime = DateTime.Now;
                            }
                            else if (appointmentStatus.AppointmentStatusName == "Checked Out")
                            {
                                appointmentCheckin.AppointmentCheckOutDateTime = DateTime.Now;
                            }
                            else if (appointmentStatus.AppointmentStatusName == "Cancelled")
                            {
                                appointmentCheckin.AppointmentCancelledDatetime = DateTime.Now;
                            }
                            else if (appointmentStatus.AppointmentStatusName == "Rescheduled")
                            {
                                appointmentCheckin.AppointmentRescheduleDateTime = DateTime.Now;
                            }
                            else if (appointmentStatus.AppointmentStatusName == "No Show")
                            {
                                appointmentCheckin.AppointmentNoShowDatetime = DateTime.Now;
                            }
                            else if (appointmentStatus.AppointmentStatusName == "Scheduled")
                            {
                                appointmentCheckin.AppointmentScheduledDatetime = DateTime.Now;
                            }

                            _appointmentCheckInRepository.Add(appointmentCheckin);
                            await _appointmentCheckInRepository.SaveChangesAsync();
                        }
                    }

                    return new SuccessResult(Messages.Updated);
                }

                return new ErrorResult(Messages.NoRecordFound);
            }
        }
    }
}
