using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientEducationEntity
{
    public class PatientEducationDocument : IEntity
    {
        [Key]
        public int PatientEducationDocumentId { get; set; }
        public string ICDCode { get; set; }
        public string DocumentURL { get; set; }
        public string DocumentName { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
    }
}
