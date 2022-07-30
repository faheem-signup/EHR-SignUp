using Entities.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LocationDto
{
    public class GetLocationData
    {
        public GetLocationByIdDto location { get; set; }
        public List<LocationWorkConfigsDto> locationWorkConfigsList { get; set; }
    }
}
