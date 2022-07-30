using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientProvideReferringDto
    {
        public int? PatientProviderReferringId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string NPI { get; set; }
        public bool? ReferringProvider { get; set; }
        public bool? PCPName { get; set; }
        public bool? Pharmacy { get; set; }
        public bool? ReferringAgency { get; set; }
        public bool? AlcoholAgency { get; set; }
        public bool? ProbationOfficer { get; set; }
        public int? PatientId { get; set; }
    }
}
