using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerStatusRepository;
using Entities.Concrete.SchedulerStatusEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetSchedulerStatusQuery : IRequest<IDataResult<IEnumerable<SchedulerStatus>>>
    {
        public class GetSchedulerStatusQueryHandler : IRequestHandler<GetSchedulerStatusQuery, IDataResult<IEnumerable<SchedulerStatus>>>
        {
            private readonly ISchedulerStatusRepository _schedulerStatusRepository;

            public GetSchedulerStatusQueryHandler(ISchedulerStatusRepository schedulerStatusRepository)
            {
                _schedulerStatusRepository = schedulerStatusRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SchedulerStatus>>> Handle(GetSchedulerStatusQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerStatusRepository.GetListAsync();
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.SchedulerStatusId).ToList();
                }

                return new SuccessDataResult<IEnumerable<SchedulerStatus>>(list.ToList());
            }
        }
    }
}
