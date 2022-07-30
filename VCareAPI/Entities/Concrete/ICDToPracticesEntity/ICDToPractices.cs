using Core.Entities;
using Entities.Concrete.ICDCategoryEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ICDToPracticesEntity
{
    public class ICDToPractices : IEntity
    {
        [Key]
        public int ICDToPracticesId { get; set; }
        public int ICDCategoryId { get; set; }
        public int PracticeId { get; set; }
        [ForeignKey("ICDCategoryId")]
        public virtual ICDCategory ICDCategory { get; set; }
        [ForeignKey("PracticeId")]
        public virtual Practice Practice { get; set; }
    }
}
