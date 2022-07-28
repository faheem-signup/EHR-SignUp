using Core.DataAccess;
using Entities.Concrete.ReminderProfileEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IReminderProfileRepository
{
    public interface IReminderProfileRepository : IEntityRepository<ReminderProfile>
    {
    }
}
