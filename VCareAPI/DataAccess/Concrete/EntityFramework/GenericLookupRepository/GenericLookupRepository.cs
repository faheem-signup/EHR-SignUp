using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract.IGenericLookupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.GenericLookupRepository
{
   public class GenericLookupRepository : EfEntityRepositoryBase<IEntity, ProjectDbContext>, IGenericLookupRepository
    {
        public GenericLookupRepository(ProjectDbContext context) : base(context)
        {

        }
        public IEnumerable<T> GetAllData<T>() where T : class
        {
            var data = Context.Set<T>().AsEnumerable<T>();

            return data;
        }

    }
}
