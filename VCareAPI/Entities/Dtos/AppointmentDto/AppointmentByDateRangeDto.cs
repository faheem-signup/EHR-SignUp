using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AppointmentDto
{
    public class AppointmentByDateRangeDto
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public bool? AllowGroupPatient { get; set; }
    }
}
