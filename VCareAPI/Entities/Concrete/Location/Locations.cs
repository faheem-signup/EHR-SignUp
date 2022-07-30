using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.POSEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.Location
{
    public class Locations : IEntity
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string NPI { get; set; }
        public int? POSId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }
        public int? PracticeId { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice Practice { get; set; }
        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }
        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }
        [ForeignKey("ZIP")]
        public virtual CityStateLookup zipcode { get; set; }
        [ForeignKey("POSId")]
        public virtual POS POS { get; set; }
    }
}
