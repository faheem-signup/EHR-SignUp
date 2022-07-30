using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProcedureGroupsToPractice
{
   public class ProcedureGroupWithProcedureSubGroupDto
    {
        public int ProcedureGroupId { get; set; }
        public string ProcedureGroupName { get; set; }
        public string ProcedureGroupCode { get; set; }
        public List<ProcedureSubGroupDto> ProcedureSubGroupDtoList { get; set; }
    }
}
