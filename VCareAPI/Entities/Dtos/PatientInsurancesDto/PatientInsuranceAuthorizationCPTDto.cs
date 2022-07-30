using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientInsurancesDto
{
  public  class PatientInsuranceAuthorizationCPTDto
    {
        //public int Id { get; set; }
        //public int PatientInsuranceAuthorizationId { get; set; }
        public int? DiagnosisId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
