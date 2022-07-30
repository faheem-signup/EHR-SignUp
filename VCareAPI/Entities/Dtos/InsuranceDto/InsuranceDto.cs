using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.InsuranceDto
{
    public class InsuranceDto
    {
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
        public string PayerTypeDescription { get; set; }
        public int? ID { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountyId { get; set; }
        public string County { get; set; }
        public int? StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int? ZipId { get; set; }
        public string ZipCode { get; set; }
    }
}
