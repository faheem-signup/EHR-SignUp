using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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
        public int RecurringAppointmentId { get; set; }
        public string Weekdays { get; set; }
        public int RecurEvery { get; set; }
        public int WeekType { get; set; }
        public int RecurringVisitReason { get; set; }
        public DateTime FirstAppointDate { get; set; }
        public DateTime LastAppointDate { get; set; }
        public int FollowUpId { get; set; }
        public string FollowUpsVisitReason { get; set; }
        public DateTime FollowUpDate { get; set; }
        public int[] PatientIds { get; set; }

        public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IGroupPatientAppointmentRepository _groupPatientAppointmentRepository;
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;
            private readonly IFollowUpAppointmentRepository _followUpAppointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IGroupPatientAppointmentRepository groupPatientAppointmentRepository, IRecurringAppointmentsRepository recurringAppointmentsRepository, IFollowUpAppointmentRepository followUpAppointmentRepository, IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = appointmentRepository;
                _groupPatientAppointmentRepository = groupPatientAppointmentRepository;
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
                _followUpAppointmentRepository = followUpAppointmentRepository;
                _contextAccessor = contextAccessor;
            }

           // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Appointment appointmentObj = new Appointment
                {
                    AppointmentId = request.AppointmentId,
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
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                };

                _appointmentRepository.Update(appointmentObj);
                await _appointmentRepository.SaveChangesAsync();

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
                else if (request.IsRecurringAppointment)
                {
                    var existingRecurringAppointment = await _recurringAppointmentsRepository.GetAsync(x => x.RecurringAppointmentId == request.RecurringAppointmentId);

                    if (existingRecurringAppointment != null)
                    {
                        existingRecurringAppointment.RecurringAppointmentId = request.RecurringAppointmentId;
                        existingRecurringAppointment.Weekdays = request.Weekdays;
                        existingRecurringAppointment.RecurEvery = request.RecurEvery;
                        existingRecurringAppointment.WeekType = request.WeekType;
                        existingRecurringAppointment.RecurringVisitReason = request.RecurringVisitReason;
                        existingRecurringAppointment.FirstAppointDate = request.FirstAppointDate;
                        existingRecurringAppointment.LastAppointDate = request.LastAppointDate;
                        existingRecurringAppointment.AppointmentId = appointmentObj.AppointmentId;
                        _recurringAppointmentsRepository.Update(existingRecurringAppointment);
                        await _recurringAppointmentsRepository.SaveChangesAsync();
                    }

                }
                else if (request.IsFollowUpAppointment)
                {
                  var existingFollowUpAppointment= await _followUpAppointmentRepository.GetAsync(x => x.FollowUpId == request.FollowUpId);
                    if(existingFollowUpAppointment != null)
                    {
                        existingFollowUpAppointment.FollowUpId = request.FollowUpId;
                        existingFollowUpAppointment.FollowUpsVisitReason = request.FollowUpsVisitReason;
                        existingFollowUpAppointment.FollowUpDate = request.FollowUpDate;
                        existingFollowUpAppointment.AppointmentId = appointmentObj.AppointmentId;
                    };
                    _followUpAppointmentRepository.Update(existingFollowUpAppointment);
                    await _followUpAppointmentRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
