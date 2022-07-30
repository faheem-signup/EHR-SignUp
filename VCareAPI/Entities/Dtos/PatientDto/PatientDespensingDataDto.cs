using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientDespensingDataDto
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string SSN { get; set; }
        public string ProviderName { get; set; }
        public string NPINumber { get; set; }
        public string ReferringProviderName { get; set; }
        public string GenderName { get; set; }
    }
}
