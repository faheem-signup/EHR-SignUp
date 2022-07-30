using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class BillingClaimsPayerInfo : IEntity
    {
        [Key]
        public int BillingClaimsPayerInfoId { get; set; }
        public int BillingClaimsId { get; set; }
        public string EOBERAId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string CheckNo { get; set; }
        public DateTime? CheckDate { get; set; }
        public decimal CheckAmount { get; set; }
        public int? PayerId { get; set; }
        [ForeignKey("BillingClaimsId")]
        public virtual BillingClaim BillingClaim { get; set; }
    }
}
