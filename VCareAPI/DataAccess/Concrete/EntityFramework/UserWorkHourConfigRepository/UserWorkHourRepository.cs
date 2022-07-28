using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserWorkHourConfigRepository
{
    public class UserWorkHourRepository : EfEntityRepositoryBase<UserWorkHour, ProjectDbContext>, IUserWorkHourRepository
    {
        public UserWorkHourRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<UserWorkHour> existingUserWorkHour, IEnumerable<UserWorkHour> newUserWorkHourList)
        {
            try
            {
                if (existingUserWorkHour.Count() > 0)
                {
                    Context.UserWorkHourConfig.RemoveRange(existingUserWorkHour);
                }
                await Context.UserWorkHourConfig.AddRangeAsync(newUserWorkHourList);
            }
            catch (System.Exception ex)
            {
                throw ex;

            }

        }

        public async Task<IEnumerable<UserWorkHour>> GetUserWorkHourByUserId(int userId)
        {
            var _list = await Context.UserWorkHourConfig.Where(x => x.UserId == userId).ToListAsync();
            return _list;
        }
    }
}
