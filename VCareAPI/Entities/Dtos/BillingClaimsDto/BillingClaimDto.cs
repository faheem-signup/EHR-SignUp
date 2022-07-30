using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.BillingClaimsDto
{
   public class BillingClaimDto
    {
        public int BillingClaimsId { get; set; }
        public string PatientName { get; set; }
        public int? GenderId { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string InsuranceId { get; set; }
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
        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }
        public int? ServiceProfileId { get; set; }
        public string OrignalClaimNo { get; set; }
        public bool? IsCorrectedClaims { get; set; }
        public string HCFABox19 { get; set; }
        public string OnsetLMP { get; set; }
        public string LastXRay { get; set; }
        public string EOBERAId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string CheckNo { get; set; }
        public DateTime? CheckDate { get; set; }
        public decimal CheckAmount { get; set; }
        public int? PayerId { get; set; }

        public IEnumerable<BillingClaimsPaymentDetail> billingClaimsPaymentDetailList { get; set; }
    }
}
