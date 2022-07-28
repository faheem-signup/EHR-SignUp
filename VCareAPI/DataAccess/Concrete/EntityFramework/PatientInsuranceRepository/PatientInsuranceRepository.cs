using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Dtos.PatientInsurancesDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientInsuranceRepository
{
   public class PatientInsuranceRepository : EfEntityRepositoryBase<PatientInsurance, ProjectDbContext>, IPatientInsuranceRepository
    {
        public PatientInsuranceRepository(ProjectDbContext context)
            : base(context)
        {
        }


        public async Task<IEnumerable<PatientInsurance>> GetPatientInsurance(int PatientId)
        {
            var _list = await Context.PatientInsurance
                .Include(x => x.patientInsuranceType)
               .Include(x => x.patient)
                .Where(x => x.PatientId == PatientId && x.IsDeleted != true).ToListAsync();
            return _list;
        }


        public async Task<IEnumerable<PatientInsurance>> GetDeletedPatientInsurance(int PatientId)
        {
            var _list = await Context.PatientInsurance
                .Include(x => x.patientInsuranceType)
               .Include(x => x.patient)
                .Where(x => x.PatientId == PatientId && x.IsDeleted == true).ToListAsync();
            return _list;
        }


        public async Task<PatientInsurancesDto> GetPatientInsuranceById(int PatientInsuranceId)
        {
            PatientInsurancesDto query = new PatientInsurancesDto();
            var sql = "";
            if (PatientInsuranceId != null && PatientInsuranceId > 0)
            {
                sql = $@"  select pi.[PatientInsuranceId]
                    ,pi.[InsuranceType]
                    ,pi.[InsuredPay]
                    ,pi.[InsuranceName]
                    ,pi.[EligibilityDate]
                    ,pi.[InsuranceAddress]
                    ,pi.[City]
                    ,pi.[State]
                    ,pi.[Zip]
                    ,pi.[InsurancePhone]
                    ,pi.[Copay]
                    ,pi.[PolicyNumber]
                    ,pi.[GroupNumber]
                    ,pi.[SubscriberName]
                    ,pi.[SubscriberPhone]
                    ,pi.[SubscriberRelationship]
                    ,pi.[SubscriberCity]
                    ,pi.[SubscriberState]
                    ,pi.[SubscriberAddress]
                    ,pi.[SubscriberZip]
                    ,pi.[RxPayerId]
                    ,pi.[RxGroupNo]
                    ,pi.[RxBinNo]
                    ,pi.[RxPCN]
                    ,pi.[StartDate]
                    ,pi.[EndDate]
                    ,pi.[PatientId]
                    ,pi.[InsuranceImage]
                    ,pi.[InsuranceImagePath]
	                ,pit.Description as InsuranceTypeName

	                from dbo.PatientInsurance pi
	                INNER JOIN dbo.PatientInsuranceType pit
	                ON pit.PatientInsuranceTypeId = pi.InsuranceType
	                where ISNULL(pi.IsDeleted,0) = 0 and pi.PatientInsuranceId = " + PatientInsuranceId;
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientInsurancesDto>(sql).FirstOrDefault();
            }

            return query;
        }

    }
}
