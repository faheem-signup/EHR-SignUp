using Core.Entities;
using Entities.Concrete.PracticesEntity;
using Entities.Concrete.ProcedureGroupEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProcedureGroupToPracticesEntity
{
    public class ProcedureGroupToPractices : IEntity
    {
        [Key]
        public int ProcedureToGroupId { get; set; }
        public int PracticeId { get; set; }
        public int ProcedureGroupId { get; set; }
        public int ProcedureSubGroupId { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }

        [ForeignKey("ProcedureGroupId")]
        public virtual ProcedureGroup procedureGroup { get; set; }

        [ForeignKey("ProcedureSubGroupId")]
        public virtual ProcedureSubGroup procedureSubGroup { get; set; }

    }
}
