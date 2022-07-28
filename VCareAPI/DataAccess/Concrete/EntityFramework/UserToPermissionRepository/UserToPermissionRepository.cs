using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IUserToPermissionRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.UserToPermissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserToPermissionRepository
{
   public class UserToPermissionRepository : EfEntityRepositoryBase<UserToPermission, ProjectDbContext>, IUserToPermissionRepository
    {
        public UserToPermissionRepository(ProjectDbContext context) : base(context)
        {
        }

        public Task<UserToPermission> GetById(int userTolRoleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserToPermission>> GetUserToPermissionList()
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<UserToPermission>> GetUserToPermissionList()
        //{
        //    return await Context.UserToPermission.Include(a => a.Roles).Include(b => b.userApp).Include(c => c.Permissions).ToListAsync();
        //}

        //public async Task<UserToPermission> GetById(int userToPermissionId)
        //{
        //    return await Context.UserToPermission.Include(a => a.Roles).Include(b => b.userApp).Include(c => c.Permissions).Where(x=>x.UserToPermissionId == userToPermissionId).FirstOrDefaultAsync();
        //}
    }
}
