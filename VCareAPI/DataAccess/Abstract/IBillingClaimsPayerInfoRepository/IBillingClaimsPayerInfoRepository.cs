using Core.DataAccess;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IBillingClaimsPayerInfoRepository
{
    public interface IBillingClaimsPayerInfoRepository : IEntityRepository<BillingClaimsPayerInfo>
    {
    }
}
