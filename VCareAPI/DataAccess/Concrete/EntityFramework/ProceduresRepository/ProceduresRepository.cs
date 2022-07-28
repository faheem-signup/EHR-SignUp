using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IProceduresRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProceduresEntity;
using Entities.Dtos.ProcedureDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ProceduresRepository
{
   public class ProceduresRepository : EfEntityRepositoryBase<Procedure, ProjectDbContext>, IProceduresRepository
    {
        public ProceduresRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProcedureDto>> GetAllProcedure(int PracticeId, string Code, string Description, int? CPTGroupId)
        {
            List<ProcedureDto> query = new List<ProcedureDto>();
            string sql = $@"select pr.*, case when IsExpired = 1 then 'Active' else 'InActive' end as IsActive,
                        ps.[Description] as PosDescription, p.LegalBusinessName as PracticeName, pg.ProcedureGroupName
                        from Procedures pr
                        left outer join Practices p on pr.PracticeId = p.PracticeId and ISNULL(p.IsDeleted,0)=0
                        left outer join POS ps on pr.POSId = ps.POSId
                        Inner Join ProcedureGroup pg On pr.ProcedureGroupId = pg.ProcedureGroupId
                        where pr.PracticeId =" + PracticeId;

            using (var connection = Context.Database.GetDbConnection())
            {
                if (!string.IsNullOrEmpty(Description))
                {
                    sql += " and pr.Description like '%" + Description + "%'";
                }

                query = connection.Query<ProcedureDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.PracticeId).ToList();

                if (!string.IsNullOrEmpty(Code))
                {
                    query = query.Where(x => x.Code == Code).ToList();
                }

                if (CPTGroupId > 0)
                {
                    query = query.Where(x => x.ProcedureGroupId == CPTGroupId).ToList();
                }
            }

            return query;
        }
    }
}
