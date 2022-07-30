using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.InsuranceDto
{
    public class AssignInsuranceDto
    {
        public int PracticeId { get; set; }
        public int InsuranceId { get; set; }
        public int? CompareInsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceType { get; set; }
        public bool InsuranceStatus { get; set; }
    }
}
