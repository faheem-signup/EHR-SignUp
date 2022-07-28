using Core.DataAccess;
using Entities.Concrete.SchedulerEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.ISchedulerRepository
{
    public interface ISchedulerRepository : IEntityRepository<AppointmentScheduler>
    {
    }
}
