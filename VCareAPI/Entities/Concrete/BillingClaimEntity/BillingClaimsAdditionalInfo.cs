using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class BillingClaimsAdditionalInfo : IEntity
    {
        [Key]
        public int BillingClaimsAdditionalInfoId { get; set; }
        public int BillingClaimsId { get; set; }
        public int? ServiceProfileId { get; set; }
        public string OrignalClaimNo { get; set; }
        public bool? IsCorrectedClaims { get; set; }
        public string HCFABox19 { get; set; }
        public string OnsetLMP { get; set; }
        public string LastXRay { get; set; }
        [ForeignKey("BillingClaimsId")]
        public virtual BillingClaim BillingClaim { get; set; }
    }
}
