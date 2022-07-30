using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ICDCategoryEntity
{
   public class ICDCategory :IEntity
    {
        [Key]
        public int ICDCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
    }
}
