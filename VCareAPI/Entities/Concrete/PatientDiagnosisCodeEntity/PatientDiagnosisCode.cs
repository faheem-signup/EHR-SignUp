using Core.Entities;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientDiagnosisCodeEntity
{
    public class PatientDiagnosisCode : IEntity
    {
        [Key]
        public int PatientDiagnosisCodeId { get; set; }
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public int DiagnosisId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime ResolvedDate { get; set; }
        public string DiagnoseCodeType { get; set; }
        public string SNOMEDCode { get; set; }
        public string SNOMEDCodeDesctiption { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DiagnosisId")]
        public virtual DiagnosisCode DiagnosisCode { get; set; }
    }

}
