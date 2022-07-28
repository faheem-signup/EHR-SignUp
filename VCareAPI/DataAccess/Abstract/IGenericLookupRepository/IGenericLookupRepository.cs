using Core.DataAccess;
using Core.Entities;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IGenericLookupRepository
{
   public interface IGenericLookupRepository : IEntityRepository<IEntity>
    {
        //List<T> LoadDataForLookUps();
        IEnumerable<T> GetAllData<T>() where T : class;
    }

}
