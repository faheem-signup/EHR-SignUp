using Core.Entities;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProviderWorkConfigEntity
{
   public class ProviderWorkConfig : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? ProviderId { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime? BreakShiftWorkFrom { get; set; }
        public DateTime? BreakShiftWorkTo { get; set; }
        public TimeSpan SlotSize { get; set; } 
        public bool? IsBreak { get; set; } 
        [ForeignKey("ProviderId")]
        public virtual Provider provider { get; set; }
    }
}
