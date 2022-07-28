using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
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

namespace Business.Handlers.Appointments.Commands
{

    public class CreateAppointmentCommand : IRequest<IResult>
    {
        public Boolean AllowGroupPatient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int ProviderId { get; set; }
        public int LocationId { get; set; }
        public int AppointmentStatus { get; set; }
        public int VisitType { get; set; }
        public int VisitReason { get; set; }
        public int RoomNo { get; set; }
        public int PatientId { get; set; }
        public string GroupAppointmentReason { get; set; }
        public string Notes { get; set; }
        public Boolean IsRecurringAppointment { get; set; }
        public Boolean IsFollowUpAppointment { get; set; }
        public string Weekdays { get; set; }
        public int RecurEvery { get; set; }
        public int WeekType { get; set; }
        public int RecurringVisitReason { get; set; }
        public DateTime FirstAppointDate { get; set; }
        public DateTime LastAppointDate { get; set; }
        public string FollowUpsVisitReason { get; set; }
        public DateTime FollowUpDate { get; set; }
        public int[] PatientIds { get; set; }


        public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IGroupPatientAppointmentRepository _groupPatientAppointmentRepository;
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;
            private readonly IFollowUpAppointmentRepository _followUpAppointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMediator mediator, IMapper mapper, IGroupPatientAppointmentRepository groupPatientAppointmentRepository, IRecurringAppointmentsRepository recurringAppointmentsRepository, IFollowUpAppointmentRepository followUpAppointmentRepository, IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = appointmentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _groupPatientAppointmentRepository = groupPatientAppointmentRepository;
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
                _followUpAppointmentRepository = followUpAppointmentRepository;
                _contextAccessor = contextAccessor;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
           // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

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
                    RoomNo = request.RoomNo,
                    GroupAppointmentReason = request.GroupAppointmentReason,
                    Notes = request.Notes,
                    PatientId = request.PatientId,
                    IsRecurringAppointment = request.IsRecurringAppointment,
                    IsFollowUpAppointment = request.IsFollowUpAppointment,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _appointmentRepository.Add(appointmentObj);
                await _appointmentRepository.SaveChangesAsync();


                if (request.AllowGroupPatient)
                {
                    if (request.PatientIds.Count() > 0)
                    {
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
                else if (request.IsRecurringAppointment)
                {
                    Entities.Concrete.RecurringAppointmentsEntity.RecurringAppointments recurringAppointmentObj = new Entities.Concrete.RecurringAppointmentsEntity.RecurringAppointments()
                    {
                        Weekdays = request.Weekdays,
                        RecurEvery = request.RecurEvery,
                        WeekType = request.WeekType,
                        RecurringVisitReason = request.RecurringVisitReason,
                        FirstAppointDate = request.FirstAppointDate,
                        LastAppointDate = request.LastAppointDate,
                        AppointmentId = appointmentObj.AppointmentId
                    };
                    _recurringAppointmentsRepository.Add(recurringAppointmentObj);
                    await _recurringAppointmentsRepository.SaveChangesAsync();
                }
                else if (request.IsFollowUpAppointment)
                {
                    FollowUpAppointment followUpAppointmentObj = new FollowUpAppointment()
                    {
                        FollowUpsVisitReason = request.FollowUpsVisitReason,
                        FollowUpDate = request.FollowUpDate,
                        AppointmentId = appointmentObj.AppointmentId
                    };
                    _followUpAppointmentRepository.Add(followUpAppointmentObj);
                    await _followUpAppointmentRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

