using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientEducationEntity
{
    public class PatientEducationWeb : IEntity
    {
        [Key]
        public int PatientEducationWebId { get; set; }
        public string Title { get; set; }
        public string WebUrl { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
    }
}
