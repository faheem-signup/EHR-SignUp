using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.AuditLogEntity
{
   public class AuditLog : IEntity
    {
        [Key]
        public int AuditLogId { get; set; }
        public string LogDetail { get; set; }
    }
}
