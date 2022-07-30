using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.ReferralProviderTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ReferralProviderEntity
{
    public class ReferralProvider : IEntity
    {
        [Key]
        public int ReferralProviderId { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Speciality { get; set; }
        public string TaxID { get; set; }
        public string License { get; set; }
        public string SSN { get; set; }
        public string NPI { get; set; }
        public string ContactPerson { get; set; }
        public string Comments { get; set; }
        public string ReferralProviderType { get; set; }

        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }
        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }
        [ForeignKey("ZIP")]
        public virtual CityStateLookup zipcode { get; set; }
    }
}
