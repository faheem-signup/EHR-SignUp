using Core.Entities;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PracticeDocsEntity
{
    public class PracticeDoc : IEntity
    {
        [Key]
        public int DocmentId { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string DocumnetPath { get; set; }
        public string FileType { get; set; }
        public byte[] DocumentData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? isDeleted { get; set; }
        public int? PracticeId { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }
    }
}
