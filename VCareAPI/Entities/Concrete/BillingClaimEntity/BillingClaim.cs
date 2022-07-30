using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.BillingClaim
{
   public class BillingClaim:IEntity
    {
        [Key]
        public int BillingClaimsId { get; set; }
        public string PatientName { get; set; }
        public int? GenderId { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string InsuranceId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
