using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReminderProfileRepository;
using Entities.Concrete.ReminderProfileEntity;
using Entities.Dtos.ReminderProfileDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReminderProfiles.Queries
{
    public class GetReminderProfileQuery : BasePaginationQuery<IDataResult<IEnumerable<ReminderProfileDto>>>
    {
        public int ReminderProfileId { get; set; }
        public class GetReminderProfileQueryHandler : IRequestHandler<GetReminderProfileQuery, IDataResult<IEnumerable<ReminderProfileDto>>>
        {
            private readonly IReminderProfileRepository _reminderProfileRepository;
            public GetReminderProfileQueryHandler(IReminderProfileRepository reminderProfileRepository)
            {
                _reminderProfileRepository = reminderProfileRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ReminderProfileDto>>> Handle(GetReminderProfileQuery request, CancellationToken cancellationToken)
            {
                var list = await _reminderProfileRepository.GetAllReminderProfile();
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ReminderProfileDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
