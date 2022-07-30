using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ReminderProfileDto
{
    public class GetReminderProfileByIdDto
    {
        public int ReminderProfileId { get; set; }
        public int? ReminderTypeId { get; set; }
        public string IsBefore { get; set; }
        public string Count { get; set; }
        public int? ReminderDaysLookupId { get; set; }
        public string Details { get; set; }
    }
}
