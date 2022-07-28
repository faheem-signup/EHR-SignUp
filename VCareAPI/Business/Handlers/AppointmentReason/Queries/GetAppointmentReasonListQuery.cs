using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentReasonsRepository;
using Entities.Concrete;
using Entities.Concrete.AppointmentReasonsEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentReason.Queries
{
    public class GetAppointmentReasonQuery : BasePaginationQuery<IDataResult<IEnumerable<AppointmentReasons>>>
    {
        
        public class GetAppointmentReasonQueryHandler : IRequestHandler<GetAppointmentReasonQuery, IDataResult<IEnumerable<AppointmentReasons>>>
        {
            private readonly IAppointmentReasonsRepository _appointmentReasonsRepository;
            public GetAppointmentReasonQueryHandler(IAppointmentReasonsRepository appointmentReasonsRepository)
            {
                _appointmentReasonsRepository = appointmentReasonsRepository;
            }

            //[SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            //[CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<AppointmentReasons>>> Handle(GetAppointmentReasonQuery request, CancellationToken cancellationToken)
            {
                var list = await _appointmentReasonsRepository.GetListAsync(x=> x.IsDeleted != true);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<AppointmentReasons>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
