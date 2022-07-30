using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProcedureGroupsToPractice
{
  public class ProcedureGroupToPracticeListDto
    {
        public int ProcedureGroupId { get; set; }
        public string ProcedureGroupName { get; set; }
        public string ProcedureGroupCode { get; set; }
        public int ProcedureSubGroupId { get; set; }
        public string ProcedureSubGroupName { get; set; }
        public string ProcedureSubGroupCode { get; set; }
    }
}
