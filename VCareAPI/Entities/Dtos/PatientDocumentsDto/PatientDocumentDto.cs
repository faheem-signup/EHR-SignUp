using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDocumentsDto
{
   public class PatientDocumentDto
    {
        public int PatientDocumentId { get; set; }
        public int PatientDocCateogryId { get; set; }
        public int UploadDocumentId { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public int? PatientId { get; set; }
        public string DocumentPath { get; set; }
        public string CategoryName { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentData { get; set; }
        public string CreatedDate { get; set; }
        public string FileType { get; set; }
    }
}
