using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IProcedureGroupToPracticesRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProcedureGroupToPracticesEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using Entities.Dtos.ProcedureGroupsToPractice;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ProcedureGroupToPracticesRepository
{
   public class ProcedureGroupToPracticesRepository : EfEntityRepositoryBase<ProcedureGroupToPractices, ProjectDbContext>, IProcedureGroupToPracticesRepository
    {
        public ProcedureGroupToPracticesRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task BulkInsert(IEnumerable<ProcedureGroupToPractices> existingProcedureGroupToPracticesList, IEnumerable<ProcedureGroupToPractices> newProcedureGroupToPracticesList)
        {
            try
            {
                if (existingProcedureGroupToPracticesList.Count() > 0)
                {
                    Context.ProcedureGroupToPractices.RemoveRange(existingProcedureGroupToPracticesList);
                }

                await Context.ProcedureGroupToPractices.AddRangeAsync(newProcedureGroupToPracticesList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ProcedureGroupToPracticeListDto>> GetProcedureGroupWithProcedureSubGroup()
        {
            List<ProcedureGroupToPracticeListDto> query = new List<ProcedureGroupToPracticeListDto>();

            var sql = $@"select pg.*,psg.* from dbo.ProcedureGroup pg
                        INNER JOIN dbo.ProcedureSubGroup psg
                        ON pg.ProcedureGroupId = psg.ProcedureGroupId";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ProcedureGroupToPracticeListDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.ProcedureGroupId).ToList();
            }

            return query;
        }

        public async Task RemoveExistingList(IEnumerable<ProcedureGroupToPractices> existingProcedureGroupToPracticesList)
        {
            try
            {
                if (existingProcedureGroupToPracticesList.Count() > 0)
                {
                    Context.ProcedureGroupToPractices.RemoveRange(existingProcedureGroupToPracticesList);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
