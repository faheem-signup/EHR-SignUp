using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.InsurancePayerTypeEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.InsuranceEntity
{
    public class Insurance : IEntity
    {
        [Key]
        public int InsuranceId { get; set; }
        public int? PayerId { get; set; }
        public int? InsurancePayerTypeId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int? DenialResponseLimit { get; set; }
        public int? TimlyFilingLimit { get; set; }
        public int? BillingProvider { get; set; }
        public int? PracticeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }

        [ForeignKey("InsurancePayerTypeId")]
        public virtual InsurancePayerType insurancePayerType { get; set; }

        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }

        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }

        [ForeignKey("ZIP")]
        public virtual CityStateLookup ZIPs { get; set; }
    }
}
