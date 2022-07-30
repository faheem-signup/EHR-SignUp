using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.SchedulerDto
{
    public class PatientAppointmentStatusDto
    {
        public int? AppointmentId { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? PatientId { get; set; }
        public int? RoomId { get; set; }
        public int? ProviderId { get; set; }
        public DateTime? AppointmentCheckInDateTime { get; set; }
        public string AppointmentStatusName { get; set; }
        public string PatientName { get; set; }
        public string RoomNumber { get; set; }
    }
}
