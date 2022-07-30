using Entities.Concrete.CityStateLookupEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Dtos.LocationDto
{
   public class LocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public int PracticeId { get; set; }
        public string PracticeName { get; set; }
       
        [ForeignKey("State")]

        public CityStateLookup states { get; set; }

    }
}
