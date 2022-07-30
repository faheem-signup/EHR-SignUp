using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LocationDto
{
    public class GetLocationByIdDto
    {
        public int LocationId { get; set; }
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
    }
}
