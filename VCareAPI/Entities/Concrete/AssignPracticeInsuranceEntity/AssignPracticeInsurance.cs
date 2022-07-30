using Core.Entities;
using Entities.Concrete.InsuranceEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.AssignPracticeInsuranceEntity
{
    public class AssignPracticeInsurance : IEntity
    {
        [Key]
        public int PracticeInsuranceId { get; set; }
        public int? InsuranceId { get; set; }
        public int? PracticeId { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }

        [ForeignKey("InsuranceId")]
        public virtual Insurance insurance { get; set; }
    }
}
