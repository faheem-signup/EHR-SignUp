using Core.DataAccess;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderBoardCertificationInfoRepository
{
   public interface IProviderBoardCertificationInfoRepository : IEntityRepository<ProviderBoardCertificationInfo>
    {
        Task BulkInsert(IEnumerable<ProviderBoardCertificationInfo> existingProviderBoardCertificationInfoList, IEnumerable<ProviderBoardCertificationInfo> newProviderBoardCertificationInfoList);
    }
}
 