using Core.DataAccess;
using Entities.Concrete.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPermissionRepository
{
    public interface IPermissionRepository : IEntityRepository<Permissions>
    {
    }
}
