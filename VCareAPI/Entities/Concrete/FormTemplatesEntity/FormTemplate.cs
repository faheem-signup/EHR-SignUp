using Core.Entities;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.TemplateCategoryEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.FormTemplatesEntity
{
    public class FormTemplate : IEntity
    {
        [Key]
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

        [ForeignKey("ProviderId")]
        public virtual Provider provider { get; set; }

        [ForeignKey("TemplateCategoryId")]
        public virtual TemplateCategory templateCategory { get; set; }
    }
}
