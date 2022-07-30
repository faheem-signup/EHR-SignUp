using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDiagnosisCodeDto
{
   public class PatientDiagnosisCodeDto
    {
        public int PatientDiagnosisCodeId { get; set; }
        public string ProviderName { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime ResolvedDate { get; set; }
        public string DiagnoseCodeType { get; set; }
        public string SNOMEDCode { get; set; }
        public string SNOMEDCodeDesctiption { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string DiagnosisCode { get; set; }
    }
}
