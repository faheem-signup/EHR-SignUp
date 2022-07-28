using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract.ICityStateLookupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.CityStateLookupEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.CityStateLookupRepository
{
   public class CityStateLookupRepository : EfEntityRepositoryBase<CityStateLookup, ProjectDbContext>, ICityStateLookupRepository
    {
        public CityStateLookupRepository(ProjectDbContext context) : base(context)
        {

        }

        //public List<IEntity> GetAllData<TEntity>(IEntity ientity) where TEntity : class
        //{
        //    return Context.Set<TEntity>().ToList();
        //}
    }
}
