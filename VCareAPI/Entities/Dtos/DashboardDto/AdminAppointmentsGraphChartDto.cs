using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.DashboardDto
{
    public class AdminAppointmentsGraphChartDto
    {
        public int? TotalAppointment { get; set; }
        public int? TotalPatients { get; set; }
        public string EndDate { get; set; }
    }
}
