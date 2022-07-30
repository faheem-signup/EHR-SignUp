using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Entities.Concrete.ADLEntity
{
    public class ADLFunction : IEntity
    {
        [Key]
        public int ADLFunctionId { get; set; }
        public int? ADLCategoryId { get; set; }
        public string ADLFunctionName { get; set; }

        [ForeignKey("ADLCategoryId")]
        public virtual ADLCategor _ADLCategor { get; set; }
    }
}
