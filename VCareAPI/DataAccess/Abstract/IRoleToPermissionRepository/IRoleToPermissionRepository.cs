using Core.DataAccess;
using Entities.Concrete.RoleToPermissionEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IRoleToPermissionRepository
{
   public interface IRoleToPermissionRepository: IEntityRepository<RoleToPermission>
    {
        Task<IEnumerable<RoleToPermission>> GetRoleToPermissionListByRoleId(int roleId);
    }
}
