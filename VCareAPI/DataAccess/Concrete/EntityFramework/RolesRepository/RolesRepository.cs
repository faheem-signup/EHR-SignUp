using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IRolesRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.RolesRepository
{
   public class RolesRepository : EfEntityRepositoryBase<Roles, ProjectDbContext>, IRolesRepository
    {
        public RolesRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
