using Core.DataAccess;
using Entities.Concrete.ReferralProviderEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IReferralProviderRepository
{
   public interface IReferralProviderRepository : IEntityRepository<ReferralProvider>
    {
        Task<IEnumerable<ReferralProvider>> GetReferralProviderBySearchParams(string referralProviderName, string NPI, string phone, int zipCode);
    }
}
