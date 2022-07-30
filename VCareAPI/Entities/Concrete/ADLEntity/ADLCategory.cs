using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ADLEntity
{
    public class ADLCategor : IEntity
    {
        [Key]
        public int ADLCategoryId { get; set; }
        public string ADLCategoryName { get; set; }
    }
}
