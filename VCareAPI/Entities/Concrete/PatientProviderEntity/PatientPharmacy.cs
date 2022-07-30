using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientProviderEntity
{
    public class PatientPharmacy : IEntity
    {
        [Key]
        public int PharmacyID { get; set; }
        public string PharmacyDescription { get; set; }
        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
    }
}
