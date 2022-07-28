using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProviderBoardCertificationInfoRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.ProviderBoardCertificationInfoRepository
{
    public class ProviderBoardCertificationInfoRepository : EfEntityRepositoryBase<ProviderBoardCertificationInfo, ProjectDbContext>, IProviderBoardCertificationInfoRepository
    {
        public ProviderBoardCertificationInfoRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ProviderBoardCertificationInfo> existingProviderBoardCertificationInfoList, IEnumerable<ProviderBoardCertificationInfo> newProviderBoardCertificationInfoList)
        {
            try
            {
                if (existingProviderBoardCertificationInfoList.Count() > 0)
                {
                    Context.ProviderBoardCertificationInfo.RemoveRange(existingProviderBoardCertificationInfoList);
                }

                await Context.ProviderBoardCertificationInfo.AddRangeAsync(newProviderBoardCertificationInfoList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
 