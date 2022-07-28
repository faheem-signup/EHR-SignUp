using Core.DataAccess;
using Entities.Concrete.PracticeDocsEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPracticeDocsRepository
{
    public interface IPracticeDocsRepository : IEntityRepository<PracticeDoc>
    {
        Task<IEnumerable<PracticeDoc>> GetPracticeDocsByPracticeId(int practiceId);
    }
}
