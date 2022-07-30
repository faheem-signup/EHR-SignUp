using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientMoreInfoDto
    {
        public int? SmokingStatus { get; set; }
        public int? Packs { get; set; }
        public int? HospitalizationStatus { get; set; }
        public DateTime? LastHospitalizationDate { get; set; }
        public DateTime? DisabilityDate { get; set; }
        public int? DisabilityStatus { get; set; }
        public DateTime? DeathDate { get; set; }
        public string DeathReason { get; set; }
        public int? PatientId { get; set; }
    }
}
