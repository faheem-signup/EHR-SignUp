using Core.DataAccess;
using Entities.Concrete.InsuranceEntity;
using Entities.Dtos.InsuranceDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IInsuranceRepository
{
    public interface IInsuranceRepository : IEntityRepository<Insurance>
    {
        Task<List<InsuranceDto>> GetInsuranceSearchParams(int PracticeId, int? PayerId, string Name, int? City, int? State, int? ZIP);
    }
}
