using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IBillingClaimsPayerInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.BillingClaimsPayerInfoRepository
{
   public class BillingClaimsPayerInfoRepository : EfEntityRepositoryBase<BillingClaimsPayerInfo, ProjectDbContext>, IBillingClaimsPayerInfoRepository
    {
        public BillingClaimsPayerInfoRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
