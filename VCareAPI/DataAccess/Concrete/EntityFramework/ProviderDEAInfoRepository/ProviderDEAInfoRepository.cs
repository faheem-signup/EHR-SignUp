using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProviderDEAInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderDEAInfoEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.ProviderDEAInfoRepository
{
    public class ProviderDEAInfoRepository : EfEntityRepositoryBase<ProviderDEAInfo, ProjectDbContext>, IProviderDEAInfoRepository
    {
        public ProviderDEAInfoRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ProviderDEAInfo> existingProviderDEAInfoList, IEnumerable<ProviderDEAInfo> newProviderDEAInfoList)
        {
            try
            {
                if (existingProviderDEAInfoList.Count() > 0)
                {
                    Context.ProviderDEAInfo.RemoveRange(existingProviderDEAInfoList);
                }

                await Context.ProviderDEAInfo.AddRangeAsync(newProviderDEAInfoList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
 