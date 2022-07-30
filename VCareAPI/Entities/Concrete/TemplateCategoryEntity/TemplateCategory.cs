using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.TemplateCategoryEntity
{
    public class TemplateCategory : IEntity
    {
        [Key]
        public int TemplateCategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
