using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientInfoDetailsEntity
{
    public class PatientInfoDetail : IEntity
    {
        [Key]
        public int PatientInfoId { get; set; }
        public int? SmokingStatus { get; set; }
        public int? Packs { get; set; }
        public int? HospitalizationStatus { get; set; }
        public DateTime? LastHospitalizationDate { get; set; }
        public DateTime? DisabilityDate { get; set; }
        public int? DisabilityStatus { get; set; }
        public DateTime? DeathDate { get; set; }
        public string DeathReason { get; set; }
        public int? SubstanceAbuseStatus { get; set; }
        public int? Alcohol { get; set; }
        public int? IllicitSubstances { get; set; }
        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
    }
}
