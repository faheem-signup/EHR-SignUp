using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientAccidentDetailsDto
    {
        public int? EmploymentStatus { get; set; }
        public int? WorkStatus { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerPhone { get; set; }
        public DateTime? AccidentDate { get; set; }
        public int? AccidentType { get; set; }
        public string Wc { get; set; }
        public int? PatientId { get; set; }
    }
}
