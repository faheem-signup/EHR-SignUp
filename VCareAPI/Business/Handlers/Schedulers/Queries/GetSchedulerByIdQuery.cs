using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using DataAccess.Abstract.ISchedulerStatusRepository;
using Entities.Concrete.SchedulerEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetSchedulerByIdQuery : IRequest<IDataResult<AppointmentScheduler>>
    {
        public int SchedulerId { get; set; }
        public class GetSchedulerByIdQueryHandler : IRequestHandler<GetSchedulerByIdQuery, IDataResult<AppointmentScheduler>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetSchedulerByIdQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<AppointmentScheduler>> Handle(GetSchedulerByIdQuery request, CancellationToken cancellationToken)
            {
                var scheduler = await _schedulerRepository.GetAsync(x => x.SchedulerId == request.SchedulerId);

                return new SuccessDataResult<AppointmentScheduler>(scheduler);
            }
        }
    }
}
