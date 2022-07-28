using AutoMapper;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.AppointmentDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Appointments.Queries
{
    public class GetProviderAvailableSlotsQuery : IRequest<IDataResult<string>>
    {
        public int ProviderId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? FirstApptAppointmentDate { get; set; }
        public DateTime? LastApptAppointmentDate { get; set; }
        public string WeekDays { get; set; }
        public bool? AllowGroupPatient { get; set; }
        public bool? IsRecurringAppointment { get; set; }
        public bool? IsFollowUpAppointment { get; set; }
        public class GetProviderAvailableSlotsQueryHandler : IRequestHandler<GetProviderAvailableSlotsQuery, IDataResult<string>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMapper _mapper;
            public GetProviderAvailableSlotsQueryHandler(IProviderWorkConfigRepository providerWorkConfigRepository,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                IAppointmentRepository appointmentRepository,
                ISchedulerRepository schedulerRepository)
            {
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _mapper = mapper;
                _appointmentRepository = appointmentRepository;
                _contextAccessor = contextAccessor;
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<string>> Handle(GetProviderAvailableSlotsQuery request, CancellationToken cancellationToken)
            {
                List<Tuple<DateTime, DateTime, AppointmentByDateRangeDto>> slotTimeList1 = new List<Tuple<DateTime, DateTime, AppointmentByDateRangeDto>>();

                List<AppointmentByDateRangeDto> appointmentObj = new List<AppointmentByDateRangeDto>();
                if ((bool)request.AllowGroupPatient)
                {
                    appointmentObj = await _appointmentRepository.GetAppointmentByDateRange(request.ProviderId, request.AppointmentDate, request.AppointmentDate, (bool)request.AllowGroupPatient);
                }
                else if ((bool)request.IsRecurringAppointment)
                {
                    appointmentObj = await _appointmentRepository.GetAppointmentByDateRange(request.ProviderId, (DateTime)request.FirstApptAppointmentDate, (DateTime)request.LastApptAppointmentDate, (bool)request.IsRecurringAppointment);
                }
                else if ((bool)request.IsFollowUpAppointment)
                {
                    appointmentObj = await _appointmentRepository.GetAppointmentByDateRange(request.ProviderId, request.AppointmentDate, request.AppointmentDate, (bool)request.IsFollowUpAppointment);
                }

                var startDate = request.AppointmentDate.Date;
                var endDate = request.AppointmentDate.AddDays(1);

                //if (request.FromDate != null && request.ToDate != null)
                //{
                //    startDate = request.FromDate;
                //    endDate = request.ToDate.AddDays(1);
                //}

                var currentFromDateTime = startDate;
                var currentToDateTime = startDate;

                currentFromDateTime = currentFromDateTime.AddHours(request.FromDate.Hour).AddMinutes(request.FromDate.Minute);
                currentToDateTime = currentToDateTime.AddHours(request.ToDate.Hour).AddMinutes(request.ToDate.Minute);
                var isSlotExist = false;
                appointmentObj.ToList().ForEach(x =>
                {
                    var appointmentDateTime = x.AppointmentDate.Date;
                    if (currentFromDateTime == appointmentDateTime.AddHours(x.TimeFrom.Hour).AddMinutes(x.TimeFrom.Minute))
                    {
                        isSlotExist = true;
                    }
                });

                if (isSlotExist)
                {
                    return new SuccessDataResult<string>("Already slots created");
                }

                var currentDate = startDate.Date;
                var currentScheduleEndDate = startDate.Date;

                var scheduleWeeklyDetails = await _providerWorkConfigRepository.GetProviderWorkConfigListById(request.ProviderId);
                var slotResult = "";
                while (startDate.Date != endDate.Date)
                {
                    // Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5,Saturday = 6

                    currentDate = startDate.Date;
                    currentScheduleEndDate = startDate.Date;

                    var daySchedules = scheduleWeeklyDetails.Where(schedule => schedule.Days == startDate.Date.DayOfWeek.ToString());

                    if (daySchedules != null && daySchedules.Any())
                    {
                        foreach (var daySchedule in daySchedules)
                        {
                            currentDate = currentDate.AddHours(daySchedule.FirstShiftWorkFrom.Hour).AddMinutes(daySchedule.FirstShiftWorkFrom.Minute);

                            currentScheduleEndDate = currentScheduleEndDate.AddHours(daySchedule.FirstShiftWorkTo.Hour).AddMinutes(daySchedule.FirstShiftWorkTo.Minute);

                            if (currentScheduleEndDate < currentDate)
                            {
                                currentScheduleEndDate = currentScheduleEndDate.AddDays(1);
                            }

                            slotResult = CheckAvailableSlots(currentDate, currentScheduleEndDate, (int)daySchedule.SlotSize.TotalMinutes, currentFromDateTime, currentToDateTime);

                            DateTime tm = DateTime.Now.Date;
                            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                            currentScheduleEndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                        }
                    }

                    startDate = startDate.AddDays(1);
                }

                return new SuccessDataResult<string>(slotResult);
            }

            public string CheckAvailableSlots(DateTime startDatetime, DateTime endDatetime, int slotSizeInMinute, DateTime appointmetnStartDatetime, DateTime appointmentEndDatetime)
            {
                DateTime slottime;
                if (appointmetnStartDatetime != null && appointmentEndDatetime != null)
                {
                    var isSlotAvailable = false;
                    while ((slottime = startDatetime.AddMinutes(slotSizeInMinute)) <= endDatetime.AddMilliseconds(slottime.Millisecond))
                    {
                        if (appointmetnStartDatetime.AddMilliseconds(-appointmetnStartDatetime.Millisecond) == startDatetime.AddMilliseconds(-startDatetime.Millisecond))
                        {
                            isSlotAvailable = false;
                            return "Slot is available";

                            startDatetime = slottime.AddMilliseconds(1);
                            break;
                        }
                        else
                        {
                            isSlotAvailable = true;
                        }

                        startDatetime = slottime.AddMilliseconds(1);
                    }
                    if(isSlotAvailable)
                    {
                        return "There is no slot";
                    }
                }

                return "No record found";
            }
        }
    }
}
