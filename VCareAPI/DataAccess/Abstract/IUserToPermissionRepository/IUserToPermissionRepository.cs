using Core.DataAccess;
using Entities.Concrete.UserToPermissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IUserToPermissionRepository
{
   public interface IUserToPermissionRepository : IEntityRepository<UserToPermission>
    {
        Task<IEnumerable<UserToPermission>> GetUserToPermissionList();
        Task<UserToPermission> GetById(int userTolRoleId);
    }
}
