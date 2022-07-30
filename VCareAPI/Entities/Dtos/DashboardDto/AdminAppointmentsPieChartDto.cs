using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.DashboardDto
{
    public class AdminAppointmentsPieChartDto
    {
        public int TotalAppointment { get; set; }
        public string AppointmentStatusName { get; set; }
        public int AppointmentStatusCount { get; set; }
    }
}
