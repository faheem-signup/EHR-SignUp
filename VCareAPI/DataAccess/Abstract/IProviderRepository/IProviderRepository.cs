using Core.DataAccess;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderRepository
{
   public interface IProviderRepository : IEntityRepository<Provider>
    {
        Task<IEnumerable<Provider>> GetProviderBySearchParams(string lastName, int? type, string title, int? statusId, int? locationId);
    }
}
