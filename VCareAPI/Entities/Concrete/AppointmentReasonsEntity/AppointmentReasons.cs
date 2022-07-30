using Core.Entities;
using Entities.Concrete.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.AppointmentReasonsEntity
{
   public class AppointmentReasons : IEntity
    {
        [Key]
        public int AppointmentReasonId { get; set; }
        public string AppointmentReasonDescription { get; set; }
        public int? LocationId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("LocationId")]
        public virtual Locations Locations { get; set; }
    }
}
