using Core.DataAccess;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IBillingClaimsAdditionalInfoRepository
{
  public  interface IBillingClaimsAdditionalInfoRepository :IEntityRepository<BillingClaimsAdditionalInfo>
    {
    }
}
