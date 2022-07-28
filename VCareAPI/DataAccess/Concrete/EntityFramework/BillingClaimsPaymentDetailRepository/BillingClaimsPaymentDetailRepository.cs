using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IBillingClaimsPaymentDetailRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.BillingClaimsPaymentDetailRepository
{
   public class BillingClaimsPaymentDetailRepository : EfEntityRepositoryBase<BillingClaimsPaymentDetail, ProjectDbContext>, IBillingClaimsPaymentDetailRepository
    {
        public BillingClaimsPaymentDetailRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<BillingClaimsPaymentDetail> newBillingClaimsPaymentDetailList)
        {
            try
            {
                await Context.BillingClaimsPaymentDetail.AddRangeAsync(newBillingClaimsPaymentDetailList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task BulkInsertAndRemove(IEnumerable<BillingClaimsPaymentDetail> existingBillingClaimsPaymentDetail, IEnumerable<BillingClaimsPaymentDetail> newBillingClaimsPaymentDetailList)
        {
            try
            {
                if (existingBillingClaimsPaymentDetail.Count() > 0)
                {
                    Context.BillingClaimsPaymentDetail.RemoveRange(existingBillingClaimsPaymentDetail);
                }

                await Context.BillingClaimsPaymentDetail.AddRangeAsync(newBillingClaimsPaymentDetailList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
