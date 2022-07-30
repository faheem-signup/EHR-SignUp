using Core.DataAccess;
using Entities.Concrete.ProviderClinicalInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IProviderClinicalInfoRepository
{
    public interface IProviderClinicalInfoRepository : IEntityRepository<ProviderClinicalInfo>
    {
    }
}
