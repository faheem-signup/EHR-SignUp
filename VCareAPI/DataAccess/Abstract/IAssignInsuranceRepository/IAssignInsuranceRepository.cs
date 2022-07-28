using Core.DataAccess;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IAssignInsuranceRepository
{
    public interface IAssignInsuranceRepository : IEntityRepository<AssignPracticeInsurance>
    {
        Task<IEnumerable<AssignPracticeInsurance>> GetAssignPracticeListAsync(int practiceId);
    }
}
