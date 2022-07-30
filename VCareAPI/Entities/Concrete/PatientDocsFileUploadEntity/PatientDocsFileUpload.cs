using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.PatientDocsFileUploadEntity
{
   public class PatientDocsFileUpload :IEntity
    {
        [Key]
        public int UploadDocumentId { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentName { get; set; }
        public Byte[] DocumentData { get; set; }
        public string FileType { get; set; }
    }
}
