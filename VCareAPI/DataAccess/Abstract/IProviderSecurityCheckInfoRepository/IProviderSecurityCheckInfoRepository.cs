using Core.DataAccess;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderSecurityCheckInfoRepository
{
   public interface IProviderSecurityCheckInfoRepository : IEntityRepository<ProviderSecurityCheckInfo>
    {
        Task BulkInsert(IEnumerable<ProviderSecurityCheckInfo> existingProviderSecurityCheckInfoList, IEnumerable<ProviderSecurityCheckInfo> newProviderSecurityCheckInfoList);
    }
}
 