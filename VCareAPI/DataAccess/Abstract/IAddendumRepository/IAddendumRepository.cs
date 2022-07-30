using Core.DataAccess;
using Entities.Concrete.AddendumEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IAddendumRepository
{
    public interface IAddendumRepository : IEntityRepository<Addendum>
    {
    }
}
