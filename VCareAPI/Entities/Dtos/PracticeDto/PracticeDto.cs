using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class PracticeDto
    {
        public int PracticeId { get; set; }
        public string LegalBusinessName { get; set; }
        public string TaxIDNumber { get; set; }
        public string BillingNPI { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public List<PracticeLocationDto> practiceLocations { get; set; }
    }
}
