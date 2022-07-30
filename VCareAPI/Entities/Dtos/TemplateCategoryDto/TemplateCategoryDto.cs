using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.TemplateCategoryDto
{
    public class TemplateCategoryDto
    {
        public int TemplateCategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<TemplateDto> TemplateList { get; set; }
    }
}
