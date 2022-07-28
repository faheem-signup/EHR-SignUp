using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.UserToLocationAssignEntity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserToLocationAssignRepository
{
    public class UserToLocationAssignRepository : EfEntityRepositoryBase<UserToLocationAssign, ProjectDbContext>, IUserToLocationAssignRepository
    {
        public UserToLocationAssignRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<UserToLocationAssign> existingUserToLocationAssign, IEnumerable<UserToLocationAssign> newUserToLocationAssign)
        {
            try
            {
                if (existingUserToLocationAssign.Count() > 0)
                {
                    Context.UserToLocationAssign.RemoveRange(existingUserToLocationAssign);
                }
                await Context.UserToLocationAssign.AddRangeAsync(newUserToLocationAssign);
            }
            catch (System.Exception ex)
            {
                throw ex;

            }

        }

    }
}
