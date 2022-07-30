using Core.Entities;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PracticePayersEntity
{
    public class PracticePayer : IEntity
    {
        [Key]
        public int PracticePayerId { get; set; }
        public string PayerName { get; set; }
        public int PayerId { get; set; }
        public string TypeQualifier { get; set; }
        public string Location { get; set; }
        public int? PracticeId { get; set; }

        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }
    }
}
