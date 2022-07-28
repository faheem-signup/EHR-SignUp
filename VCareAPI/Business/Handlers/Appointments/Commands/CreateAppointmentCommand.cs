namespace Business.Handlers.Appointments.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.BusinessAspects;
    using Business.Constants;
    using Business.Helpers.Validators;
    using Core.Aspects.Autofac.Logging;
    using Core.Aspects.Autofac.Transaction;
    using Core.Aspects.Autofac.Validation;
    using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
    using Core.Utilities.Results;
    using DataAccess.Abstract.IAppointmentRepository;
    using DataAccess.Abstract.IAppointmentsCheckInRepository;
    using DataAccess.Abstract.IFollowUpAppointmentRepository;
    using DataAccess.Abstract.IGroupPatientAppointmentRepository;
    using DataAccess.Abstract.IProviderWorkConfigRepository;
    using DataAccess.Abstract.IRecurringAppointmentsRepository;
    using Entities.Concrete.AppointmentEntity;
    using Entities.Concrete.AppointmentsCheckInEntity;
    using Entities.Concrete.FollowUpAppointmentEntity;
    using Entities.Concrete.GroupPatientAppointmentEntity;
    using Entities.Concrete.RecurringAppointmentsEntity;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateAppointmentCommand : IRequest<IResult>
    {
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
        public string Weekdays { get; set; }
        public int? RecurEvery { get; set; }
        public int? WeekType { get; set; }
        public int? RecurringVisitReason { get; set; }
        public DateTime? FirstAppointDate { get; set; }
        public DateTime? LastAppointDate { get; set; }
        public string FollowUpsVisitReason { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public int? AppointmentAutoReminderId { get; set; }
        public int[] PatientIds { get; set; }

        [TransactionScopeAspectAsync]
        public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IGroupPatientAppointmentRepository _groupPatientAppointmentRepository;
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;
            private readonly IFollowUpAppointmentRepository _followUpAppointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;

            public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMediator mediator, IMapper mapper, IGroupPatientAppointmentRepository groupPatientAppointmentRepository, IRecurringAppointmentsRepository recurringAppointmentsRepository, IFollowUpAppointmentRepository followUpAppointmentRepository, IHttpContextAccessor contextAccessor, IProviderWorkConfigRepository providerWorkConfigRepository, IAppointmentsCheckInRepository appointmentCheckInRepository)
            {
                _appointmentRepository = appointmentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _groupPatientAppointmentRepository = groupPatientAppointmentRepository;
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
                _followUpAppointmentRepository = followUpAppointmentRepository;
                _contextAccessor = contextAccessor;
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _appointmentCheckInRepository = appointmentCheckInRepository;
            }

            [ValidationAspect(typeof(ValidatorAppointment), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var providerWorkConfigsList = await _providerWorkConfigRepository.GetListAsync(x => x.ProviderId == request.ProviderId);
                if (providerWorkConfigsList.Count() == 0)
                {
                    return new ErrorResult("Provider WorkConfig not added");
                }

                if (request.TimeFrom != null)
                {
                    request.TimeFrom = request.TimeFrom?.AddSeconds(-Convert.ToDouble(request.TimeFrom?.Second));
                }

                if (request.TimeTo != null)
                {
                    request.TimeTo = request.TimeTo?.AddSeconds(-Convert.ToDouble(request.TimeTo?.Second));
                }

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                userId = string.IsNullOrEmpty(userId) ? "1" : userId;
                Appointment appointmentObj = new Appointment
                {
                    AllowGroupPatient = request.AllowGroupPatient,
                    AppointmentDate = request.AppointmentDate,
                    TimeFrom = request.TimeFrom,
                    TimeTo = request.TimeTo,
                    ProviderId = request.ProviderId,
                    LocationId = request.LocationId,
                    AppointmentStatus = request.AppointmentStatus,
                    VisitType = request.VisitType,
                    VisitReason = request.VisitReason,
                    ServiceProfileId = request.ServiceProfileId,
                    RoomNo = request.RoomNo,
                    GroupAppointmentReason = request.GroupAppointmentReason,
                    Notes = request.Notes,
                    PatientId = request.PatientId,
                    IsRecurringAppointment = request.IsRecurringAppointment,
                    IsFollowUpAppointment = request.IsFollowUpAppointment,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Duration = string.IsNullOrEmpty(request.Duration) ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(request.Duration),
                };

                _appointmentRepository.Add(appointmentObj);
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
                        foreach (var item in request.PatientIds)
                        {
                            GroupPatientAppointment newObj = new GroupPatientAppointment
                            {
                                AppointmentId = appointmentObj.AppointmentId,
                                PatientId = item,
                            };
                            _groupPatientAppointmentRepository.Add(newObj);
                        }

                        await _groupPatientAppointmentRepository.SaveChangesAsync();
                    }
                }
                else if ((bool)request.IsRecurringAppointment)
                {
                    RecurringAppointments recurringAppointmentObj = new RecurringAppointments()
                    {
                        Weekdays = request.Weekdays.ToString(),
                        RecurEvery = (int)request.RecurEvery,
                        WeekType = (int)request.WeekType,
                        RecurringVisitReason = (int)request.RecurringVisitReason,
                        FirstAppointDate = request.FirstAppointDate,
                        LastAppointDate = request.LastAppointDate,
                        AppointmentId = appointmentObj.AppointmentId,
                    };
                    _recurringAppointmentsRepository.Add(recurringAppointmentObj);
                    await _recurringAppointmentsRepository.SaveChangesAsync();
                }
                else if ((bool)request.IsFollowUpAppointment)
                {
                    FollowUpAppointment followUpAppointmentObj = new FollowUpAppointment()
                    {
                        FollowUpsVisitReason = request.FollowUpsVisitReason,
                        FollowUpDate = request.FollowUpDate,
                        AppointmentAutoReminderId = request.AppointmentAutoReminderId,
                        AppointmentId = appointmentObj.AppointmentId,
                    };
                    _followUpAppointmentRepository.Add(followUpAppointmentObj);
                    await _followUpAppointmentRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}