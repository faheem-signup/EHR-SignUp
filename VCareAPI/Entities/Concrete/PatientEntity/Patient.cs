using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.GenderEntity;
using Entities.Concrete.LookupsEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientEntity
{
    public class Patient : IEntity
    {
        [Key]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime? DOB { get; set; }
        public int? Gender { get; set; }
        public string SSN { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? County { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? Race { get; set; }
        public int? Ethnicity { get; set; }
        public int? MaritalStatus { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public int? PreferredCom { get; set; }
        public int? PreferredLanguage { get; set; }
        public string EmergencyConName { get; set; }
        public string EmergencyConAddress { get; set; }
        public string EmergencyConRelation { get; set; }
        public string EmergencyConPhone { get; set; }
        public string Comment { get; set; }
        public byte[] PatientImage { get; set; }
        public string PatientImagePath { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status _status { get; set; }
        [ForeignKey("City")]
        public virtual CityStateLookup cities { get; set; }
        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }
        [ForeignKey("ZIP")]
        public virtual CityStateLookup zipcode { get; set; }
        [ForeignKey("County")]
        public virtual CityStateLookup counties { get; set; }
        [ForeignKey("Gender")]
        public virtual Gender _gender { get; set; }
        [ForeignKey("Race")]
        public virtual RaceLookup _race { get; set; }
        [ForeignKey("MaritalStatus")]
        public virtual MaritalStatusLookup _maritalStatus { get; set; }
        [ForeignKey("Ethnicity")]
        public virtual EthnicityLookup _ethnicity { get; set; }
        [ForeignKey("PreferredCom")]
        public virtual PreferredCommsLookup _preferredCom { get; set; }
        [ForeignKey("PreferredLanguage")]
        public virtual PreferredLanguageLookup _preferredLanguage { get; set; }
    }
}
