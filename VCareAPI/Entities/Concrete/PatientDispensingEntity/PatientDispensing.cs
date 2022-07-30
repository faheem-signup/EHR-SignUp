using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientDispensingEntity
{
    public class PatientDispensing : IEntity
    {
        [Key]
        public int DispensingId { get; set; }
        public int PatientId { get; set; }
        public int? ProgramId { get; set; }
        public string DrugUsage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MainBottle { get; set; }
        public decimal? TotalDispensed { get; set; }
        public int? TotalDispensedUnitId { get; set; }
        public decimal? Remaining { get; set; }
        public int? RemainingUnitId { get; set; }
        public decimal? TotalQuantity { get; set; }
        public int? TotalQuantityUnitId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }

    }
}
