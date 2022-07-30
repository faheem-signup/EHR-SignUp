using Core.Entities;
using Entities.Concrete.Location;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientProviderEntity
{
    public class PatientProvider : IEntity
    {
        [Key]
        public int PatientProviderId { get; set; }
        public string AttendingPhysician { get; set; }
        public string SupervisingProvider { get; set; }
        //public int? ReferringProvider { get; set; }
        //public int? PCPName { get; set; }
        //public string Pharmacy { get; set; }
        //public int? ReferringAgency { get; set; }
        //public int? DrugsAgency { get; set; }
        //public int? ProbationOfficer { get; set; }
        public int? PatientId { get; set; }
        public int? LocationId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
        [ForeignKey("LocationId")]
        public virtual Locations _locations { get; set; }
    }
}
