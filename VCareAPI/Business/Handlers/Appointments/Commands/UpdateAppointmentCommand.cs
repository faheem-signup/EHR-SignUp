using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
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
using Entities.Concrete.AppointmentsCheckInEntity;
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

namespace Business.Handlers.Appointments.Commands
{
    public class UpdateAppointmentCommand : IRequest<IResult>
    {
        public int AppointmentId { get; set; }
        public bool AllowGroupPatient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public string Duration { get; set; }
        public int? ProviderId { get; set; }
        public int? LocationId { get; set; }
        public int? ServiceProfileId { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? VisitType { get; set; }
        public int? VisitReason { get; set; }
        public int? RoomNo { get; set; }
        public int? PatientId { get; set; }
        public string GroupAppointmentReason { get; set; }
        public string Notes { get; set; }
        public bool? IsRecurringAppointment { get; set; }
        public bool? IsFollowUpAppointment { get; set; }
        public int? RecurringAppointmentId { get; set; }
        public string Weekdays { get; set; }
        public int? RecurEvery { get; set; }
        public int? WeekType { get; set; }
        public int? RecurringVisitReason { get; set; }
        public DateTime? FirstAppointDate { get; set; }
        public DateTime? LastAppointDate { get; set; }
        public int? FollowUpId { get; set; }
        public string FollowUpsVisitReason { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public int? AppointmentAutoReminderId { get; set; }
        public int[] PatientIds { get; set; }
        public string StatusReasons { get; set; }

        [TransactionScopeAspectAsync]
        public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IGroupPatientAppointmentRepository _groupPatientAppointmentRepository;
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;
            private readonly IFollowUpAppointmentRepository _followUpAppointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;
            public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository,
                IGroupPatientAppointmentRepository groupPatientAppointmentRepository,
                IRecurringAppointmentsRepository recurringAppointmentsRepository,
                IFollowUpAppointmentRepository followUpAppointmentRepository,
                IAppointmentsCheckInRepository appointmentCheckInRepository,
                IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = appointmentRepository;
                _groupPatientAppointmentRepository = groupPatientAppointmentRepository;
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
                _followUpAppointmentRepository = followUpAppointmentRepository;
                _appointmentCheckInRepository = appointmentCheckInRepository;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateAppointment), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var appointmentObj = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);

                appointmentObj.AppointmentId = request.AppointmentId;
                appointmentObj.AllowGroupPatient = request.AllowGroupPatient;
                appointmentObj.AppointmentDate = request.AppointmentDate;
                appointmentObj.TimeFrom = request.TimeFrom == null ? appointmentObj.TimeFrom : request.TimeFrom;
                appointmentObj.TimeTo = request.TimeTo == null ? appointmentObj.TimeTo : request.TimeTo;
                appointmentObj.ProviderId = request.ProviderId;
                appointmentObj.LocationId = request.LocationId;
                appointmentObj.AppointmentStatus = request.AppointmentStatus;
                appointmentObj.VisitType = request.VisitType;
                appointmentObj.VisitReason = request.VisitReason;
                appointmentObj.ServiceProfileId = request.ServiceProfileId;
                appointmentObj.RoomNo = request.RoomNo;
                appointmentObj.GroupAppointmentReason = request.GroupAppointmentReason;
                appointmentObj.Notes = request.Notes;
                appointmentObj.PatientId = request.PatientId;
                appointmentObj.IsRecurringAppointment = request.IsRecurringAppointment;
                appointmentObj.IsFollowUpAppointment = request.IsFollowUpAppointment;
                appointmentObj.ModifiedBy = int.Parse(userId);
                appointmentObj.ModifiedDate = DateTime.Now;
                appointmentObj.Duration = string.IsNullOrEmpty(request.Duration) ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(request.Duration);
                appointmentObj.StatusReasons = request.StatusReasons;

                _appointmentRepository.Update(appointmentObj);
                await _appointmentRepository.SaveChangesAsync();

                if (request.AppointmentStatus != null && request.AppointmentStatus != 0)
                {
                    var appointmentStatus = await _appointmentRepository.GetAppointmentStatusById((int)request.AppointmentStatus);

                    if (appointmentStatus != null)
                    {
                        AppointmentsCheckIn appointmentCheckin = new AppointmentsCheckIn
                        {
                            AppointmentId = appointmentObj.AppointmentId,
                            AppointmentStatusId = (int)request.AppointmentStatus,
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

                if (request.AllowGroupPatient)
                {

                    if (request.PatientIds.Count() > 0)
                    {
                        var existingList = await _groupPatientAppointmentRepository.GetListAsync(x => x.AppointmentId == appointmentObj.AppointmentId);

                        _groupPatientAppointmentRepository.RemoveGroupPatientAppointments(existingList);
                        await _groupPatientAppointmentRepository.SaveChangesAsync();

                        foreach (var item in request.PatientIds)
                        {
                            GroupPatientAppointment newObj = new GroupPatientAppointment
                            {
                                AppointmentId = appointmentObj.AppointmentId,
                                PatientId = item
                            };
                            _groupPatientAppointmentRepository.Add(newObj);
                        }

                        await _groupPatientAppointmentRepository.SaveChangesAsync();
                    }
                }
                else if ((bool)request.IsRecurringAppointment)
                {
                    var existingRecurringAppointment = await _recurringAppointmentsRepository.GetAsync(x => x.RecurringAppointmentId == request.RecurringAppointmentId);

                    if (existingRecurringAppointment != null)
                    {
                        existingRecurringAppointment.RecurringAppointmentId = (int)request.RecurringAppointmentId;
                        existingRecurringAppointment.Weekdays = request.Weekdays;
                        existingRecurringAppointment.RecurEvery = (int)request.RecurEvery;
                        existingRecurringAppointment.WeekType = (int)request.WeekType;
                        existingRecurringAppointment.RecurringVisitReason = (int)request.RecurringVisitReason;
                        existingRecurringAppointment.FirstAppointDate = request.FirstAppointDate;
                        existingRecurringAppointment.LastAppointDate = request.LastAppointDate;
                        existingRecurringAppointment.AppointmentId = appointmentObj.AppointmentId;
                        _recurringAppointmentsRepository.Update(existingRecurringAppointment);
                        await _recurringAppointmentsRepository.SaveChangesAsync();
                    }
                }
                else if ((bool)request.IsFollowUpAppointment)
                {
                    var existingFollowUpAppointment = await _followUpAppointmentRepository.GetAsync(x => x.FollowUpId == request.FollowUpId);
                    if (existingFollowUpAppointment != null)
                    {
                        existingFollowUpAppointment.FollowUpId = (int)request.FollowUpId;
                        existingFollowUpAppointment.FollowUpsVisitReason = request.FollowUpsVisitReason;
                        existingFollowUpAppointment.FollowUpDate = request.FollowUpDate;
                        existingFollowUpAppointment.AppointmentAutoReminderId = request.AppointmentAutoReminderId;
                        existingFollowUpAppointment.AppointmentId = appointmentObj.AppointmentId;
                    }

                    _followUpAppointmentRepository.Update(existingFollowUpAppointment);
                    await _followUpAppointmentRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
