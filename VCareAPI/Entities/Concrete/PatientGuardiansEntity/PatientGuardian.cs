using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientGuardiansEntity
{
    public class PatientGuardian : IEntity
    {
        [Key]
        public int GuardianId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string GuardianAddress { get; set; }
        public string GuardianPhone { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
    }
}
