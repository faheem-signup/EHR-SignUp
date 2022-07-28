using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Location;
using Entities.Concrete.User;
using Entities.Concrete.UserToLocationAssignEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IUserToLocationAssignRepository
{
   public interface IUserToLocationAssignRepository : IEntityRepository<UserToLocationAssign>
    {
        Task BulkInsert(IEnumerable<UserToLocationAssign> existingUserToLocationAssign, IEnumerable<UserToLocationAssign> newUserToLocationAssign);
    }
}
