using Core.DataAccess;
using Entities.Concrete.ProcedureGroupToPracticesEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using Entities.Dtos.ProcedureGroupsToPractice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProcedureGroupToPracticesRepository
{
    public interface IProcedureGroupToPracticesRepository : IEntityRepository<ProcedureGroupToPractices>
    {
        Task BulkInsert(IEnumerable<ProcedureGroupToPractices> existingProcedureGroupToPracticesList, IEnumerable<ProcedureGroupToPractices> newProcedureGroupToPracticesList);
        Task<IEnumerable<ProcedureGroupToPracticeListDto>> GetProcedureGroupWithProcedureSubGroup();
        Task RemoveExistingList(IEnumerable<ProcedureGroupToPractices> existingProcedureGroupToPracticesList);
    }
} 
