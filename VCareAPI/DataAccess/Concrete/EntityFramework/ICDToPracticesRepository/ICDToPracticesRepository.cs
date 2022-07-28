using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IICDToPracticesRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.ICDToPracticesEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ICDToPracticesRepository
{
    public class ICDToPracticesRepository : EfEntityRepositoryBase<ICDToPractices, ProjectDbContext>, IICDToPracticesRepository
    {
        public ICDToPracticesRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ICDToPractices> existingICDToPracticesList, IEnumerable<ICDToPractices> newICDToPracticesList)
        {
            try
            {
                if (existingICDToPracticesList.Count() > 0)
                {
                    Context.ICDToPractices.RemoveRange(existingICDToPracticesList);
                }

                await Context.ICDToPractices.AddRangeAsync(newICDToPracticesList);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task RemoveExistingList(IEnumerable<ICDToPractices> existingICDToPracticesList)
        {
            try
            {
                if (existingICDToPracticesList.Count() > 0)
                {
                    Context.ICDToPractices.RemoveRange(existingICDToPracticesList);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //public async Task<IEnumerable<DiagnosisCode>> GetDiagnosesAndICDCategoryByPracticeId(int practiceId)
        //{
        //    var _list = await Context.DiagnosisCodes
        //        .Include(x => x.ICDCategory)
        //       .Include(x => x.PracticeId)
        //        .Where(x => x.PracticeId == practiceId).ToListAsync();
        //    return _list;
        //}
    }
}
