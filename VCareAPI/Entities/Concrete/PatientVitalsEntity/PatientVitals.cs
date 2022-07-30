using Core.Entities;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientVitalsEntity
{
    public class PatientVitals : IEntity
    {
        [Key]
        public int PatientVitalsId { get; set; }
        public int? ProviderId { get; set; }
        public int PatientId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string Waist { get; set; }
        public string SystolicBP { get; set; }
        public string DiaSystolicBP { get; set; }
        public string HeartRate { get; set; }
        public string RespiratoryRate { get; set; }
        public string Temprature { get; set; }
        public string PainScale { get; set; }
        public string HeadCircumference { get; set; }
        public string Comment { get; set; }
        public int? TemplateId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider _provider { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
    }
}
