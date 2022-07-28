using Core.DataAccess;
using Entities.Concrete.ProviderWorkConfigEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderWorkConfigRepository
{
   public interface IProviderWorkConfigRepository : IEntityRepository<ProviderWorkConfig>
    {
        Task BulkInsert(IEnumerable<ProviderWorkConfig> existingProviderWorkConfigList, IEnumerable<ProviderWorkConfig> newProviderWorkConfigList);
        Task<ProviderWorkConfig> GetAllProviderWorkConfigById(int ProviderId, DateTime CurrentDate);
    }
}
