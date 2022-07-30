using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class PracticeDataDto
    {
        public int PracticeId { get; set; }
        public string LegalBusinessName { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
    }
}
