using Core.DataAccess;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IProviderDEAInfoRepository
{
   public interface IProviderDEAInfoRepository : IEntityRepository<ProviderDEAInfo>
    {
        Task BulkInsert(IEnumerable<ProviderDEAInfo> existingProviderDEAInfoList, IEnumerable<ProviderDEAInfo> newProviderDEAInfoList);
    }
}
 