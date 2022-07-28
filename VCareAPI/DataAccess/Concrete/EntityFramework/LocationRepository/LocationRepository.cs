using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.LocationRepository
{
   public class LocationRepository : EfEntityRepositoryBase<Locations, ProjectDbContext>, ILocationRepository
    {
        public LocationRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<List<GetAllLocationsDto>> GetLocationByPracticeId(int? practiceId)
        {
            List<GetAllLocationsDto> query = new List<GetAllLocationsDto>();
            var sql = $@"select * from Locations l";
            if (practiceId != null && practiceId > 0)
            {
                sql = $@"select l.*, p.LegalBusinessName as PracticeName, s.StatusName, c.ID, c.CityId, c.CityName, c.County, c.CountyId, c.StateId, c.StateCode, c.State as StateName,
                    c.ZipId, c.ZipCode
                    from Locations l
                    inner join Practices p on l.PracticeId = p.PracticeId and ISNULL(p.IsDeleted,0)=0
                    left outer join Statuses s on l.StatusId = s.StatusId
                    left outer join CityStateLookup c on l.City = c.CityId
                    where l.PracticeId =" + practiceId;
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetAllLocationsDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.LocationId).ToList();
            }

            return query;
        }

        public async Task<GetLocationData> GetLocationWorkConfigsByLocationId(int locationId)
        {
            GetLocationData query = new GetLocationData();
            GetLocationByIdDto location = new GetLocationByIdDto();
            List<LocationWorkConfigsDto> LocationWorkConfigs = new List<LocationWorkConfigsDto>();

            var sqlLocation = $@"select * from Locations where LocationId =" + locationId;
            var sql = $@"select * from LocationWorkConfigs where LocationId =" + locationId;

            using (var connection = Context.Database.GetDbConnection())
            {
                location = connection.Query<GetLocationByIdDto>(sqlLocation).FirstOrDefault();
                LocationWorkConfigs = connection.Query<LocationWorkConfigsDto>(sql).ToList();
            }

            if (location != null)
            {
                query.location = location;
                query.locationWorkConfigsList = LocationWorkConfigs;
            }

            return query;
        }
    }
}
