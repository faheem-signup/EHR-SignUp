using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Concrete.SchedulerEntity;
using Entities.Dtos.LocationDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetScheduleLocationListQuery : IRequest<IDataResult<IEnumerable<LocationLookupDto>>>
    {
        public int? ProviderId { get; set; }
        public int? PracticeId { get; set; }

        public class GetScheduleLocationListQueryHandler : IRequestHandler<GetScheduleLocationListQuery, IDataResult<IEnumerable<LocationLookupDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;

            public GetScheduleLocationListQueryHandler(ISchedulerRepository schedulerRepository, IMediator mediator, IMapper mapper, IProviderWorkConfigRepository providerWorkConfigRepository)
            {
                _schedulerRepository = schedulerRepository;
                _mediator = mediator;
                _mapper = mapper;
                _providerWorkConfigRepository = providerWorkConfigRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LocationLookupDto>>> Handle(GetScheduleLocationListQuery request, CancellationToken cancellationToken)
            {
                List<LocationLookupDto> list = new List<LocationLookupDto>();
                if (request.ProviderId != null)
                {
                    list = await _schedulerRepository.GetLocationByProviderId((int)request.ProviderId);
                }
                else if (request.PracticeId != null)
                {
                    list = await _schedulerRepository.GetLocationByPracticeId((int)request.PracticeId);
                }

                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.LocationId).ToList();
                }

                return new SuccessDataResult<IEnumerable<LocationLookupDto>>(list.ToList());
            }
        }
    }
}
