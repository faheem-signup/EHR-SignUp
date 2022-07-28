using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IInsuranceRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.InsuranceEntity;
using Entities.Dtos.InsuranceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.InsuranceRepository
{
    public class InsuranceRepository : EfEntityRepositoryBase<Insurance, ProjectDbContext>, IInsuranceRepository
    {
        public InsuranceRepository(ProjectDbContext context)
    : base(context)
        {
        }

        public async Task<List<InsuranceDto>> GetInsuranceSearchParams(int PracticeId, int? PayerId, string Name, int? City, int? State, int? ZIP)
        {
            List<InsuranceDto> query = new List<InsuranceDto>();

            var sql = $@"select i.*, ipt.PayerTypeDescription, c.ID, c.CityId, c.CityName, c.County, c.CountyId, c.StateId, c.StateCode, c.State as StateName,
                c.ZipId, c.ZipCode
                from Insurances i
                inner join Practices p on i.PracticeId = p.PracticeId and ISNULL(p.IsDeleted,0)=0
                left outer join InsurancePayerType ipt on i.InsurancePayerTypeId = ipt.InsurancePayerTypeId
                left outer join CityStateLookup c on i.City = c.CityId
                where ISNULL(i.IsDeleted,0)=0 and i.PracticeId =" + PracticeId;

            using (var connection = Context.Database.GetDbConnection())
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    sql += " and i.Name like '%" + Name + "%'";
                }

                query = connection.Query<InsuranceDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.InsuranceId).ToList();

                if (PayerId > 0)
                {
                    query = query.Where(s => s.PayerId == PayerId).ToList();
                }

                if (City > 0)
                {
                    query = query.Where(s => s.City == City).ToList();
                }

                if (State > 0)
                {
                    query = query.Where(s => s.State == State).ToList();
                }

                if (ZIP > 0)
                {
                    query = query.Where(s => s.ZIP == ZIP).ToList();
                }
            }

            return query;
        }
    }
}
