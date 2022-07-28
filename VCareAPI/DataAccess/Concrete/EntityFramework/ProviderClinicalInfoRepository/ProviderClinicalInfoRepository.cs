using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProviderClinicalInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderClinicalInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.ProviderClinicalInfoRepository
{
    public class ProviderClinicalInfoRepository : EfEntityRepositoryBase<ProviderClinicalInfo, ProjectDbContext>, IProviderClinicalInfoRepository
    {
        public ProviderClinicalInfoRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
