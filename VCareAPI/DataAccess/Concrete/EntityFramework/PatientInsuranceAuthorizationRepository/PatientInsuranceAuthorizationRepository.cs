using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Dtos.PatientInsurancesDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientInsuranceAuthorizationRepository
{
    public class PatientInsuranceAuthorizationRepository : EfEntityRepositoryBase<PatientInsuranceAuthorization, ProjectDbContext>, IPatientInsuranceAuthorizationRepository
    {
        public PatientInsuranceAuthorizationRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PatientInsuranceAuthorization>> GetPatientInsuranceAuthorization()
        {
            var result = Context.PatientInsuranceAuthorization.Include(x => x.patientInsurance).Where(x => x.IsDeleted != true);
           // var data = Context.PatientInsuranceAuthorization.Include(x => x.patientInsurance).Where(x => x.IsDeleted != true && PatientInsuranceAuthorizationIdList.All(a=> a== x.PatientInsuranceAuthorizationId));
            return result;
            //var detail = data.Contains(PatientId)
            //        List<PatientInsuranceAuthorizationDto> query = new List<PatientInsuranceAuthorizationDto>();

            //        var sql = $@"select pa.AuthorizationNo,pa.PatientInsuranceAuthorizationId,pa.AuthorizationUnitsVsits,pa.Comment,pa.Count,
            //           pa.StartDate,pa.EndDate,pa.WarningDate,pa.PatientInsuranceId,pti.InsuranceName,'' as ProcedureCode from dbo.PatientInsuranceAuthorization pa
            //           inner join dbo.PatientInsurance pti
            //on pti.PatientInsuranceId = pa.PatientInsuranceId
            //            where ISNULL(pa.IsDeleted,0)=0 and pa.PatientInsuranceId in (select PatientInsuranceId from dbo.PatientInsurance where ISNULL(IsDeleted,0)=0 and PatientId =" + PatientId + ")";
            //        using (var connection = Context.Database.GetDbConnection())
            //        {
            //            query = connection.Query<PatientInsuranceAuthorizationDto>(sql).ToList();
            //        }



            //        query.ToList().ForEach(async x =>
            //        {
            //            List<PatientInsuranceAuthorizationCPTDto> queryCPT = new List<PatientInsuranceAuthorizationCPTDto>();

            //            var sql1 = $@"select cpt.*,prc.Code from dbo.PatientInsuranceAuthorizationCPT cpt
            //left join dbo.[Procedures] prc
            //on prc.ProcedureId = cpt.ProcedureId
            //where cpt.PatientInsuranceAuthorizationId =" + x.PatientInsuranceAuthorizationId;

            //            using (var connection = Context.Database.GetDbConnection())
            //            {
            //                queryCPT = connection.Query<PatientInsuranceAuthorizationCPTDto>(sql1).ToList();
            //            }
            //            //var cptList = Context.PatientInsuranceAuthorizationCPT.Include(a => a.procedures).Where(a => a.PatientInsuranceAuthorizationId == x.PatientInsuranceAuthorizationId);

            //            if (queryCPT != null)
            //            {
            //                var code = "";
            //                queryCPT.ToList().ForEach(b =>
            //                {
            //                    if (b.Code != null)
            //                    {
            //                        code += b.Code + ',';
            //                    }
            //                });
            //                x.ProcedureCode = code.TrimEnd(','); //string.Join(",", cptList.Select(a => (a.procedures != null ? a.procedures.Code :"")));
            //            }
            //        });



            // return query;
        }

        public async Task<PatientInsuranceAuthorization> GetPatientInsuranceAuthorizationById(int PatientInsuranceAuthorizationId)
        {
            var data = Context.PatientInsuranceAuthorization
                .Where(x => x.PatientInsuranceAuthorizationId == PatientInsuranceAuthorizationId)
                .FirstOrDefault();

            return data;
        }
    }
}
