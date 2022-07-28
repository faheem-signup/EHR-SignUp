using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Locations.Queries
{
    public class GetLocationListQuery : BasePaginationQuery<IDataResult<IEnumerable<GetAllLocationsDto>>>
    {
        public int? PracticeId { get; set; }
        public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, IDataResult<IEnumerable<GetAllLocationsDto>>>
        {
            private readonly ILocationRepository _locationRepository;
            private readonly IMapper _mapper;
            public GetLocationListQueryHandler(ILocationRepository locationRepository, IMapper mapper)
            {
                _locationRepository = locationRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<GetAllLocationsDto>>> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _locationRepository.GetLocationByPracticeId(request.PracticeId);
                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<GetAllLocationsDto>>(dataList, rawData.Count(), request.PageNumber);
            }
        }
    }
}
