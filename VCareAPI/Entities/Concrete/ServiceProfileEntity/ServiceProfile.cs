using Core.Entities;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Concrete.ProceduresEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ServiceProfileEntity
{
    public class ServiceProfile : IEntity
    {
        public int ServiceProfileId { get; set; }
        public string ServiceProfileName { get; set; }
        public Guid? Row_Id { get; set; }
        public int? ICDId { get; set; }
        public int? ProcedureId { get; set; }
        public int? TemplateId { get; set; }

        [ForeignKey("ICDId")]
        public virtual DiagnosisCode DiagnosisCode { get; set; }
        [ForeignKey("ProcedureId")]
        public virtual Procedure Procedure { get; set; }
        [ForeignKey("TemplateId")]
        public virtual FormTemplate FormTemplate { get; set; }
    }
}
