using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Abstract.IUserToProviderAssignRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.UserToProviderAssignEnity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserToProviderAssignRepository
{
    public class UserToProviderAssignRepository : EfEntityRepositoryBase<UserToProviderAssign, ProjectDbContext>, IUserToProviderAssignRepository
    {
        public UserToProviderAssignRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<UserToProviderAssign> existingUserToProviderAssign, IEnumerable<UserToProviderAssign> newUserToProviderAssignList)
        {
            try
            {
                if (existingUserToProviderAssign.Count() > 0)
                {
                    Context.UserToProviderAssign.RemoveRange(existingUserToProviderAssign);
                }
                await Context.UserToProviderAssign.AddRangeAsync(newUserToProviderAssignList);
            }
            catch (System.Exception ex)
            {
                throw ex;

            }

        }

    }
}
