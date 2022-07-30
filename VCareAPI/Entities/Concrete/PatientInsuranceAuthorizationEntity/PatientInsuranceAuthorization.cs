using Core.Entities;
using Entities.Concrete.PatientInsuranceEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientInsuranceAuthorizationEntity
{
   public class PatientInsuranceAuthorization : IEntity
    {
        [Key]
        public int PatientInsuranceAuthorizationId { get; set; }
        public int PatientInsuranceId { get; set; }
        public string AuthorizationNo { get; set; }
        public string Count { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? WarningDate { get; set; }
        public string Comment { get; set; }
        public string AuthorizationUnitsVsits { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("PatientInsuranceId")]
        public virtual PatientInsurance patientInsurance { get; set; }
    }
}
