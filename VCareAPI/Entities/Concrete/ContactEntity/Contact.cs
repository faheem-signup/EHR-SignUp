using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.ContactTypeEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ContactEntity
{
    public class Contact : IEntity
    {
        [Key]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        //public int? PracticeId { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? StatusId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ContactTypeId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status status { get; set; }
        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }
        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }
        [ForeignKey("ZIP")]
        public virtual CityStateLookup zipcode { get; set; }
        [ForeignKey("ContactTypeId")]
        public virtual ContactType ContactType { get; set; }
    }
}
