using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ReminderStatusEntity;
using Entities.Concrete.ReminderTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ReminderCallEntity
{
    public class ReminderCall : IEntity
    {
        [Key]
        public int ReminderCallId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ReminderTypeId { get; set; }
        public int ReminderStatusId { get; set; }
        public int AppointmentId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
        [ForeignKey("ReminderTypeId")]
        public virtual ReminderType _reminderType { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment _appointment { get; set; }
        [ForeignKey("ReminderStatusId")]
        public virtual ReminderStatus _reminderStatus { get; set; }
    }
}
