using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.AppointmentReasonsEntity;
using Entities.Concrete.LookupsEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.RecurringAppointmentsEntity
{
   public class RecurringAppointments :IEntity
    {
        [Key]
        public int RecurringAppointmentId { get; set; }
        public string Weekdays { get; set; }
        public int RecurEvery { get; set; }
        public int WeekType { get; set; }
        public int RecurringVisitReason { get; set; }
        public DateTime? FirstAppointDate { get; set; }
        public DateTime? LastAppointDate { get; set; }
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey("WeekType")]
        public virtual WeekTypeLookup _weekTypeLookup { get; set; }
        [ForeignKey("RecurringVisitReason")]
        public virtual AppointmentReasons _appointmentReasons { get; set; }
    }
}
