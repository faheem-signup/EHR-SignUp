using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RecurringAppointmentsDto
{
    public class RecurringAppointmentsDto
    {
        public int RecurringAppointmentId { get; set; }
        public string PatientName { get; set; }
        public int? PatientId { get; set; }
        public string OriginalScheduledTime { get; set; }
        public string RecurringDescription { get; set; }
        public string RoomName { get; set; }
        public string Detail { get; set; }
        public List<DateTime> FutureInstances { get; set; }
    }
}
