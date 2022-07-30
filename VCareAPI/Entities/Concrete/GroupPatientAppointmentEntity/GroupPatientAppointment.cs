using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.GroupPatientAppointmentEntity
{
   public class GroupPatientAppointment : IEntity
    {
        [Key]
        public int GroupAppointmentId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("PatientId")]

        public virtual Patient Patients { get; set; }
        [ForeignKey("AppointmentId")]

        public virtual Appointment Appointment { get; set; }
    }
}
