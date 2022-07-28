using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientProviderEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientProvideReferralRepository
{
    public class PatientProvideReferralRepository : EfEntityRepositoryBase<PatientProvideReferring, ProjectDbContext>, IPatientProvideReferralRepository
    {
        public PatientProvideReferralRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task BulkInsert(IEnumerable<PatientProvideReferring> existingPatientProvideReferral, IEnumerable<PatientProvideReferring> newPatientProvideReferralList)
        {
            try
            {
                if (existingPatientProvideReferral.Count() > 0)
                {
                    Context.PatientProvideReferring.RemoveRange(existingPatientProvideReferral);
                }

                await Context.PatientProvideReferring.AddRangeAsync(newPatientProvideReferralList);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PatientProvideReferringDto>> GetPatientProviderReferringList(int PatientId)
        {
            List<PatientProvideReferringDto> query = new List<PatientProvideReferringDto>();
            var sql = $@"select * from PatientProvideReferring where PatientId = " + PatientId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientProvideReferringDto>(sql).ToList();
            }

            return query;
        }

        public async Task<List<PatientReferralProviderDto>> GetPatientReferralProviderList(int PatientProviderId)
        {
            List<PatientReferralProviderDto> query = new List<PatientReferralProviderDto>();
            var sql = $@"select ppr.ReferralProviderId, rp.FirstName + ' '+ rp.LastName as ReferralName 
                    from PatientProvideReferring ppr
                    left outer join ReferralProvider rp on ppr.ReferralProviderId = rp.ReferralProviderId
                    where ppr.PatientProviderId = " + PatientProviderId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientReferralProviderDto>(sql).ToList();
            }

            return query;
        }
    }
}
