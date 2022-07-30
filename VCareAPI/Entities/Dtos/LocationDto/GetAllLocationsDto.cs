using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LocationDto
{
    public class GetAllLocationsDto
    {
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string NPI { get; set; }
        public int? POSId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }
        public int? PracticeId { get; set; }
        public string PracticeName { get; set; }
        public string StatusName { get; set; }
        public int? ID { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountyId { get; set; }
        public string County { get; set; }
        public int? StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int? ZipId { get; set; }
        public string ZipCode { get; set; }
    }
}
