using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.LookupsEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.FollowUpAppointmentEntity
{
    public class FollowUpAppointment : IEntity
    {
        [Key]
        public int FollowUpId { get; set; }
        public string FollowUpsVisitReason { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public int AppointmentId { get; set; }
        public int? AppointmentAutoReminderId { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey("AppointmentAutoReminderId")]
        public virtual AppointmentAutoReminder _appointmentAutoReminder { get; set; }
    }
}
