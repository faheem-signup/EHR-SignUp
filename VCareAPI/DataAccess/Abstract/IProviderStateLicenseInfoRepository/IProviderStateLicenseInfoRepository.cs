using Core.DataAccess;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderStateLicenseInfoRepository
{
   public interface IProviderStateLicenseInfoRepository : IEntityRepository<ProviderStateLicenseInfo>
    {
        Task BulkInsert(IEnumerable<ProviderStateLicenseInfo> existingProviderStateLicenseInfoList, IEnumerable<ProviderStateLicenseInfo> newProviderStateLicenseInfoList);
    }
}
 