using Core.DataAccess;
using Entities.Concrete.UserToProviderAssignEnity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IUserToProviderAssignRepository
{
   public interface IUserToProviderAssignRepository : IEntityRepository<UserToProviderAssign>
    {
        Task BulkInsert(IEnumerable<UserToProviderAssign> existingUserToProviderAssign, IEnumerable<UserToProviderAssign> newUserToProviderAssignList);
    }
}
