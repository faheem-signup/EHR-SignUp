using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class PracticeLocationDto
    {
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string NPI { get; set; }
        public string Phone { get; set; }
        public int? PracticeId { get; set; }
    }
}
