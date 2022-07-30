using Core.Entities;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientEmploymentEntity
{
    public class PatientEmployment : IEntity
    {
        [Key]
        public int PatientEmploymentId { get; set; }
        public int? EmploymentStatus { get; set; }
        public int? WorkStatus { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerPhone { get; set; }
        public DateTime? AccidentDate { get; set; }
        public int? AccidentType { get; set; }
        public string Wc { get; set; }
        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
    }
}
