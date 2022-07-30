using Core.Entities;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Concrete.ProceduresEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientInsuranceAuthorizationCPTEntity
{
   public class PatientInsuranceAuthorizationCPT : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientInsuranceAuthorizationId { get; set; }
        public int? DiagnosisId    { get; set; }
        public int? ProcedureId { get; set; }

        [ForeignKey("PatientInsuranceAuthorizationId")]
        public virtual PatientInsuranceAuthorization patientInsuranceAuthorization { get; set; }
        [ForeignKey("DiagnosisId")]
        public virtual DiagnosisCode diagnosisCodes { get; set; }
        [ForeignKey("ProcedureId")]
        public virtual Procedure procedures { get; set; }
    }
}
