using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IBillingClaimRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using Entities.Dtos.BillingClaimsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.BillingClaimRepository
{
   public class BillingClaimRepository : EfEntityRepositoryBase<BillingClaim, ProjectDbContext>, IBillingClaimRepository
    {
        public BillingClaimRepository(ProjectDbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<BillingClaimDto>> GetBillingClaimList()
        {
            List<BillingClaimDto> query = new List<BillingClaimDto>();

            var sql = $@"SELECT bc.[BillingClaimsId]
                     ,bc.[PatientName]
                     ,bc.[GenderId]
                     ,bc.[DOB]
                     ,bc.[Address]
                     ,bc.[InsuranceId]
                	  ,cbi.[BilledStatusId]
                     ,cbi.[CurrentStatusId]
                     ,cbi.[PrimaryInsurance]
                     ,cbi.[SecondaryInsurance]
                     ,cbi.[LocationId]
                     ,cbi.[AttendingProvider]
                     ,cbi.[SupervisingProvider]
                     ,cbi.[ReferringProvider]
                     ,cbi.[BillingProvider]
                     ,cbi.[PlaceOfService]
                	  ,bcdc.[ICDCode1]
                     ,bcdc.[ICDCode2]
                     ,bcdc.[ICDCode3]
                     ,bcdc.[ICDCode4]
                	  ,bcai.[ServiceProfileId]
                     ,bcai.[OrignalClaimNo]
                     ,bcai.[IsCorrectedClaims]
                     ,bcai.[HCFABox19]
                     ,bcai.[OnsetLMP]
                     ,bcai.[LastXRay]
                	  ,bcpi.[EOBERAId]
                     ,bcpi.[EntryDate]
                     ,bcpi.[CheckNo]
                     ,bcpi.[CheckDate]
                     ,bcpi.[CheckAmount]
                     ,bcpi.[PayerId]
                 FROM [dbo].[BillingClaim] bc
                 INNER JOIN dbo.ClaimsBillingInfo cbi
                 ON cbi.BillingClaimsId = bc.BillingClaimsId
                 INNER JOIN dbo.BillingClaimsDiagnosisCode bcdc
                 ON bcdc.BillingClaimsId = bc.BillingClaimsId
                 LEFT JOIN dbo.BillingClaimsAdditionalInfo bcai
                 ON bcai.BillingClaimsId = bc.BillingClaimsId
                 LEFT JOIN dbo.BillingClaimsPayerInfo bcpi
                 ON bcpi.BillingClaimsId = bc.BillingClaimsId

                WHERE ISNULL(bc.IsDeleted,0) = 0";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<BillingClaimDto>(sql).ToList();
            }

            return query;
        }

        public async Task<BillingClaimDto> GetBillingClaimById(int BillingClaimsId)
        {
            BillingClaimDto query = new BillingClaimDto();

            var sql = $@"SELECT bc.[BillingClaimsId]
                     ,bc.[PatientName]
                     ,bc.[GenderId]
                     ,bc.[DOB]
                     ,bc.[Address]
                     ,bc.[InsuranceId]
                	  ,cbi.[BilledStatusId]
                     ,cbi.[CurrentStatusId]
                     ,cbi.[PrimaryInsurance]
                     ,cbi.[SecondaryInsurance]
                     ,cbi.[LocationId]
                     ,cbi.[AttendingProvider]
                     ,cbi.[SupervisingProvider]
                     ,cbi.[ReferringProvider]
                     ,cbi.[BillingProvider]
                     ,cbi.[PlaceOfService]
                	  ,bcdc.[ICDCode1]
                     ,bcdc.[ICDCode2]
                     ,bcdc.[ICDCode3]
                     ,bcdc.[ICDCode4]
                	  ,bcai.[ServiceProfileId]
                     ,bcai.[OrignalClaimNo]
                     ,bcai.[IsCorrectedClaims]
                     ,bcai.[HCFABox19]
                     ,bcai.[OnsetLMP]
                     ,bcai.[LastXRay]
                	  ,bcpi.[EOBERAId]
                     ,bcpi.[EntryDate]
                     ,bcpi.[CheckNo]
                     ,bcpi.[CheckDate]
                     ,bcpi.[CheckAmount]
                     ,bcpi.[PayerId]
                 FROM [dbo].[BillingClaim] bc
                 INNER JOIN dbo.ClaimsBillingInfo cbi
                 ON cbi.BillingClaimsId = bc.BillingClaimsId
                 INNER JOIN dbo.BillingClaimsDiagnosisCode bcdc
                 ON bcdc.BillingClaimsId = bc.BillingClaimsId
                 LEFT JOIN dbo.BillingClaimsAdditionalInfo bcai
                 ON bcai.BillingClaimsId = bc.BillingClaimsId
                 LEFT JOIN dbo.BillingClaimsPayerInfo bcpi
                 ON bcpi.BillingClaimsId = bc.BillingClaimsId

                WHERE ISNULL(bc.IsDeleted,0) = 0 AND bc.BillingClaimsId="+ BillingClaimsId;
            var connection = Context.Database.GetDbConnection();
            query = connection.Query<BillingClaimDto>(sql).FirstOrDefault();
           
            return query;
        }
    }
}
