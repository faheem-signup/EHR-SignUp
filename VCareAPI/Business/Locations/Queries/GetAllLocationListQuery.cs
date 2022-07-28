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
    public class GetAllLocationListQuery : IRequest<IDataResult<IEnumerable<Entities.Concrete.Location.Locations>>>
    {
        public int? PracticeId { get; set; }
        public class GetAllLocationListQueryHandler : IRequestHandler<GetAllLocationListQuery, IDataResult<IEnumerable<Entities.Concrete.Location.Locations>>>
        {
            private readonly ILocationRepository _locationRepository;
            private readonly IMapper _mapper;
            public GetAllLocationListQueryHandler(ILocationRepository locationRepository, IMapper mapper)
            {
                _locationRepository = locationRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Entities.Concrete.Location.Locations>>> Handle(GetAllLocationListQuery request, CancellationToken cancellationToken)
            {

                if (request.PracticeId != null && request.PracticeId != 0)
                {
                    var locationList = await _locationRepository.GetListAsync(x => x.PracticeId == request.PracticeId);
                    if (locationList.Count() > 0)
                    {
                        locationList = locationList.OrderByDescending(x => x.LocationId).ToList();
                    }

                    return new SuccessDataResult<IEnumerable<Entities.Concrete.Location.Locations>>(locationList.ToList());
                }
                else
                {
                    var locationList = await _locationRepository.GetListAsync();
                    if (locationList.Count() > 0)
                    {
                        locationList = locationList.OrderByDescending(x => x.LocationId).ToList();
                    }

                    return new SuccessDataResult<IEnumerable<Entities.Concrete.Location.Locations>>(locationList.ToList());
                }
            }
        }
    }
}
