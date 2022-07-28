using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IBillingClaimRepository;
using DataAccess.Abstract.IBillingClaimsAdditionalInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.BillingClaimsAdditionalInfoRepository
{
   public class BillingClaimsAdditionalInfoRepository : EfEntityRepositoryBase<BillingClaimsAdditionalInfo, ProjectDbContext>, IBillingClaimsAdditionalInfoRepository
    {
        public BillingClaimsAdditionalInfoRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
