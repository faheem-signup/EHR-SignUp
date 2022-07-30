using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientAppointmentDto
    {
        public string PatientName { get; set; }
        public string ProviderName { get; set; }
        public string Location { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? VisitReason { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

    }
}
