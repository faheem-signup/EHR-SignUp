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
    public class GetAppointmentAndProviderSlotsDetailQuery : IRequest<IDataResult<AppointmentSlotsDetailDto>>
    {
        public int ProviderId { get; set; }
        public int LocationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public class GetAppointmentAndProviderSlotsDetailQueryHandler : IRequestHandler<GetAppointmentAndProviderSlotsDetailQuery, IDataResult<AppointmentSlotsDetailDto>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMapper _mapper;
            public GetAppointmentAndProviderSlotsDetailQueryHandler(IProviderWorkConfigRepository providerWorkConfigRepository,
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
            public async Task<IDataResult<AppointmentSlotsDetailDto>> Handle(GetAppointmentAndProviderSlotsDetailQuery request, CancellationToken cancellationToken)
            {
                AppointmentSlotsDetailDto appointmentSlotsDetailObj = new AppointmentSlotsDetailDto();
                List<AppointmentByDateRangeDto> appointmentListObj = new List<AppointmentByDateRangeDto>();

                var appointmentDetailList = await _appointmentRepository.GetAppointmentByDateRangeAndProviderId(request.ProviderId, request.AppointmentDate, request.AppointmentDate, request.LocationId);

                var isSlotExist = false;
                if (appointmentDetailList.Count > 0)
                {
                    appointmentSlotsDetailObj.BookedSlotsDetail = appointmentDetailList;
                    //appointmentDetailList.ToList().ForEach(x =>
                    //{
                    //    var appointmentDateTime = x.AppointmentDate.Date;
                    //    if (currentFromDateTime == appointmentDateTime.AddHours(x.TimeFrom.Hour).AddMinutes(x.TimeFrom.Minute))
                    //    {
                    //        appointmentListObj.Add(x);
                    //        // isSlotExist = true;
                    //    }
                    //});
                }
                else
                {
                    appointmentSlotsDetailObj.BookedSlotsDetail = appointmentListObj;
                }

                var scheduleWeeklyDetails = await _providerWorkConfigRepository.GetProviderWorkConfigListDetail(request.ProviderId, request.LocationId);
                var daySchedules = scheduleWeeklyDetails.Where(schedule => schedule.Days == request.AppointmentDate.Date.DayOfWeek.ToString());
                if (daySchedules != null && daySchedules.Any())
                {
                    appointmentSlotsDetailObj.ProviderWorkConfigDetail = daySchedules.FirstOrDefault();
                    appointmentSlotsDetailObj.ProviderWorkConfigDetail.AppointmentDate = request.AppointmentDate;
                }

                return new SuccessDataResult<AppointmentSlotsDetailDto>(appointmentSlotsDetailObj);
            }
        }
    }
}
