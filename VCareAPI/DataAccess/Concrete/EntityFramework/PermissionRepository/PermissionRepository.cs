using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPermissionRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PermissionRepository
{
   public class PermissionRepository : EfEntityRepositoryBase<Permissions, ProjectDbContext>, IPermissionRepository
    {
        public PermissionRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
