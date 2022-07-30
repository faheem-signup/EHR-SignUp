using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ReminderTypeEntity
{
    public class ReminderType : IEntity
    {
        [Key]
        public int ReminderTypeId { get; set; }
        public string Description { get; set; }
    }
}
