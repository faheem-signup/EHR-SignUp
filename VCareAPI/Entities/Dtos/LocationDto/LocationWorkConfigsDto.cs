using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LocationDto
{
    public class LocationWorkConfigsDto
    {
        public int WorkConfigId { get; set; }
        public string Day { get; set; }
        public DateTime? StartFrom { get; set; }
        public DateTime? EndTo { get; set; }
        public int? LocationWorkConfigStatusId { get; set; }
        public int? LocationId { get; set; }
    }
}
