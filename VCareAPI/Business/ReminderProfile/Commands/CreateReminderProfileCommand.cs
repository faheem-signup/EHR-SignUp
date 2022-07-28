namespace Business.Handlers.ReminderProfiles.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
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

    public class CreateReminderProfileCommand : IRequest<IResult>
    {
        public int ReminderTypeId { get; set; }
        public string IsBefore { get; set; }
        public int Count { get; set; }
        public int ReminderDaysLookupId { get; set; }
        public string Details { get; set; }
        public class CreateReminderProfileCommandHandler : IRequestHandler<CreateReminderProfileCommand, IResult>
        {
            private readonly IReminderProfileRepository _reminderProfileRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateReminderProfileCommandHandler(IReminderProfileRepository reminderProfileRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _reminderProfileRepository = reminderProfileRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorReminderProfile), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateReminderProfileCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                ReminderProfile reminderProfileObj = new ReminderProfile
                {
                    ReminderTypeId = request.ReminderTypeId,
                    IsBefore = request.IsBefore,
                    Count = request.Count.ToString(),
                    ReminderDaysLookupId = request.ReminderDaysLookupId,
                    Details = request.Details,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _reminderProfileRepository.Add(reminderProfileObj);
                await _reminderProfileRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
