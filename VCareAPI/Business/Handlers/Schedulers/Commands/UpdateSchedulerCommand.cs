using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using DataAccess.Abstract.ISchedulerStatusRepository;
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
    public class UpdateSchedulerCommand : IRequest<IResult>
    {
        public int SchedulerId { get; set; }
        public int SchedulerStatusId { get; set; }
        public class UpdateSchedulerCommandHandler : IRequestHandler<UpdateSchedulerCommand, IResult>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ISchedulerStatusRepository _scheduleStatusrepository;

            public UpdateSchedulerCommandHandler(ISchedulerRepository schedulerRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor, ISchedulerStatusRepository scheduleStatusrepository)
            {
                _schedulerRepository = schedulerRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _scheduleStatusrepository = scheduleStatusrepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateSchedulerCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var SchedulerObj = await _schedulerRepository.GetAsync(x => x.SchedulerId == request.SchedulerId);
                if (SchedulerObj != null)
                {
                    if (request.SchedulerStatusId == 3)
                        SchedulerObj.CheckinTime = DateTime.Now;
                    if (request.SchedulerStatusId == 4)
                        SchedulerObj.CheckoutTime = DateTime.Now;

                    SchedulerObj.SchedulerId = request.SchedulerId;
                    SchedulerObj.SchedulerStatusId = request.SchedulerStatusId;
                    SchedulerObj.ModifiedBy = int.Parse(userId);
                    SchedulerObj.ModifiedDate = DateTime.Now;

                    _schedulerRepository.Update(SchedulerObj);
                    await _schedulerRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
