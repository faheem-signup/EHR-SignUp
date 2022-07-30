using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ReminderProfileDto
{
    public class ReminderProfileDto
    {
        public int ReminderProfileId { get; set; }
        public int? ReminderTypeId { get; set; }
        public string IsBefore { get; set; }
        public string Count { get; set; }
        public int? ReminderDaysLookupId { get; set; }
        public string Details { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ReminderDaysDescription { get; set; }
        public string ReminderTypeDescription { get; set; }
    }
}
