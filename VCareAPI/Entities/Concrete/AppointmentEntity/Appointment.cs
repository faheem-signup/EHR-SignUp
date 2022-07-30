using Core.Entities;
using Entities.Concrete.AppointmentReasonsEntity;
using Entities.Concrete.Location;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.RoomEntity;
using Entities.Concrete.ServiceProfileEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.AppointmentEntity
{
    public class Appointment : IEntity
    {
        [Key]
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
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public TimeSpan Duration { get; set; }
        public string StatusReasons { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patients { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        [ForeignKey("LocationId")]
        public virtual Locations Locations { get; set; }
        [ForeignKey("ServiceProfileId")]
        public virtual ServiceProfile _serviceProfile { get; set; }
        [ForeignKey("VisitType")]
        public virtual AppointmentType _appointmentTypes { get; set; }
        [ForeignKey("AppointmentStatus")]
        public virtual AppointmentStatus _appointmentStatuses { get; set; }
        [ForeignKey("RoomNo")]
        public virtual Room Room { get; set; }
        [ForeignKey("VisitReason")]
        public virtual AppointmentReasons AppointmentReasons { get; set; }
    }
}
