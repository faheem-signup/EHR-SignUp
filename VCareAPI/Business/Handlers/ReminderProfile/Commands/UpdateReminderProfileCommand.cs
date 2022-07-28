using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReminderProfileRepository;
using Entities.Concrete.ReminderProfileEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ReminderProfiles.Commands
{
    public class UpdateReminderProfileCommand : IRequest<IResult>
    {
        public int ReminderProfileId { get; set; }
        public int ReminderTypeId { get; set; }
        public string IsBefore { get; set; }
        public int Count { get; set; }
        public int ReminderDaysLookupId { get; set; }
        public string Details { get; set; }
        public class UpdateReminderProfileCommandHandler : IRequestHandler<UpdateReminderProfileCommand, IResult>
        {
            private readonly IReminderProfileRepository _reminderProfileRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateReminderProfileCommandHandler(IReminderProfileRepository reminderProfileRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _reminderProfileRepository = reminderProfileRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateReminderProfile), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateReminderProfileCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var reminderProfileObj = await _reminderProfileRepository.GetAsync(x => x.ReminderProfileId == request.ReminderProfileId && x.IsDeleted != true);
                if (reminderProfileObj != null)
                {
                    reminderProfileObj.ReminderProfileId = request.ReminderProfileId;
                    reminderProfileObj.ReminderTypeId = request.ReminderTypeId;
                    reminderProfileObj.IsBefore = request.IsBefore;
                    reminderProfileObj.Count = request.Count.ToString();
                    reminderProfileObj.ReminderDaysLookupId = request.ReminderDaysLookupId;
                    reminderProfileObj.Details = request.Details;
                    reminderProfileObj.ModifiedBy = int.Parse(userId);
                    reminderProfileObj.ModifiedDate = DateTime.Now;
                    _reminderProfileRepository.Update(reminderProfileObj);
                    await _reminderProfileRepository.SaveChangesAsync();

                    return new SuccessResult(Messages.Updated);
                }
                else
                {
                    return new SuccessResult(Messages.NotUpdated);
                }

            }
        }
    }
}
