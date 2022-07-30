using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ReminderCallDto
{
    public class ReminderCallDto
    {
        public int? ReminderCallId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public int? ReminderStatusId { get; set; }
        public string ReminderStatusName { get; set; }
        public int? ReminderTypeId { get; set; }
        public string ReminderTypeName { get; set; }
    }
}
