using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPracticePayersRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PracticePayersEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PracticePayersRepository
{
    public class PracticePayersRepository : EfEntityRepositoryBase<PracticePayer, ProjectDbContext>, IPracticePayersRepository
    {
        public PracticePayersRepository(ProjectDbContext context)
            : base(context)
        {
        }   

        public async Task BulkInsert(IEnumerable<PracticePayer> existingList, IEnumerable<PracticePayer> practicePayers)
        {
            try
            {
                if (existingList.Count() > 0)
                {
                    Context.PracticePayers.RemoveRange(existingList);
                }

                await Context.PracticePayers.AddRangeAsync(practicePayers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PracticePayer>> GetPracticePayersByPracticeId(int practiceId)
        {
            var _list = await Context.PracticePayers.Where(x => x.PracticeId == practiceId).ToListAsync();
            return _list;
        }
    }
}
