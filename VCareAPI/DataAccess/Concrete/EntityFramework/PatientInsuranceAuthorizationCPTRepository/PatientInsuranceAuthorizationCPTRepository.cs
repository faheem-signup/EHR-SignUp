using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Dtos.PatientInsurancesDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientInsuranceAuthorizationCPTRepository
{
    public class PatientInsuranceAuthorizationCPTRepository : EfEntityRepositoryBase<PatientInsuranceAuthorizationCPT, ProjectDbContext>, IPatientInsuranceAuthorizationCPTRepository
    {
        public PatientInsuranceAuthorizationCPTRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<PatientInsuranceAuthorizationCPT> existingPatientInsuranceAuthorizationCPT, IEnumerable<PatientInsuranceAuthorizationCPT> newPatientInsuranceAuthorizationCPT)
        {
            try
            {
                if (existingPatientInsuranceAuthorizationCPT.Count() > 0)
                {
                    Context.PatientInsuranceAuthorizationCPT.RemoveRange(existingPatientInsuranceAuthorizationCPT);
                }
                await Context.PatientInsuranceAuthorizationCPT.AddRangeAsync(newPatientInsuranceAuthorizationCPT);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<PatientInsuranceAuthorizationCPT>> GetPatientInsuranceAuthorizationCPTList(int PatientInsuranceAuthorizationId)
        {
            var PatientInsuranceAuthorizationCPTList = Context.PatientInsuranceAuthorizationCPT.Include(x => x.procedures).ToList();
            if (PatientInsuranceAuthorizationId != 0)
            {
                PatientInsuranceAuthorizationCPTList = PatientInsuranceAuthorizationCPTList.Where(x => x.PatientInsuranceAuthorizationId == PatientInsuranceAuthorizationId).ToList();
                if (PatientInsuranceAuthorizationCPTList.Count() > 0)
                {
                    return PatientInsuranceAuthorizationCPTList;
                }
                else
                {
                    return null;
                }
            }

            return null;

            //        List<PatientInsuranceAuthorizationCPTDto> queryCPT = new List<PatientInsuranceAuthorizationCPTDto>();

            //        var sql1 = $@"select cpt.*,prc.Code from dbo.PatientInsuranceAuthorizationCPT cpt
            //left join dbo.[Procedures] prc
            //on prc.ProcedureId = cpt.ProcedureId
            //where cpt.PatientInsuranceAuthorizationId =" + PatientInsuranceAuthorizationId;

            //        using (var connection = Context.Database.GetDbConnection())
            //        {
            //            queryCPT = connection.Query<PatientInsuranceAuthorizationCPTDto>(sql1).ToList();
            //        }
            //        return queryCPT;
        }
    }
}
