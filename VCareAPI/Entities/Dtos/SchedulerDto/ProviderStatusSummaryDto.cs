using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.SchedulerDto
{
    public class ProviderStatusSummaryDto
    {
        public int AppointmentId { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? ProviderId { get; set; }
        public string AppointmentStatusName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderEmail { get; set; }
        public int? TotalAppointment { get; set; }
        public int? TotalCheckedIn { get; set; }
        public int? TotalCheckedOut { get; set; }
        public int? TotalCancelled { get; set; }
        public int? TotalRescheduled { get; set; }
        public int? TotalScheduled { get; set; }
        public int? TotalNoShow { get; set; }
        public int? TotalProvierAppointment { get; set; }
        public int? TotalProvierCompletedAppointment { get; set; }
        public int? TotalProvierScheduledAppointment { get; set; }
        public int? TotalProvierCancelledAppointment { get; set; }
        public int? TotalProvierCheckedInAppointment { get; set; }
        public int? TotalProvierRescheduledAppointment { get; set; }
        public int? TotalProvierNoShowAppointment { get; set; }

    }
}
