using Core.DataAccess;
using Entities.Concrete.TBLPageEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.ITBLPageRepository
{
   public interface ITBLPageRepository : IEntityRepository<TblPage>
    {
    }
}
