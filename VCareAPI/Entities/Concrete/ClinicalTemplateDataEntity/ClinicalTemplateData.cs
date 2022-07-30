using Core.Entities;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ClinicalTemplateDataEntity
{
    public class ClinicalTemplateData : IEntity
    {
        [Key]
        public int ClinicalTemplateId { get; set; }
        public int? TemplateId { get; set; }
        public int? PatientId { get; set; }
        public int? ProviderId { get; set; }
        public int? AppointmentId { get; set; }
        public string FormData { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider _provider { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }

        [ForeignKey("TemplateId")]
        public virtual FormTemplate _formTemplate { get; set; }
    }
}
