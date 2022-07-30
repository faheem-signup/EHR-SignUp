using Core.Entities;
using Entities.Concrete.ProcedureGroupEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProcedureSubGroupEntity
{
    public class ProcedureSubGroup : IEntity
    {
        [Key]
        public int ProcedureSubGroupId { get; set; }
        public string ProcedureSubGroupName { get; set; }
        public string ProcedureSubGroupCode { get; set; }
        public int ProcedureGroupId { get; set; }

        [ForeignKey("ProcedureGroupId")]
        public virtual ProcedureGroup ProcedureGroup { get; set; }
    }
}
