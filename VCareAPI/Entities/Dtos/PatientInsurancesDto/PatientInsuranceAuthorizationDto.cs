using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientInsurancesDto
{
   public class PatientInsuranceAuthorizationDto
    {
        public int PatientInsuranceAuthorizationId { get; set; }
        public int PatientInsuranceId { get; set; }
        public string AuthorizationNo { get; set; }
        public string Count { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? WarningDate { get; set; }
        public string Comment { get; set; }
        public string ProcedureCode { get; set; }
        public string InsuranceName { get; set; }
        public string AuthorizationUnitsVsits { get; set; }
        public IEnumerable<PatientInsuranceAuthorizationCPTDto> patientInsuranceAuthorizationCPTList { get; set; }
    }
}
