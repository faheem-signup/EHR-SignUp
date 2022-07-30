using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class PracticeDocumentDto
    {
        public int DocmentId { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string DocumnetPath { get; set; }
        public string FileType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PracticeId { get; set; }
    }
}
