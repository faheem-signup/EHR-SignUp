using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using Entities.Dtos.RecurringAppointmentsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RecurringAppointments.Queries
{
    public class GetRecurringAppointmentsQuery : BasePaginationQuery<IDataResult<IEnumerable<RecurringAppointmentsDto>>>
    {
        public string PatientName { get; set; }

        public class GetRecurringAppointmentsQueryHandler : IRequestHandler<GetRecurringAppointmentsQuery, IDataResult<IEnumerable<RecurringAppointmentsDto>>>
        {
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;

            public GetRecurringAppointmentsQueryHandler(IRecurringAppointmentsRepository recurringAppointmentsRepository)
            {
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<RecurringAppointmentsDto>>> Handle(GetRecurringAppointmentsQuery request, CancellationToken cancellationToken)
            {
                var list = await _recurringAppointmentsRepository.GetRecurringAppointments(request.PatientName);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<RecurringAppointmentsDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
