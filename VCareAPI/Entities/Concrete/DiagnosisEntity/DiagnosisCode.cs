using System;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Entities.Concrete.PracticesEntity;
using Entities.Concrete.ICDCategoryEntity;

#nullable disable

namespace Entities.Concrete
{
    public class DiagnosisCode : IEntity
    {
        [Key]
        public int DiagnosisId { get; set; }
        public int? ICDCategoryId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PracticeId { get; set; }
        public int StatusId { get; set; }
       
        [ForeignKey("StatusId")]
        public virtual Status status { get; set; }
       
        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }
        [ForeignKey("ICDCategoryId")]
        public virtual ICDCategory ICDCategory { get; set; }
    }
}
