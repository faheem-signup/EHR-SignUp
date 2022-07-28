using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Concrete.SchedulerEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetSchedulersQuery : IRequest<IDataResult<IEnumerable<AppointmentScheduler>>>
    {
        public int ProviderId { get; set; }
        public DateTime? CurrentDate { get; set; }
        public class GetSchedulersQueryHandler : IRequestHandler<GetSchedulersQuery, IDataResult<IEnumerable<AppointmentScheduler>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            public GetSchedulersQueryHandler(ISchedulerRepository schedulerRepository, IMediator mediator, IMapper mapper, IProviderWorkConfigRepository providerWorkConfigRepository)
            {
                _schedulerRepository = schedulerRepository;
                _mediator = mediator;
                _mapper = mapper;
                _providerWorkConfigRepository = providerWorkConfigRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AppointmentScheduler>>> Handle(GetSchedulersQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerRepository.GetListAsync();
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.SchedulerId).ToList();
                }

                return new SuccessDataResult<IEnumerable<AppointmentScheduler>>(list.ToList());
            }
        }
    }
}
