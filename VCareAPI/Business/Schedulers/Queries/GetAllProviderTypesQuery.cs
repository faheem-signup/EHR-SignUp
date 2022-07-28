using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Dtos.SchedulerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetAllProviderTypesQuery : IRequest<IDataResult<IEnumerable<ProviderTypeDto>>>
    {
        public int? ProviderId { get; set; }

        public class GetAllProviderTypesQueryHandler : IRequestHandler<GetAllProviderTypesQuery, IDataResult<IEnumerable<ProviderTypeDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetAllProviderTypesQueryHandler(ISchedulerRepository schedulerStatusRepository)
            {
                _schedulerRepository = schedulerStatusRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProviderTypeDto>>> Handle(GetAllProviderTypesQuery request, CancellationToken cancellationToken)
            {
                if (request.ProviderId != 0 && request.ProviderId != null)
                {
                    var list = await _schedulerRepository.GetAllProviderTypesByIdList((int)request.ProviderId);
                    return new SuccessDataResult<IEnumerable<ProviderTypeDto>>(list.ToList());
                }
                else
                {
                    var list = await _schedulerRepository.GetAllProviderTypesList();
                    return new SuccessDataResult<IEnumerable<ProviderTypeDto>>(list.ToList());
                }
            }
        }
    }
}
