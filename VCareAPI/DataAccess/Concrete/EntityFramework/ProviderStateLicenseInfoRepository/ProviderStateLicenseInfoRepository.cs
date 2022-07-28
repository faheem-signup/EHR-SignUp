using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProviderStateLicenseInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.ProviderStateLicenseInfoRepository
{
    public class ProviderStateLicenseInfoRepository : EfEntityRepositoryBase<ProviderStateLicenseInfo, ProjectDbContext>, IProviderStateLicenseInfoRepository
    {
        public ProviderStateLicenseInfoRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ProviderStateLicenseInfo> existingProviderStateLicenseInfoList, IEnumerable<ProviderStateLicenseInfo> newProviderStateLicenseInfoList)
        {
            try
            {
                if (existingProviderStateLicenseInfoList.Count() > 0)
                {
                    Context.ProviderStateLicenseInfo.RemoveRange(existingProviderStateLicenseInfoList);
                }

                await Context.ProviderStateLicenseInfo.AddRangeAsync(newProviderStateLicenseInfoList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
 