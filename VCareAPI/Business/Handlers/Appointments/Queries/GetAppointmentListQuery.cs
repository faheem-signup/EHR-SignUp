using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using Entities.Concrete.AppointmentEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Queries
{
    public class GetAppointmentListQuery : BasePaginationQuery<IDataResult<IEnumerable<Appointment>>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public class GetAppointmentListQueryHandler : IRequestHandler<GetAppointmentListQuery, IDataResult<IEnumerable<Appointment>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            public GetAppointmentListQueryHandler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

          //  [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
           // [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<Appointment>>> Handle(GetAppointmentListQuery request, CancellationToken cancellationToken)
            {
                var appointmentList = await _appointmentRepository.GetListAsync();
                var pagedData = Paginate(appointmentList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Appointment>>(pagedData, appointmentList.Count(), request.PageNumber);
               // return new SuccessDataResult<IEnumerable<Appointment>>(appointmentList.ToList());
            }
        }
    }
}
