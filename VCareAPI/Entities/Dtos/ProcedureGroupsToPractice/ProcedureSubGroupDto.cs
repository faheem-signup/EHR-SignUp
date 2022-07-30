using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProcedureGroupsToPractice
{
   public class ProcedureSubGroupDto
    {
        public int ProcedureSubGroupId { get; set; }
        public string ProcedureSubGroupName { get; set; }
        public string ProcedureSubGroupCode { get; set; }
        public int ProcedureGroupId { get; set; }
    }
}
