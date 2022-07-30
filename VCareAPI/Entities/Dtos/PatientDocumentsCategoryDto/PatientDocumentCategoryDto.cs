using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDocumentsCategoryDto
{
  public class PatientDocumentCategoryDto
    {
        public int PatientDocCateogryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
