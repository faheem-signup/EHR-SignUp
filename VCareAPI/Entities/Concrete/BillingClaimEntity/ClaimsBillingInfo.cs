using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class ClaimsBillingInfo : IEntity
    {
        [Key]
        public int ClaimsBillingInfoId { get; set; }
        public int BillingClaimsId { get; set; }
        public int? BilledStatusId { get; set; }
        public int? CurrentStatusId { get; set; }
        public string PrimaryInsurance { get; set; }
        public string SecondaryInsurance { get; set; }
        public int? LocationId { get; set; }
        public string AttendingProvider { get; set; }
        public string SupervisingProvider { get; set; }
        public string ReferringProvider { get; set; }
        public string BillingProvider { get; set; }
        public string PlaceOfService { get; set; }
        [ForeignKey("BillingClaimsId")]
        public virtual BillingClaim BillingClaim { get; set; }
    }
}
