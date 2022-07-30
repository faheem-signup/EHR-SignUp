using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProcedureDto
{
    public class ProcedureDto
    {
        public int ProcedureId { get; set; }
        public string Code { get; set; }
        public string NDCNumber { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? POSId { get; set; }
        public bool IsExpired { get; set; }
        public string IsActive { get; set; }
        public DateTime? Date { get; set; }
        public decimal DefaultCharges { get; set; }
        public decimal MedicareAllowance { get; set; }
        public int? PracticeId { get; set; }
        public string PosDescription { get; set; }
        public string PracticeName { get; set; }
        public string ProcedureGroupName { get; set; }
        public int? ProcedureGroupId { get; set; }
    }
}
