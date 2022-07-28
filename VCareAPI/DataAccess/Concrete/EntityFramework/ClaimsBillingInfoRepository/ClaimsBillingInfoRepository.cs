using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IClaimsBillingInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.ClaimsBillingInfoRepository
{
   public class ClaimsBillingInfoRepository : EfEntityRepositoryBase<ClaimsBillingInfo, ProjectDbContext>, IClaimsBillingInfoRepository
    {
        public ClaimsBillingInfoRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
