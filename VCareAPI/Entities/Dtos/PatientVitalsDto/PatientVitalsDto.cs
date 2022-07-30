using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientVitalsDto
{
   public class PatientVitalsDto
    {
        public int PatientVitalsId { get; set; }
        public string ProvderName { get; set; }
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
    }
}
