using Core.DataAccess;
using Entities.Concrete.RoomEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IRoomRepository
{
    public interface IRoomRepository : IEntityRepository<Room>
    {
    }
}
