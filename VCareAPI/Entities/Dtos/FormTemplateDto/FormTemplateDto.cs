using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.FormTemplateDto
{
   public class FormTemplateDto
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateFilePath { get; set; }
        public string TemplateHtml { get; set; }
        public string JsonHtml { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ProviderId { get; set; }
        public int? TemplateCategoryId { get; set; }
        public string ProviderName { get; set; }
        public string TemplateCategory { get; set; }
    }
}
