using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IDiagnosisRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos.DiagnosisCodeDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.DiagnosisRepository
{
    public class DiagnosisRepository : EfEntityRepositoryBase<DiagnosisCode, ProjectDbContext>, IDiagnosisRepository
    {
        public DiagnosisRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<DiagnosisCodeDto>> GetDiagnosesList(int PracticeId, int? ICDCategoryId, string Code, string ShortDescription, string Description)
        {
            List<DiagnosisCodeDto> query = new List<DiagnosisCodeDto>();

            var sql = $@"select dc.DiagnosisId, dc.Code, dc.ShortDescription, dc.Description, dc.ICDCategoryId, ic.CategoryName as ICDGroupName
                from DiagnosisCodes dc
                inner join ICDCategory ic on dc.ICDCategoryId = ic.ICDCategoryId
                where ISNULL(dc.IsDeleted,0)=0 and dc.PracticeId = " + PracticeId;

            if (!string.IsNullOrEmpty(Code))
            {
                sql += " and dc.Code like '%" + Code + "%'";
            }

            if (!string.IsNullOrEmpty(ShortDescription))
            {
                sql += " and dc.ShortDescription like '%" + ShortDescription + "%'";
            }

            if (!string.IsNullOrEmpty(Description))
            {
                sql += " and dc.Description like '%" + Description + "%'";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<DiagnosisCodeDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.DiagnosisId).ToList();
                if (ICDCategoryId > 0)
                {
                    query = query.Where(x => x.ICDCategoryId == ICDCategoryId).ToList();
                }
            }

            return query;
        }
    }
}
