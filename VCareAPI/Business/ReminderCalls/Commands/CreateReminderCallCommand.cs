using DataAccess.Abstract.IReminderCallsRepository;
using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Concrete.ReminderCallEntity;

namespace Business.Handlers.ReminderCalls.Commands
{
    public class CreateReminderCallCommand : IRequest<IResult>
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ReminderTypeId { get; set; }
        public int ReminderStatusId { get; set; }
        public int AppointmentId { get; set; }
        public class CreateReminderCallCommandHandler : IRequestHandler<CreateReminderCallCommand, IResult>
        {
            private readonly IReminderCallsRepository _reminderCallsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreateReminderCallCommandHandler(IReminderCallsRepository reminderCallsRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _reminderCallsRepository = reminderCallsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateReminderCallCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                ReminderCall sendEmailObj = new ReminderCall
                {
                    PatientId = request.PatientId,
                    AppointmentDate = request.AppointmentDate,
                    ReminderTypeId = request.ReminderTypeId,
                    ReminderStatusId = request.ReminderStatusId,
                };

                _reminderCallsRepository.Add(sendEmailObj);
                await _reminderCallsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
