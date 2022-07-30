using Core.DataAccess;
using Entities.Concrete.BillingClaim;
using Entities.Dtos.BillingClaimsDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IBillingClaimRepository
{
    public interface IBillingClaimRepository : IEntityRepository<BillingClaim>
    {
        Task<IEnumerable<BillingClaimDto>> GetBillingClaimList();
        Task<BillingClaimDto> GetBillingClaimById(int BillingClaimsId);
    }
}
