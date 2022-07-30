using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.FormTemplateDto
{
    public class NotesDto
    {
        public int ClinicalTemplateId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int TemplateId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string ProviderName { get; set; }
        public string TemplateName { get; set; }
    }
}
