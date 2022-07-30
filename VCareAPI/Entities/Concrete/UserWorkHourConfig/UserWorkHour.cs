using Core.Entities;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
   public class UserWorkHour : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime? FirstShiftWorkFrom { get; set; }
        public DateTime? FirstShiftWorkTo { get; set; }
        public DateTime? SecondShiftWorkFrom { get; set; }
        public DateTime? SecondShiftWorkTo { get; set; }
        public string SlotSize { get; set; }
        public bool? IsBreak { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserApp userApp { get; set; }

        [ForeignKey("LocationId")]
        public virtual Locations location { get; set; }
    }
}
