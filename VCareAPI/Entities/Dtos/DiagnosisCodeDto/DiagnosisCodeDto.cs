using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.DiagnosisCodeDto
{
   public class DiagnosisCodeDto
    {
        public int DiagnosisId { get; set; }
        public int? ICDCategoryId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ICDGroupName { get; set; }
    }
}
