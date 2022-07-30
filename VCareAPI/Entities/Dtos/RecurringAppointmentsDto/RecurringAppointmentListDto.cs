using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RecurringAppointmentsDto
{
    public class RecurringAppointmentListDto
    {
        public int RecurringAppointmentId { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public string OriginalScheduledTime { get; set; }
        public string RecurringDescription { get; set; }
        public string RoomName { get; set; }
        public string Detail { get; set; }
        public DateTime FirstAppointDate { get; set; }
        public DateTime LastAppointDate { get; set; }
    }
}
