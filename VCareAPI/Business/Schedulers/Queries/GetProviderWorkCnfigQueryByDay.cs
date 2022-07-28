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
    public class GetProviderWorkCnfigQueryByDay : BasePaginationQuery<IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>> //// IRequest<IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>>
    {
        public int? ProviderId { get; set; }
        public string ProviderType { get; set; }
        public int? LocationId { get; set; }
        public string DateType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string AppointmentStatus { get; set; }

        public class GetProviderWorkCnfigQueryByDayHandler : IRequestHandler<GetProviderWorkCnfigQueryByDay, IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMapper _mapper;

            public GetProviderWorkCnfigQueryByDayHandler(IProviderWorkConfigRepository providerWorkConfigRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IAppointmentRepository appointmentRepository, ISchedulerRepository schedulerRepository)
            {
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _mapper = mapper;
                _appointmentRepository = appointmentRepository;
                _contextAccessor = contextAccessor;
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>> Handle(GetProviderWorkCnfigQueryByDay request, CancellationToken cancellationToken)
            {
                var loginUserId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var roleData = await _schedulerRepository.GetRoleByUserId(Convert.ToInt32(loginUserId));

                List<Tuple<DateTime, DateTime, AppointmentScheduleDto>> slotTimeList1 = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();
                if (roleData.RoleName == "Admin" || roleData.RoleName == "Scheduler")
                {
                    var providerList = await _schedulerRepository.GetProviderIDs(request.ProviderId);
                    if (providerList.Count() > 0)
                    {
                        foreach (var item in providerList)
                        {

                            var startDate = DateTime.Now;
                            var endDate = DateTime.Now.Date;//.AddDays(1);

                            if (request.DateType == "Daily")
                            {
                                startDate = DateTime.Now;
                                endDate = DateTime.Now.Date;//.AddDays(1);
                            }
                            else if (request.DateType == "Weekly")
                            {
                                var dayCount = (int)DateTime.Now.DayOfWeek;
                                startDate = DateTime.Now;
                                if (dayCount == 0)
                                {
                                    startDate = DateTime.Now.AddDays(-6);
                                    endDate = startDate.AddDays(6);
                                }
                                else
                                {
                                    startDate = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek);
                                    endDate = startDate.AddDays(6);
                                }
                            }
                            else if (request.DateType == "Monthly")
                            {
                                var d = DateTime.Now.Day;
                                startDate = DateTime.Now.AddDays(1 - (int)DateTime.Now.Day);
                                endDate = startDate.AddMonths(1).AddDays(-1);
                            }
                            else if (request.FromDate != null && request.ToDate != null)
                            {
                                startDate = (DateTime)request.FromDate;
                                endDate = (DateTime)request.ToDate;//?.AddDays(1);
                            }

                            // var currentDate = startDate.Date;
                            //var currentScheduleEndDate = startDate.Date;

                            List<AppointmentScheduleDto> appointmentDetaillst = new List<AppointmentScheduleDto>();

                            // var appointmentList = await _appointmentRepository.GetAppointmentScheduleList(item.ProviderID);
                            var appointmentList = await _appointmentRepository.GetAppointmentScheduleListByDateRangeAndProviderId(item.ProviderID, startDate.Date, endDate.Date);
                            if (appointmentList != null && appointmentList.Count > 0)
                            {
                                if (request.LocationId != null && request.LocationId != 0)
                                {
                                    appointmentList = appointmentList.Where(x => x.LocationId == request.LocationId).ToList();
                                }

                                if (!string.IsNullOrEmpty(request.AppointmentStatus))
                                {
                                    if (request.AppointmentStatus != "All")
                                    {
                                        appointmentList = appointmentList.Where(x => x.AppointmentStatusName == request.AppointmentStatus).ToList();
                                    }
                                }

                                if (!string.IsNullOrEmpty(request.ProviderType))
                                {
                                    appointmentList = appointmentList.Where(x => x.Degree == request.ProviderType).ToList();
                                }

                                appointmentDetaillst = appointmentList.Select(x => _mapper.Map<AppointmentScheduleDto>(x)).ToList();

                                //// new work Start ///
                                var listofRows = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();

                                foreach (var aptItem in appointmentDetaillst)
                                {
                                    var appointmentStartDate = aptItem.AppointmentDate.Date;
                                    if (aptItem.TimeFrom != null && aptItem.TimeTo != null)
                                    {
                                        if ((bool)aptItem.AllowGroupPatient)
                                        {
                                            aptItem.GroupPatientAppointmentList = _appointmentRepository.GetGroupPatientAppointmentList(aptItem.AppointmentId).Result;
                                        }

                                        listofRows.Add(Tuple.Create((DateTime)aptItem.TimeFrom, (DateTime)aptItem.TimeTo, aptItem));
                                    }
                                }

                                slotTimeList1.AddRange(listofRows);

                                //// new work end ///
                            }

                            // var scheduleWeeklyDetails = await _providerWorkConfigRepository.GetProviderWorkConfigListById(item.ProviderID);

                            //while (startDate.Date != endDate.Date)
                            //{
                            //    // Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5,Saturday = 6

                            //    currentDate = startDate.Date;
                            //    currentScheduleEndDate = startDate.Date;

                            //    var daySchedules = scheduleWeeklyDetails.Where(schedule => schedule.Days == startDate.Date.DayOfWeek.ToString());

                            //    if (daySchedules != null && daySchedules.Any())
                            //    {
                            //        foreach (var daySchedule in daySchedules)
                            //        {
                            //            currentDate = currentDate.AddHours(daySchedule.FirstShiftWorkFrom.Hour).AddMinutes(daySchedule.FirstShiftWorkFrom.Minute);

                            //            currentScheduleEndDate = currentScheduleEndDate.AddHours(daySchedule.FirstShiftWorkTo.Hour).AddMinutes(daySchedule.FirstShiftWorkTo.Minute);

                            //            if (currentScheduleEndDate < currentDate)
                            //            {
                            //                currentScheduleEndDate = currentScheduleEndDate.AddDays(1);
                            //            }

                            //            var slotTimeList = SplitDateRange(currentDate, currentScheduleEndDate, (int)daySchedule.SlotSize.TotalMinutes, appointmentDetaillst);

                            //            slotTimeList1.AddRange(slotTimeList);

                            //            DateTime tm = new DateTime();
                            //            tm = DateTime.Now.Date;
                            //            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                            //            currentScheduleEndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                            //        }
                            //    }

                            //    startDate = startDate.AddDays(1);
                            //}
                        }
                    }
                }
                else
                {
                    var startDate = DateTime.Now;
                    var endDate = DateTime.Now.Date;//.AddDays(1);

                    if (request.DateType == "Daily")
                    {
                        startDate = DateTime.Now;
                        endDate = DateTime.Now.Date;//.AddDays(1);
                    }
                    else if (request.DateType == "Weekly")
                    {
                        var dayCount = (int)DateTime.Now.DayOfWeek;
                        startDate = DateTime.Now;
                        if (dayCount == 0)
                        {
                            startDate = DateTime.Now.AddDays(-6);
                            endDate = startDate.AddDays(6);
                        }
                        else
                        {
                            startDate = DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek);
                            endDate = startDate.AddDays(6);
                        }
                    }
                    else if (request.DateType == "Monthly")
                    {
                        var d = DateTime.Now.Day;
                        startDate = DateTime.Now.AddDays(1 - (int)DateTime.Now.Day);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                    }
                    else if (request.FromDate != null && request.ToDate != null)
                    {
                        startDate = (DateTime)request.FromDate;
                        endDate = (DateTime)request.ToDate;//?.AddDays(1);
                    }

                    //var currentDate = startDate.Date;
                    //var currentScheduleEndDate = startDate.Date;

                    //var WorkConfigobj = await _providerWorkConfigRepository.GetProviderWorkConfigListById(request.ProviderId);

                    //   var scheduleWeeklyDetails = await _providerWorkConfigRepository.GetProviderWorkConfigListById((int)request.ProviderId);
                    //var appointmentList = await _appointmentRepository.GetAppointmentScheduleList((int)request.ProviderId);

                    List<AppointmentScheduleDto> appointmentDetaillst = new List<AppointmentScheduleDto>();
                    var appointmentList = await _appointmentRepository.GetAppointmentScheduleListByDateRangeAndProviderId((int)request.ProviderId, startDate.Date, endDate.Date);
                    if (appointmentList != null && appointmentList.Count > 0)
                    {
                        if (request.LocationId != null && request.LocationId != 0)
                        {
                            appointmentList = appointmentList.Where(x => x.LocationId == request.LocationId).ToList();
                        }

                        if (!string.IsNullOrEmpty(request.AppointmentStatus))
                        {
                            if (request.AppointmentStatus != "All")
                            {
                                appointmentList = appointmentList.Where(x => x.AppointmentStatusName == request.AppointmentStatus).ToList();
                            }
                        }

                        if (!string.IsNullOrEmpty(request.ProviderType))
                        {
                            appointmentList = appointmentList.Where(x => x.Degree == request.ProviderType).ToList();
                        }

                        appointmentDetaillst = appointmentList.Select(x => _mapper.Map<AppointmentScheduleDto>(x)).ToList();

                        //// new work Start ///

                        var listofRows = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();

                        foreach (var item in appointmentDetaillst)
                        {
                            var appointmentStartDate = item.AppointmentDate.Date;
                            if (item.TimeFrom != null && item.TimeTo != null)
                            {
                                if ((bool)item.AllowGroupPatient)
                                {
                                    item.GroupPatientAppointmentList = _appointmentRepository.GetGroupPatientAppointmentList(item.AppointmentId).Result;
                                }

                                listofRows.Add(Tuple.Create((DateTime)item.TimeFrom, (DateTime)item.TimeTo, item));
                            }
                        }

                        slotTimeList1.AddRange(listofRows);

                        //// new work end ///
                    }

                    // List<Tuple<DateTime, DateTime, AppointmentScheduleDto>> slotTimeList1 = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();

                    // while (startDate.Date != endDate.Date)
                    // {
                    //    // Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5,Saturday = 6

                    // currentDate = startDate.Date;
                    //    currentScheduleEndDate = startDate.Date;

                    // var daySchedules = scheduleWeeklyDetails.Where(schedule => schedule.Days == startDate.Date.DayOfWeek.ToString());
                    //    if (daySchedules != null && daySchedules.Any())
                    //    {
                    //        foreach (var daySchedule in daySchedules)
                    //        {
                    //            currentDate = currentDate.AddHours(daySchedule.FirstShiftWorkFrom.Hour).AddMinutes(daySchedule.FirstShiftWorkFrom.Minute);

                    // currentScheduleEndDate = currentScheduleEndDate.AddHours(daySchedule.FirstShiftWorkTo.Hour).AddMinutes(daySchedule.FirstShiftWorkTo.Minute);

                    // if (currentScheduleEndDate < currentDate)
                    //            {
                    //                currentScheduleEndDate = currentScheduleEndDate.AddDays(1);
                    //            }

                    // var slotTimeList = SplitDateRange(currentDate, currentScheduleEndDate, (int)daySchedule.SlotSize.TotalMinutes, appointmentDetaillst);

                    // slotTimeList1.AddRange(slotTimeList);

                    // DateTime tm = new DateTime();
                    //            tm = DateTime.Now.Date;
                    //            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                    //            currentScheduleEndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, tm.Hour, 0, 0);
                    //        }
                    //    }
                    //    startDate = startDate.AddDays(1);
                    // }
                    // WorkConfigobj.ToList().ForEach(x =>
                    // {
                    //    var slotTimeList = SplitDateRange(x.FirstShiftWorkFrom, x.FirstShiftWorkTo, (int)x.SlotSize.TotalMinutes);
                    //    slotTimeList1.AddRange(slotTimeList);
                    //});
                }

                if (slotTimeList1.Count() > 0)
                {
                    slotTimeList1 = slotTimeList1.OrderByDescending(x => x.Item3.AppointmentDate).ToList();
                }

                var pagedData = Paginate(slotTimeList1, request.PageNumber, request.PageSize);

                return new PagedDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>(pagedData, slotTimeList1.Count(), request.PageNumber);

                // return new SuccessDataResult<IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>>>(slotTimeList1);
            }

            //public IEnumerable<Tuple<DateTime, DateTime, AppointmentScheduleDto>> SplitDateRange(DateTime startDatetime, DateTime endDatetime, int slotSizeInMinute, IEnumerable<AppointmentScheduleDto> appointmentDetaillst)
            //{
            //    DateTime slottime;
            //    var listofRows = new List<Tuple<DateTime, DateTime, AppointmentScheduleDto>>();
            //    while ((slottime = startDatetime.AddMinutes(slotSizeInMinute)) <= (endDatetime.AddMilliseconds(slottime.Millisecond)))
            //    {
            //        //    var isAppointment = false;
            //        //    Appointment apt = new Appointment();

            //        if (appointmentDetaillst.Count() > 0)
            //        {
            //            foreach (var item in appointmentDetaillst)
            //            {
            //                var appointmentStartDate = item.AppointmentDate.Date;
            //                if (item.TimeFrom != null && item.TimeTo != null)
            //                {
            //                    var appointmentStartDateTime = appointmentStartDate.AddHours((double)item.TimeFrom?.Hour).AddMinutes((double)item.TimeFrom?.Minute);
            //                    //   startDatetime = startDatetime.AddMilliseconds(-startDatetime.Millisecond);
            //                    /// appointmentStartDateTime = appointmentStartDateTime.AddMilliseconds(-appointmentStartDateTime.Millisecond);
            //                    if (appointmentStartDateTime.AddMilliseconds(-appointmentStartDateTime.Millisecond) == startDatetime.AddMilliseconds(-startDatetime.Millisecond))
            //                    {
            //                        // isAppointment = true;
            //                        // apt = item;
            //                        if ((bool)item.AllowGroupPatient)
            //                        {
            //                            item.GroupPatientAppointmentList = _appointmentRepository.GetGroupPatientAppointmentList(item.AppointmentId).Result;
            //                        }

            //                        listofRows.Add(Tuple.Create(startDatetime, slottime, item));
            //                        startDatetime = slottime.AddMilliseconds(1);
            //                        break;
            //                    }
            //                    //else
            //                    //{
            //                    //    startDatetime = slottime.AddMilliseconds(1);
            //                    //    //isAppointment = false;
            //                    //}
            //                }

            //            }
            //        }
            //        startDatetime = slottime.AddMilliseconds(1);
            //        //if (isAppointment)
            //        //{
            //        //    listofRows.Add(Tuple.Create(startDatetime, slottime, apt));
            //        //    startDatetime = slottime.AddMilliseconds(1);
            //        //}
            //        //else
            //        //{
            //        //    listofRows.Add(Tuple.Create(startDatetime, slottime, apt));
            //        //    startDatetime = slottime.AddMilliseconds(1);
            //        //}


            //        //Appointment apt = new Appointment();
            //        //listofRows.Add(Tuple.Create(startDatetime, slottime,apt));
            //        //startDatetime = slottime.AddMilliseconds(1);
            //    }

            //    return listofRows.AsEnumerable();
            //}
        }
    }
}
