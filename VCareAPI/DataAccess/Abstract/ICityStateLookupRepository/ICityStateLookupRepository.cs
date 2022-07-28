using Core.DataAccess;
using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.ICityStateLookupRepository
{
   public interface ICityStateLookupRepository : IEntityRepository<CityStateLookup>
    {
        //List<IEntity> GetAllData<TEntity>(IEntity ientity);
    }
}
