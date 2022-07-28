using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract.IReminderProfileRepository;
using Entities.Concrete.ReminderProfileEntity;
using Entities.Dtos.ReminderProfileDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReminderProfiles.Queries
{
    public class GetReminderProfileByIdQuery : IRequest<IDataResult<GetReminderProfileByIdDto>>
    {
        public int ReminderProfileId { get; set; }

        public class GetReminderProfileByIdQueryHandler : IRequestHandler<GetReminderProfileByIdQuery, IDataResult<GetReminderProfileByIdDto>>
        {
            private readonly IReminderProfileRepository _reminderProfileRepository;
            public GetReminderProfileByIdQueryHandler(IReminderProfileRepository reminderProfileRepository)
            {
                _reminderProfileRepository = reminderProfileRepository;
            }

            public async Task<IDataResult<GetReminderProfileByIdDto>> Handle(GetReminderProfileByIdQuery request, CancellationToken cancellationToken)
            {
                var reminder = await _reminderProfileRepository.GetReminderProfileById(request.ReminderProfileId);

                return new SuccessDataResult<GetReminderProfileByIdDto>(reminder);
            }
        }
    }
}
