using Core.Entities;
using Entities.Concrete.PatientDocsFileUploadEntity;
using Entities.Concrete.PatientDocumentCategoryEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientDocumentEntity
{
  public class PatientDocument :IEntity
    {
        [Key]
        public int PatientDocumentId { get; set; }
        public int PatientDocCateogryId { get; set; }
        public int UploadDocumentId { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public int? PatientId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("PatientDocCateogryId")]
        public virtual PatientDocumentCategory PatientDocumentCategory { get; set; }
        [ForeignKey("UploadDocumentId")]
        public virtual PatientDocsFileUpload PatientDocsFileUpload { get; set; }
    }
}
