using Core.DataAccess;
using Entities.Concrete.InsuranceEntity;
using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.PracticeDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPracticesRepository
{
    public interface IPracticesRepository : IEntityRepository<Practice>
    {
        Task<IEnumerable<PracticeDto>> GetPracticeSearchParams(string LegalBusinessName, string TaxIDNumber, string BillingNPI, int? StatusId);
        Task<IEnumerable<Insurance>> GetPracticePayerSearchParams(string PayerName, int? PayerID);
        Task<List<GetPracticePayersDto>> GetPracticePayersById(int PracticeId);
    }
}
