using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IDiagnosisRepository
{
   public interface IDiagnosisRepository : IEntityRepository<DiagnosisCode>
    {
        Task<IEnumerable<DiagnosisCode>> GetDiagnosesList(int practiceId);
    }
}
