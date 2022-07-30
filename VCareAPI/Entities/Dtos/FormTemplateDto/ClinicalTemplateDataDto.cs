using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.FormTemplateDto
{
    public class ClinicalTemplateDataDto
    {
        public int ClinicalTemplateId { get; set; }
        public int? TemplateId { get; set; }
        public int? PatientId { get; set; }
        public int? ProviderId { get; set; }
        public int? AppointmentId { get; set; }
        public string FormData { get; set; }
    }
}
