using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.AppointmentDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Queries
{
    public class GetAppointmentListForAdvanceSearchQuery : BasePaginationQuery<IDataResult<IEnumerable<AppointmentScheduleDto>>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public int? ProviderId { get; set; }
        public int? ServiceProfileId { get; set; }
        public int? LocationId { get; set; }
        public int? InsuranceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public class GetAppointmentListForAdvanceSearchQueryHandler : IRequestHandler<GetAppointmentListForAdvanceSearchQuery, IDataResult<IEnumerable<AppointmentScheduleDto>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;

            public GetAppointmentListForAdvanceSearchQueryHandler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AppointmentScheduleDto>>> Handle(GetAppointmentListForAdvanceSearchQuery request, CancellationToken cancellationToken)
            {
                var appointmentList = await _appointmentRepository.GetAllAppointments(request.FromDate, request.ToDate);

                if (appointmentList.Count() > 0)
                {

                    appointmentList = appointmentList.OrderByDescending(x => x.AppointmentDate).ToList();
                    appointmentList = appointmentList.Where(x => x.AppointmentDate.Date >= request.FromDate.Date && x.AppointmentDate.Date <= request.ToDate.Date).ToList();

                    if (request.ProviderId != 0 && request.ProviderId != null)
                    {
                        appointmentList = appointmentList.Where(x => x.ProviderId == request.ProviderId).ToList();
                    }

                    if (request.LocationId != 0 && request.LocationId != null)
                    {
                        appointmentList = appointmentList.Where(x => x.LocationId == request.LocationId).ToList();
                    }

                    if (request.ServiceProfileId != 0 && request.ServiceProfileId != null)
                    {
                        appointmentList = appointmentList.Where(x => x.ServiceProfileId == request.ServiceProfileId).ToList();
                    }
                }

                var pagedData = Paginate(appointmentList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<AppointmentScheduleDto>>(pagedData, appointmentList.Count(), request.PageNumber);
            }
        }
    }
}
