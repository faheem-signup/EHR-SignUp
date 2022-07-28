using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReminderProfileRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReminderProfiles.Commands
{
    public class DeleteReminderProfileCommand : IRequest<IResult>
    {
        public int ReminderProfileId { get; set; }

        public class DeleteReminderProfileCommandHandler : IRequestHandler<DeleteReminderProfileCommand, IResult>
        {
            private readonly IReminderProfileRepository _reminderProfileRepository;

            public DeleteReminderProfileCommandHandler(IReminderProfileRepository reminderProfileRepository)
            {
                _reminderProfileRepository = reminderProfileRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteReminderProfileCommand request, CancellationToken cancellationToken)
            {
                var reminderProfileToDelete = await _reminderProfileRepository.GetAsync(x => x.ReminderProfileId == request.ReminderProfileId && x.IsDeleted != true);
                if (reminderProfileToDelete != null)
                {
                    reminderProfileToDelete.IsDeleted = true;
                    _reminderProfileRepository.Update(reminderProfileToDelete);
                    await _reminderProfileRepository.SaveChangesAsync();

                    return new SuccessResult(Messages.Deleted);
                }
                else
                {
                    return new SuccessResult(Messages.NoRecordFound);
                }
            }
        }
    }
}
