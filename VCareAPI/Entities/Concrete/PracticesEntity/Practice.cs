using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.CLIATypeEntity;
using Entities.Concrete.Location;
using Entities.Concrete.PracticeTypeEntity;
using Entities.Concrete.TaxTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PracticesEntity
{
    public class Practice : IEntity
    {
        [Key]
        public int PracticeId { get; set; }
        public string LegalBusinessName { get; set; }
        public string DBA { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhysicalAddress { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public string OfficeEmail { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public string CLIANumber { get; set; }
        public int? CLIATypeId { get; set; }
        public string LiabilityInsuranceID { get; set; }
        public DateTime? LiabilityInsuranceExpiryDate { get; set; }
        public string LiabilityInsuranceCarrier { get; set; }
        public string StateLicense { get; set; }
        public DateTime? StateLicenseExpiryDate { get; set; }
        public string DeaNumber { get; set; }
        public DateTime? DeaNumberExpiryDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingPhone { get; set; }
        public int? TaxTypeId { get; set; }
        public int? BillingCity { get; set; }
        public int? BillingState { get; set; }
        public int? BillingZIP { get; set; }
        public string BillingFax { get; set; }
        public string BillingNPI { get; set; }
        public string TaxIDNumber { get; set; }
        public int? AcceptAssignment { get; set; }
        public string Taxonomy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public int? PracticeTypeId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status status { get; set; }
        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }
        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }
        [ForeignKey("ZIP")]
        public virtual CityStateLookup zipcode { get; set; }
        [ForeignKey("PracticeTypeId")]
        public virtual PracticeType practiceType { get; set; }
        [ForeignKey("CLIATypeId")]
        public virtual CLIAType CLIAType { get; set; }
        [ForeignKey("TaxTypeId")]
        public virtual TaxType taxType { get; set; }
    }
}
