using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ReminderStatusEntity
{
    public class ReminderStatus : IEntity
    {
        [Key]
        public int ReminderStatusId { get; set; }
        public string Description { get; set; }
    }
}
