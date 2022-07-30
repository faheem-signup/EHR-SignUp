using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.DocumentParentCategoryLookupeEntity
{
   public class DocumentParentCategoryLookup :IEntity
    {
        [Key]
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
