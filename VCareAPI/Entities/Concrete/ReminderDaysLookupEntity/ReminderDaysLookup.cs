using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ReminderDaysLookupEntity
{
    public class ReminderDaysLookup : IEntity
    {
        [Key]
        public int ReminderDaysLookupId { get; set; }
        public string ReminderDaysDescription { get; set; }
    }
}
