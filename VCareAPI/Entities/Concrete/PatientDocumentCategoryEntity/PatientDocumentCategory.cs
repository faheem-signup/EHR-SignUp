using Core.Entities;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientDocumentCategoryEntity
{
   public class PatientDocumentCategory :IEntity
    {
        [Key]
        public int PatientDocCateogryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public virtual DocumentParentCategoryLookup DocumentParentCategoryLookup { get; set; }
    }
}
