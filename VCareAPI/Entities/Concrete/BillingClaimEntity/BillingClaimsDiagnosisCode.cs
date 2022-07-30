using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class BillingClaimsDiagnosisCode : IEntity
    {
        [Key]
        public int BillingClaimsDiagnosisCodeId { get; set; }
        public int BillingClaimsId { get; set; }
        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }
        [ForeignKey("BillingClaimsId")]
        public virtual BillingClaim BillingClaim { get; set; }
    }
}
