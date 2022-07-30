using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.AppointmentsCheckInEntity
{
  public class AppointmentsCheckIn : IEntity
    {
        [Key]
        public int AppointmentsCheckInId { get; set; }
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
        public DateTime? AppointmentCheckInDateTime { get; set; }
        public DateTime? AppointmentCheckOutDateTime { get; set; }
        public DateTime? AppointmentRescheduleDateTime { get; set; }
        public DateTime? AppointmentPendingRefillsDateTime { get; set; }
        public DateTime? AppointmentCancelledDatetime { get; set; }
        public DateTime? AppointmentNoShowDatetime { get; set; }
        public DateTime? AppointmentScheduledDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment _appointment { get; set; }
        [ForeignKey("AppointmentStatusId")]
        public virtual AppointmentStatus _appointmentStatus { get; set; }
    }
}
