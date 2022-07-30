using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.CityStateLookupEntity
{
  public class CityStateLookup : IEntity
    {
        [Key]
        public int ID { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountyId { get; set; }
        public string County { get; set; }
        public int? StateId { get; set; }
        public string StateCode { get; set; }
        public string State { get; set; }
        public int? ZipId { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    
    }
}
