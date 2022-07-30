using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientInfoDetailDropdownDto
    {
        public string SmokingStatusDescription { get; set; }
        public string PacksDescription { get; set; }
        public string HospitalizationStatusDescription { get; set; }
        public string PatientDisabilityStatusDescription { get; set; }
        public string SubstanceAbuseStatusDescription { get; set; }
        public string AlcoholDescription { get; set; }
        public string IllicitSubstancesDescription { get; set; }
    }
}
