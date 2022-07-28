using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IUserWorkHourConfigRepository
{
   public interface IUserWorkHourRepository : IEntityRepository<UserWorkHour>
    {

        Task BulkInsert(IEnumerable<UserWorkHour> existingUserWorkHour, IEnumerable<UserWorkHour> newUserWorkHourList);
        Task<IEnumerable<UserWorkHour>> GetUserWorkHourByUserId(int userId);
    }
}
