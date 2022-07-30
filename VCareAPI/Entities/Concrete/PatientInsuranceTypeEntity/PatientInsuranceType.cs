using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.PatientInsuranceTypeEntity
{
   public class PatientInsuranceType : IEntity
    {
        [Key]
        public int PatientInsuranceTypeId { get; set; }
        public string Description { get; set; }
    }
}
