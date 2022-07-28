using Core.DataAccess;
using Entities.Concrete.PracticePayersEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPracticePayersRepository
{
    public interface IPracticePayersRepository : IEntityRepository<PracticePayer>
    {
        Task<IEnumerable<PracticePayer>> GetPracticePayersByPracticeId(int practiceId);
        Task BulkInsert(IEnumerable<PracticePayer> existingList, IEnumerable<PracticePayer> practicePayers);
    }
}
