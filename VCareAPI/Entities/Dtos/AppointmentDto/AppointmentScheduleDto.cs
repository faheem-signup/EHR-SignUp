using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AppointmentDto
{
    public class AppointmentScheduleDto
    {
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
        public int AppointmentsCheckInId { get; set; }
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
        public DateTime? AppointmentCheckInDateTime { get; set; }
        public DateTime? AppointmentCheckOutDateTime { get; set; }
        public string ProviderName { get; set; }
        public string ServiceProfileName { get; set; }
        public string LocationName { get; set; }
        public string AppointmentTypeName { get; set; }
        public string AppointmentReason { get; set; }
        public string RoomName { get; set; }
        public string AppointmentStatusName { get; set; }
        public string PatientName { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string GenderName { get; set; }
        public string Duration { get; set; }
        public DateTime? DOB { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Degree { get; set; }
        public List<GroupPatientAppointmentDto> GroupPatientAppointmentList { get; set; }
    }
}
