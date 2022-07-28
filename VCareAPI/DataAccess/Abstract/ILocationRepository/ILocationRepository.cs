using Core.DataAccess;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ILocationRepository
{
   public interface ILocationRepository : IEntityRepository<Locations>
    {
        Task<List<GetAllLocationsDto>> GetLocationByPracticeId(int practiceId);
    }
}
