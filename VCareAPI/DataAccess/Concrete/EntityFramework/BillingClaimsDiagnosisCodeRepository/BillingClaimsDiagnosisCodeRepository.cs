using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IBillingClaimRepository;
using DataAccess.Abstract.IBillingClaimsDiagnosisCodeRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.BillingClaimsDiagnosisCodeRepository
{
   public class BillingClaimsDiagnosisCodeRepository : EfEntityRepositoryBase<BillingClaimsDiagnosisCode, ProjectDbContext>, IBillingClaimsDiagnosisCodeRepository
    {
        public BillingClaimsDiagnosisCodeRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
