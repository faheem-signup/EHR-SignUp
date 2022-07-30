using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.SchedulerDto
{
    public class AppiontmentStatusDetailDto
    {
        public int AppointmentId { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? ProviderId { get; set; }
        public string AppointmentStatusName { get; set; }
        public string ProviderName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? Total { get; set; }
    }
}
