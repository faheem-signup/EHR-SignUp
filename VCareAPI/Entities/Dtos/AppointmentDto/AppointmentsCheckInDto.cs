using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AppointmentDto
{
  public class AppointmentsCheckInDto
    {
        public bool AllowGroupPatient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public int? ProviderId { get; set; }
        public int? LocationId { get; set; }
        public int? ServiceProfileId { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? VisitType { get; set; }
        public int? VisitReason { get; set; }
        public int? RoomNo { get; set; }
        public int? PatientId { get; set; }
        public string GroupAppointmentReason { get; set; }
        public string Notes { get; set; }
        public bool? IsRecurringAppointment { get; set; }
        public bool? IsFollowUpAppointment { get; set; }
        public int AppointmentsCheckInId { get; set; }
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
        public DateTime? AppointmentCheckInDateTime { get; set; }
        public DateTime? AppointmentCheckOutDateTime { get; set; }
        public string RoomName { get; set; }
    }
}
