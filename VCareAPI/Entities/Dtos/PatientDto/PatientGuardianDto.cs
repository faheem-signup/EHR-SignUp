using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientGuardianDto
    {
        public int? GuardianId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string GuardianAddress { get; set; }
        public string GuardianPhone { get; set; }
        public int? PatientId { get; set; }
    }
}
