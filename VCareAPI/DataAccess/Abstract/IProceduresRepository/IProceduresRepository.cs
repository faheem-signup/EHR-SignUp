using Core.DataAccess;
using Entities.Concrete.ProceduresEntity;
using Entities.Dtos.ProcedureDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProceduresRepository
{
   public interface IProceduresRepository : IEntityRepository<Procedure>
    {
        Task<IEnumerable<ProcedureDto>> GetAllProcedure(int PracticeId);
    }
}
