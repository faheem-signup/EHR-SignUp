using Core.DataAccess;
using Entities.Concrete.ADLEntity;
using Entities.Dtos.ADLFunctionDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IADLLookupRepository
{
    public interface IADLLookupRepository : IEntityRepository<ADLLookup>
    {
        Task<List<GetADLListDto>> GetAllADLFunctionList(int ProviderId, int PatientId);
    }
}
