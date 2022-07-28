using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IReminderProfileRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ReminderProfileEntity;
using Entities.Dtos.ReminderProfileDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ReminderProfileRepository
{
    public class ReminderProfileRepository : EfEntityRepositoryBase<ReminderProfile, ProjectDbContext>, IReminderProfileRepository
    {
        public ReminderProfileRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<ReminderProfileDto>> GetAllReminderProfile()
        {
            List<ReminderProfileDto> query = new List<ReminderProfileDto>();

            var sql = $@"select rp.* , rd.ReminderDaysDescription, rt.[Description] as ReminderTypeDescription
                    from ReminderProfiles rp
                    left outer join ReminderDaysLookup rd on rp.ReminderDaysLookupId = rd.ReminderDaysLookupId
                    left outer join ReminderType rt on rp.ReminderTypeId = rt.ReminderTypeId
                    where ISNULL(rp.IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ReminderProfileDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.ReminderProfileId).ToList();
            }

            return query;
        }

        public async Task<GetReminderProfileByIdDto> GetReminderProfileById(int ReminderProfileId)
        {
            GetReminderProfileByIdDto query = new GetReminderProfileByIdDto();

            var sql = $@"select ReminderProfileId, ReminderTypeId, IsBefore, [Count], ReminderDaysLookupId, Details from ReminderProfiles";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetReminderProfileByIdDto>(sql).FirstOrDefault();
            }

            return query;
        }
    }
}
