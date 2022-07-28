using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IReferralProviderRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Dtos.ReferralProviderDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ReferralProviderRepository
{
    public class ReferralProviderRepository : EfEntityRepositoryBase<ReferralProvider, ProjectDbContext>, IReferralProviderRepository
    {
        public ReferralProviderRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReferralProviderDto>> GetReferralProviderBySearchParams(string referralProviderName, string nPI, string phone, int? zipCode)
        {
            List<ReferralProviderDto> query = new List<ReferralProviderDto>();
            var sql = $@"drop table if exists #tempReferralProvider
                select rp.ReferralProviderId, rp.FirstName + ' ' + rp.LastName as FirstName, rp.NPI, rp.Address, rp.Phone, rp.ZIP
                into #tempReferralProvider
                from ReferralProvider rp
                select * from #tempReferralProvider t";

            if (!string.IsNullOrEmpty(referralProviderName))
            {
                sql += " where t.FirstName like '%" + referralProviderName + "%'";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ReferralProviderDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.ReferralProviderId).ToList();
                if (!string.IsNullOrEmpty(nPI))
                {
                    query = query.Where(s => s.NPI == nPI).ToList();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    query = query.Where(s => s.Phone == phone).ToList();
                }

                if (zipCode > 0)
                {
                    query = query.Where(s => s.ZIP == zipCode).ToList();
                }
            }

            return query;
        }
    }
}
