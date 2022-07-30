using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ServiceProfileDto
{
    public class ServiceProfilesDto
    {
        public int? ServiceProfileId { get; set; }
        public string ServiceProfileName { get; set; }
        public Guid? Row_Id { get; set; }
        public string DiagnosisCode { get; set; }
        public string ProcedureCode { get; set; }
        public string Template { get; set; }
    }
}
