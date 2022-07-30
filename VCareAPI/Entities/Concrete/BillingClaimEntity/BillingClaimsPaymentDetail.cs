using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class BillingClaimsPaymentDetail : IEntity
    {
        [Key]
        public int BillingClaimsPaymentDetailId { get; set; }
        public int BillingClaimsId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CPTHCPCS { get; set; }
        public string Modifiers { get; set; }
        public string ICDPointers { get; set; }
        public string Units { get; set; }
        public string Billed { get; set; }
        public string Allowed { get; set; }
        public string Adjusted { get; set; }
        public decimal Payment { get; set; }
        public decimal Balance { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("BillingClaimsId")]
        public virtual BillingClaim BillingClaim { get; set; }
    }
}
