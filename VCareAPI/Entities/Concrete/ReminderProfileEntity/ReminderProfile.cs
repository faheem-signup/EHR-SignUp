using Core.Entities;
using Entities.Concrete.ReminderDaysLookupEntity;
using Entities.Concrete.ReminderTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ReminderProfileEntity
{
    public class ReminderProfile : IEntity
    {
        [Key]
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
        [ForeignKey("ReminderTypeId")]
        public virtual ReminderType ReminderType { get; set; }
        [ForeignKey("ReminderDaysLookupId")]
        public virtual ReminderDaysLookup reminderDaysLookup { get; set; }
    }
}
