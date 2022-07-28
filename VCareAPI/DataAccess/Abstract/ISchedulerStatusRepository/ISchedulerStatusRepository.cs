using Core.DataAccess;
using Entities.Concrete.SchedulerStatusEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ISchedulerStatusRepository
{
    public interface ISchedulerStatusRepository : IEntityRepository<SchedulerStatus>
    {
        Task<SchedulerStatus> GetSchedulerStatusById(string status);
    }
}
