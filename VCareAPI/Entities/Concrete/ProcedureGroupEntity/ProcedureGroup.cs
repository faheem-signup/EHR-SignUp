using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ProcedureGroupEntity
{
    public class ProcedureGroup : IEntity
    {
        [Key]
        public int ProcedureGroupId { get; set; }
        public string ProcedureGroupName { get; set; }
        public string ProcedureGroupCode { get; set; }
    }
}
