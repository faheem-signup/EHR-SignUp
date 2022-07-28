using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProviderSecurityCheckInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.ProviderSecurityCheckInfoRepository
{
    public class ProviderSecurityCheckInfoRepository : EfEntityRepositoryBase<ProviderSecurityCheckInfo, ProjectDbContext>, IProviderSecurityCheckInfoRepository
    {
        public ProviderSecurityCheckInfoRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ProviderSecurityCheckInfo> existingProviderSecurityCheckInfoList, IEnumerable<ProviderSecurityCheckInfo> newProviderSecurityCheckInfoList)
        {
            try
            {
                if (existingProviderSecurityCheckInfoList.Count() > 0)
                {
                    Context.ProviderSecurityCheckInfo.RemoveRange(existingProviderSecurityCheckInfoList);
                }

                await Context.ProviderSecurityCheckInfo.AddRangeAsync(newProviderSecurityCheckInfoList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
 