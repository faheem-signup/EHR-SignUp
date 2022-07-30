using Core.Entities;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.UserToLocationAssignEntity
{
    public class UserToLocationAssign : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Locations Location { get; set; }
        [ForeignKey("UserId")]
        public virtual UserApp UserApp { get; set; }
    }
}
