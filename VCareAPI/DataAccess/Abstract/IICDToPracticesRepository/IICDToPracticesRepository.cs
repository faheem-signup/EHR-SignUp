using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.ICDToPracticesEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IICDToPracticesRepository
{
   public interface IICDToPracticesRepository : IEntityRepository<ICDToPractices>
    {
        Task BulkInsert(IEnumerable<ICDToPractices> existingICDToPracticesList, IEnumerable<ICDToPractices> newICDToPracticesList);
        //Task<IEnumerable<DiagnosisCode>> GetDiagnosesAndICDCategoryByPracticeId(int practiceId);
    }
}
