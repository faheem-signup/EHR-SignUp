using AutoMapper;
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.ILocationRepository;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Locations.Queries
{
    public class GetLocationQuery : IRequest<IDataResult<GetLocationData>>
    {
        public int LocationId { get; set; }

        public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, IDataResult<GetLocationData>>
        {
            private readonly ILocationRepository _locationRepository;
            private readonly IMapper _mapper;

            public GetLocationQueryHandler(ILocationRepository locationRepository, IMapper mapper)
            {
                _locationRepository = locationRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<GetLocationData>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
            {
                var locationObj = await _locationRepository.GetAsync(x => x.LocationId == request.LocationId);
                var workConfigsListObj = await _locationRepository.GetLocationWorkConfigsByLocationId(request.LocationId);
                
                return new SuccessDataResult<GetLocationData>(workConfigsListObj);
            }
        }
    }
}
