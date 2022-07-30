using Core.Entities;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.SchedulerStatusEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.SchedulerEntity
{
    public class AppointmentScheduler : IEntity
    {
        [Key]
        public int SchedulerId { get; set; }
        public DateTime? CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? AppointmentId { get; set; }
        public int? SchedulerStatusId { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment appointment { get; set; }

        [ForeignKey("SchedulerStatusId")]
        public virtual SchedulerStatus schedulerStatus { get; set; }
    }
}
