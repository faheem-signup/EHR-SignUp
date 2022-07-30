using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.FollowUpAppointmentEntity;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Entities.Concrete.RecurringAppointmentsEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AppointmentDto
{
   public class AppointmentDTO //:Appointment
    {

        public int AppointmentId { get; set; }
        public bool? AllowGroupPatient { get; set; }
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
       // public TimeSpan? Duration { get; set; }
        public string Duration { get; set; }
        public string StatusReasons { get; set; }
        public IEnumerable<GroupPatientAppointment> GroupPatientAppointmentList { get; set; }
        public GroupPatientAppointment GroupPatientAppointment { get; set; }

        public RecurringAppointments RecurringAppointments { get; set; }
        public FollowUpAppointment FollowUpAppointments { get; set; }
    }
}
