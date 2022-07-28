using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.LocationWorkConfigsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.LocationWorkConfigsRepository
{
    public class LocationWorkConfigsRepository : EfEntityRepositoryBase<LocationWorkConfigs, ProjectDbContext>, ILocationWorkConfigsRepository
    {
        public LocationWorkConfigsRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<LocationWorkConfigs> existingLocationWorkConfigsList, IEnumerable<LocationWorkConfigs> newLocationWorkConfigList)
        {
            try
            {
                if (existingLocationWorkConfigsList.Count() > 0)
                {
                    Context.LocationWorkConfigs.RemoveRange(existingLocationWorkConfigsList);
                }

                await Context.LocationWorkConfigs.AddRangeAsync(newLocationWorkConfigList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}
