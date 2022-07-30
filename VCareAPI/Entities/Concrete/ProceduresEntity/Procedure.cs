using Core.Entities;
using Entities.Concrete.POSEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Concrete.ProcedureGroupEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProceduresEntity
{
    public class Procedure : IEntity
    {
        [Key]
        public int ProcedureId { get; set; }
        public string Code { get; set; }
        public string NDCNumber { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? POSId { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? Date { get; set; }
        public decimal? DefaultCharges { get; set; }
        public decimal? MedicareAllowance { get; set; }
        public int? PracticeId { get; set; }
        public int? ProcedureGroupId { get; set; }
        public int? ProcedureSubGroupId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }

        [ForeignKey("POSId")]
        public virtual POS pos { get; set; }

        [ForeignKey("ProcedureGroupId")]
        public virtual ProcedureGroup procedureGroup { get; set; }

        [ForeignKey("ProcedureSubGroupId")]
        public virtual ProcedureSubGroup procedureSubGroup { get; set; }
    }
}
