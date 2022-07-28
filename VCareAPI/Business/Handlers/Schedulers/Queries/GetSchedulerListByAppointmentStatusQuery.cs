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

namespace Business.Handlers.Schedulers.Queries
{
    public class GetSchedulerListByAppointmentStatusQuery : BasePaginationQuery<IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>>
    {
        public int? ProviderId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string AppointmentStatus { get; set; }

        public class GetSchedulerListByAppointmentStatusQueryHandler : IRequestHandler<GetSchedulerListByAppointmentStatusQuery, IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMapper _mapper;

            public GetSchedulerListByAppointmentStatusQueryHandler(IProviderWorkConfigRepository providerWorkConfigRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IAppointmentRepository appointmentRepository, ISchedulerRepository schedulerRepository)
            {
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _mapper = mapper;
                _appointmentRepository = appointmentRepository;
                _contextAccessor = contextAccessor;
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>> Handle(GetSchedulerListByAppointmentStatusQuery request, CancellationToken cancellationToken)
            {
                var loginUserId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var roleData = await _schedulerRepository.GetRoleByUserId(Convert.ToInt32(loginUserId));
                List<Tuple<DateTime, DateTime, AppointmentScheduleDto>> slotTimeList1 = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();
                var startDate = DateTime.Now;
                var endDate = DateTime.Now.Date.AddDays(1);

                if (request.FromDate != null && request.ToDate != null)
                {
                    startDate = (DateTime)request.FromDate;
                    endDate = (DateTime)request.ToDate?.AddDays(1);
                }

                var currentDate = startDate.Date;
                var currentScheduleEndDate = startDate.Date;

                var scheduleWeeklyDetails = await _providerWorkConfigRepository.GetProviderWorkConfigListById((int)request.ProviderId);
                var appointmentList = await _appointmentRepository.GetAppointmentScheduleList((int)request.ProviderId);
                if (!string.IsNullOrEmpty(request.AppointmentStatus))
                {
                    appointmentList = appointmentList.Where(x => x.AppointmentStatusName == request.AppointmentStatus).ToList();
                }

                var appointmentDetaillst = appointmentList.Select(x => _mapper.Map<AppointmentScheduleDto>(x)).ToList();

                while (startDate.Date != endDate.Date)
                {
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

                            var slotTimeList = SplitDateRange(currentDate, currentScheduleEndDate, (int)daySchedule.SlotSize.TotalMinutes, appointmentDetaillst);

                            slotTimeList1.AddRange(slotTimeList);

                            DateTime tm = new DateTime();
                            tm = DateTime.Now.Date;
                            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                            currentScheduleEndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                        }
                    }

                    startDate = startDate.AddDays(1);
                }

                if (slotTimeList1.Count() > 0)
                {
                    slotTimeList1 = slotTimeList1.OrderByDescending(x => x.Item3.AppointmentDate).ToList();
                }

                var pagedData = Paginate(slotTimeList1, request.PageNumber, request.PageSize);

                return new PagedDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>(pagedData, slotTimeList1.Count(), request.PageNumber);
            }

            public IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>> SplitDateRange(DateTime startDatetime, DateTime endDatetime, int slotSizeInMinute, IEnumerable<AppointmentScheduleDto> appointmentDetaillst)
            {
                DateTime slottime;
                var listofRows = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();
                while ((slottime = startDatetime.AddMinutes(slotSizeInMinute)) <= (endDatetime.AddMilliseconds(slottime.Millisecond)))
                {
                    if (appointmentDetaillst.Count() > 0)
                    {
                        foreach (var item in appointmentDetaillst)
                        {
                            var appointmentStartDate = item.AppointmentDate.Date;
                            if (item.TimeFrom != null || item.TimeFrom != null)
                            {
                                var appointmentStartDateTime = appointmentStartDate.AddHours((double)item.TimeFrom?.Hour).AddMinutes((double)item.TimeFrom?.Minute);
                                if (appointmentStartDateTime.AddMilliseconds(-appointmentStartDateTime.Millisecond) == startDatetime.AddMilliseconds(-startDatetime.Millisecond))
                                {
                                    if ((bool)item.AllowGroupPatient)
                                    {
                                        item.GroupPatientAppointmentList = _appointmentRepository.GetGroupPatientAppointmentList(item.AppointmentId).Result;
                                    }

                                    listofRows.Add(Tuple.Create(startDatetime, slottime, item));
                                    startDatetime = slottime.AddMilliseconds(1);
                                    break;
                                }
                            }
                        }
                    }

                    startDatetime = slottime.AddMilliseconds(1);
                }

                return listofRows.AsEnumerable();
            }
        }
    }
}
