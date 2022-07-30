using Core.DataAccess;
using Entities.Concrete.BillingClaim;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IBillingClaimsPaymentDetailRepository
{
    public interface IBillingClaimsPaymentDetailRepository : IEntityRepository<BillingClaimsPaymentDetail>
    {
        Task BulkInsert(IEnumerable<BillingClaimsPaymentDetail> newBillingClaimsPaymentDetailList);
        Task BulkInsertAndRemove(IEnumerable<BillingClaimsPaymentDetail> existingBillingClaimsPaymentDetail, IEnumerable<BillingClaimsPaymentDetail> newBillingClaimsPaymentDetailList);
    }
}
