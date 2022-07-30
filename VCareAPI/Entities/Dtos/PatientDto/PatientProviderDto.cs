using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientProviderDto
    {
        public int? AttendingPhysician { get; set; }
        public int? SupervisingProvider { get; set; }
        public int? ReferringProvider { get; set; }
        public int? PCPName { get; set; }
        public int? Pharmacy { get; set; }
        public int? ReferringAgency { get; set; }
        public int? DrugsAgency { get; set; }
        public int? ProbationOfficer { get; set; }
        public int? SubstanceAbuseStatus { get; set; }
        public int? Alcohol { get; set; }
        public int? IllicitSubstances { get; set; }
        public int? PatientId { get; set; }
        public int? LocationId { get; set; }
    }
}
