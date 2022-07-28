using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using Entities.Concrete.LocationWorkConfigsEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GetLocationWorkConfigsWorkConfig.Queries
{
    public class GetLocationWorkConfigsListQuery : IRequest<IDataResult<IEnumerable<LocationWorkConfigs>>>
    {
        public int LocationId { get; set; }

        public class GetLocationWorkConfigsListQueryHandler : IRequestHandler<GetLocationWorkConfigsListQuery, IDataResult<IEnumerable<LocationWorkConfigs>>>
        {
            private readonly ILocationWorkConfigsRepository _locationWorkConfigsRepository;

            public GetLocationWorkConfigsListQueryHandler(ILocationWorkConfigsRepository locationWorkConfigsRepository)
            {
                _locationWorkConfigsRepository = locationWorkConfigsRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LocationWorkConfigs>>> Handle(GetLocationWorkConfigsListQuery request, CancellationToken cancellationToken)
            {
                var list = await _locationWorkConfigsRepository.GetListAsync(x => x.LocationId == request.LocationId);

                return new SuccessDataResult<IEnumerable<LocationWorkConfigs>>(list.ToList());
            }
        }
    }
}
