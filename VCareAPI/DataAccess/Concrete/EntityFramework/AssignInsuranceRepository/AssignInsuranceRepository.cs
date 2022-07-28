using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IAssignInsuranceRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using Entities.Dtos.InsuranceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.AssignInsuranceRepository
{
    public class AssignInsuranceRepository : EfEntityRepositoryBase<AssignPracticeInsurance, ProjectDbContext>, IAssignInsuranceRepository
    {
        public AssignInsuranceRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<AssignPracticeInsurance> existingList, IEnumerable<AssignPracticeInsurance> practiceInsurance)
        {
            try
            {
                if (existingList.Count() > 0)
                {
                    Context.AssignPracticeInsurance.RemoveRange(existingList);
                }

                if (practiceInsurance.Count() > 0)
                {
                    await Context.AssignPracticeInsurance.AddRangeAsync(practiceInsurance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AssignInsuranceDto>> GetAssignPracticeListAsync(int practiceId)
        {
            List<AssignInsuranceDto> query = new List<AssignInsuranceDto>();
            var sql = $@"select i.PracticeId, i.InsuranceId, api.InsuranceId as CompareInsuranceId, 
                case when api.InsuranceId is null then 0 else 1 end as InsuranceStatus,
                i.Name as InsuranceName, ipt.PayerTypeDescription as InsuranceType
                from Insurances i
                left outer join AssignPracticeInsurance api on i.InsuranceId = api.InsuranceId
                inner join InsurancePayerType ipt on i.InsurancePayerTypeId = ipt.InsurancePayerTypeId
                where ISNULL(i.IsDeleted,0)=0 and i.PracticeId = " + practiceId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AssignInsuranceDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.InsuranceId).ToList();
            }

            return query;
        }
    }
}
