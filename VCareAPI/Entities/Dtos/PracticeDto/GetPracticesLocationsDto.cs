using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class GetPracticesLocationsDto
    {
        public int PracticeId { get; set; }
        public string LegalBusinessName { get; set; }
        public string TaxIDNumber { get; set; }
        public string BillingNPI { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string NPI { get; set; }
        public string Phone { get; set; }
    }
}
