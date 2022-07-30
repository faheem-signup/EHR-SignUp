using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class AppointmentAutoReminder : IEntity
    {
        [Key]
        public int AppointmentAutoReminderId { get; set; }
        public string AutoReminderDescription { get; set; }
    }
}
