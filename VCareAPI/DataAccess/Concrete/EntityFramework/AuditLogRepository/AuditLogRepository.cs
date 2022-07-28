using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IAuditLogRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AuditLogEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.AuditLogRepository
{
   public class AuditLogRepository : EfEntityRepositoryBase<AuditLog, ProjectDbContext>, IAuditLogRepository
    {
        public AuditLogRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task WriteLog(string LogDetail)
        {
            AuditLog auditLogObj = new AuditLog();

            auditLogObj.LogDetail = LogDetail;
             Context.Add(auditLogObj);
            Context.SaveChangesAsync();

        }
    }
}
