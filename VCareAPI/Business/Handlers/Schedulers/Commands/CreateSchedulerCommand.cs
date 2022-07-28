using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using DataAccess.Abstract.ISchedulerStatusRepository;
using Entities.Concrete.SchedulerEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Commands
{
    public class CreateSchedulerCommand : IRequest<IResult>
    {
        public int? AppointmentId { get; set; }
        public class CreateSchedulerCommandHandler : IRequestHandler<CreateSchedulerCommand, IResult>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerStatusRepository _scheduleStatusrepository;
            public CreateSchedulerCommandHandler(ISchedulerRepository schedulerRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor, ISchedulerStatusRepository scheduleStatusrepository)
            {
                _schedulerRepository = schedulerRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _scheduleStatusrepository = scheduleStatusrepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateSchedulerCommand request, CancellationToken cancellationToken)
            {
                var statusScheduler = await _scheduleStatusrepository.GetSchedulerStatusById("Pending");
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                AppointmentScheduler schedulerObj = new AppointmentScheduler
                {
                    AppointmentId = request.AppointmentId,
                    SchedulerStatusId = statusScheduler.SchedulerStatusId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now
                };

                _schedulerRepository.Add(schedulerObj);
                await _schedulerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
