using Core.DataAccess;
using Entities.Concrete.LocationWorkConfigsEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ILocationWorkConfigsRepository
{
   public interface ILocationWorkConfigsRepository : IEntityRepository<LocationWorkConfigs>
    {
        Task BulkInsert(IEnumerable<LocationWorkConfigs> existingLocationWorkConfigsList, IEnumerable<LocationWorkConfigs> newLocationWorkConfigList);
    }
}
