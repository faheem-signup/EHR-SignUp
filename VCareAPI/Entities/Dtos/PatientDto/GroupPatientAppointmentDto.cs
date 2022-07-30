using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
  public class GroupPatientAppointmentDto
    {
        public int GroupAppointmentId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string GenderName { get; set; }
        public DateTime? DOB { get; set; }
    }
}
