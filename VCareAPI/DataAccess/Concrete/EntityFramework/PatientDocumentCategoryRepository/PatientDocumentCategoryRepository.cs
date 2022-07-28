using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDocumentCategoryEntity;
using Entities.Dtos.PatientDocumentsCategoryDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientDocumentCategoryRepository
{
   public class PatientDocumentCategoryRepository : EfEntityRepositoryBase<PatientDocumentCategory, ProjectDbContext>, IPatientDocumentCategoryRepository
    {
        public PatientDocumentCategoryRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PatientDocumentCategoryDto>> GetPatientDocumentCategory()
        {
            List<PatientDocumentCategoryDto> query = new List<PatientDocumentCategoryDto>();

            var sql = $@"select pdc.PatientDocCateogryId,pdc.ParentCategoryId,pdc.CategoryName,dpcl.Name as ParentCategoryName
                        from dbo.PatientDocumentCategory pdc
                        INNER JOIN dbo.DocumentParentCategoryLookup dpcl ON dpcl.ParentCategoryId = pdc.ParentCategoryId";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDocumentCategoryDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.PatientDocCateogryId).ToList();
            }

            return query;
        }
    }
}
