using Core.DataAccess;
using Entities.Concrete.AuditLogEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IAuditLogRepository
{
   public interface IAuditLogRepository : IEntityRepository<AuditLog>
    {
        Task WriteLog(string LogDetail);
    }
}
