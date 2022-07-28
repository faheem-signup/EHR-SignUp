using Core.DataAccess;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Dtos.ServiceProfileDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IServiceProfileRepository
{
  public interface IServiceProfileRepository : IEntityRepository<ServiceProfile>
    {
        Task BulkInsert(List<ServiceProfile> serviceProfileList);
        Task<IEnumerable<ServiceProfile>>  GetByRowId(string Row_Id);
        Task<IEnumerable<ServiceProfilesDto>> GetAllServiceProfile();
        Task RemoveServiceProfile(string Row_Id);
    }
}
