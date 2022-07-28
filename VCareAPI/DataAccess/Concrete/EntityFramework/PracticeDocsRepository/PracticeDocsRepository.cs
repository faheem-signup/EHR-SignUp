
using DataAccess.Abstract.IPracticeDocsRepository;
using Entities.Concrete.PracticeDocsEntity;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DataAccess.Concrete.EntityFramework.PracticeDocsRepository
{
    public class PracticeDocsRepository : EfEntityRepositoryBase<PracticeDoc, ProjectDbContext>, IPracticeDocsRepository
    {
        public PracticeDocsRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PracticeDoc>> GetPracticeDocsByPracticeId(int practiceId)
        {
            var list = await Context.PracticeDocs.Where(x => x.PracticeId == practiceId && x.isDeleted != true).ToListAsync();
            var convertList = list.ConvertAll(a =>
            {
                return new PracticeDoc()
                {
                    DocmentId = a.DocmentId,
                    DocumentName = a.DocumentName,
                    CreatedDate = a.CreatedDate,
                    Description = a.Description,
                    PracticeId = a.PracticeId,
                    DocumnetPath = a.DocumnetPath,
                    FileType = a.FileType,
                };
            });

            if (convertList.Count() > 0)
            {
                convertList = convertList.OrderByDescending(x => x.DocmentId).ToList();
            }

            return convertList;
        }
    }
}
