using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ISchedulerStatusRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.SchedulerStatusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.SchedulerStatusRepository
{
    public class SchedulerStatusRepository : EfEntityRepositoryBase<SchedulerStatus, ProjectDbContext>, ISchedulerStatusRepository
    {
        public SchedulerStatusRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<SchedulerStatus> GetSchedulerStatusById(string status)
        {
            var schedulerStatusObj = Context.SchedulerStatuses
                .Where(x => x.SchedulerStatusName == status)
                .FirstOrDefault();

            return schedulerStatusObj;
        }
    }
}
