using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AssignPracticeInsurancesDto
{
   public class AssignPracticeInsuranceDto
    {
        public int PracticeInsuranceId { get; set; }
        public int InsuranceId { get; set; }
        public int PracticeId { get; set; }
        public string InsuranceType { get; set; }
        public string InsuranceName { get; set; }
    }
}
