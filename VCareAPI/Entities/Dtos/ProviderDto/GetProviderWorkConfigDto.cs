using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class GetProviderWorkConfigDto
    {
        public int Id { get; set; }
        public int? ProviderId { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime? BreakShiftWorkFrom { get; set; }
        public DateTime? BreakShiftWorkTo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string SlotSize { get; set; }
        public bool? IsBreak { get; set; }
    }
}
