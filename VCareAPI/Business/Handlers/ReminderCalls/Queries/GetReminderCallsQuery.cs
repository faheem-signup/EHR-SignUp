using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Abstract.IReminderCallsRepository;
using Entities.Concrete.PatientEntity;
using Entities.Dtos.PatientDto;
using Entities.Dtos.ReminderCallDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Business.Handlers.ReminderCalls.Queries
{
    public class GetReminderCallsQuery : BasePaginationQuery<IDataResult<IEnumerable<ReminderCallDto>>>
    {
        public int? PatientId { get; set; }
        public DateTime? AppointmentFrom { get; set; }
        public DateTime? AppointmentTo { get; set; }
        public int? ReminderStatusId { get; set; }
        public class GetReminderCallsQueryHandler : IRequestHandler<GetReminderCallsQuery, IDataResult<IEnumerable<ReminderCallDto>>>
        {
            private readonly IReminderCallsRepository _reminderCallsRepository;
            public GetReminderCallsQueryHandler(IReminderCallsRepository reminderCallsRepository)
            {
                _reminderCallsRepository = reminderCallsRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ReminderCallDto>>> Handle(GetReminderCallsQuery request, CancellationToken cancellationToken)
            {
                var list = await _reminderCallsRepository.GetReminderCallsById(request.PatientId, request.AppointmentFrom, request.AppointmentTo, request.ReminderStatusId);

                return new PagedDataResult<IEnumerable<ReminderCallDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
