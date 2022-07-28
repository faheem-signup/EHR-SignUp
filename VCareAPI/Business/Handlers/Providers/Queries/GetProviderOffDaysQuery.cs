using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderRepository;
using MediatR;

namespace Business.Handlers.Providers.Queries
{
    public class GetProviderOffDaysQuery : IRequest<IDataResult<IEnumerable<string>>>
    {
        public int ProviderId { get; set; }
        public int LocationId { get; set; }

        public class GetProviderOffDaysQueryHandler : IRequestHandler<GetProviderOffDaysQuery, IDataResult<IEnumerable<string>>>
        {
            private readonly IProviderRepository _providerRepository;

            public GetProviderOffDaysQueryHandler(IProviderRepository providerRepository)
            {
                _providerRepository = providerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<string>>> Handle(GetProviderOffDaysQuery request, CancellationToken cancellationToken)
            {
                var list = await _providerRepository.GetProviderOffDaysList(request.ProviderId, request.LocationId);

                return new SuccessDataResult<IEnumerable<string>>(list.ToList());
            }
        }
    }
}
