using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ICityStateLookupRepository;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.Role;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.CityStateLookups.Queries
{
    public class GetCityStateLookupsListQuery : IRequest<IDataResult<IEnumerable<CityStateLookup>>>
    {

        public class GetCityStateLookupsListQueryHandler : IRequestHandler<GetCityStateLookupsListQuery, IDataResult<IEnumerable<CityStateLookup>>>
        {
            private readonly ICityStateLookupRepository _cityStateLookupRepository;
            public GetCityStateLookupsListQueryHandler(ICityStateLookupRepository cityStateLookupRepository)
            {
                _cityStateLookupRepository = cityStateLookupRepository;
            }

            //[SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
           // [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<CityStateLookup>>> Handle(GetCityStateLookupsListQuery request, CancellationToken cancellationToken)
            {
                var list = await _cityStateLookupRepository.GetListAsync();
                  return new SuccessDataResult<IEnumerable<CityStateLookup>>(list);
            }
        }
    }
}
