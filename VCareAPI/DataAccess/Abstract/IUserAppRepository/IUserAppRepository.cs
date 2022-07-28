using Core.DataAccess;
using Entities.Concrete.User;
using Entities.Concrete.UserToPermissions;
using Entities.Dtos.UesrAppDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IUserAppRepository
{
   public interface IUserAppRepository : IEntityRepository<UserApp>
    {
        Task<IEnumerable<UserApp>> GetUserAppList();
        Task<List<GetAllUserAppDto>> GetUserAppBySearchParams(string name, int userId, int roleId, int statusId, int locationId);
        Task<UserAppPermissionDto> GetUserAppPermission(int userId);
        Task<UserApp> GetUserQuery(string email);
        Task<UserApp> GetUserByIdQuery(int userId);
    }
}
