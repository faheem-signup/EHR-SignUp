using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
    public class UserWorkHourDto
    {
        public int? Id { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime? FirstShiftWorkFrom { get; set; }
        public DateTime? FirstShiftWorkTo { get; set; }
        public DateTime? SecondShiftWorkFrom { get; set; }
        public DateTime? SecondShiftWorkTo { get; set; }
        public string SlotSize { get; set; }
        public bool? IsBreak { get; set; }
        public int? UserId { get; set; }
    }
}
