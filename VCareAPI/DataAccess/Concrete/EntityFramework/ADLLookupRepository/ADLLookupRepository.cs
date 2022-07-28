using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IADLLookupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ADLEntity;
using Entities.Dtos.ADLFunctionDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ADLLookupRepository
{
    public class ADLLookupRepository : EfEntityRepositoryBase<ADLLookup, ProjectDbContext>, IADLLookupRepository
    {
        public ADLLookupRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<List<GetADLListDto>> GetAllADLFunctionList(int ProviderId, int PatientId)
        {
            List<GetADLListDto> queryList = new List<GetADLListDto>();
            List<ADLFunctionDto> query = new List<ADLFunctionDto>();
            List<ADLCategor> queryCat = new List<ADLCategor>();
            List<ADLLookupDto> query2 = new List<ADLLookupDto>();
            List<ADLFunctionListDto> query3 = new List<ADLFunctionListDto>();
            var sql1 = $@"select * from ADLLookup where ProviderId = " + ProviderId + " and PatientId = " + PatientId;
            var sqlADL = $@"select * from ADLFunction";
            var sqlCat = $@"select * from ADLCategory";
            var sql = $@"select distinct c.ADLCategoryId,c.ADLCategoryName, af.ADLFunctionId, af.ADLFunctionName, 
                case when adl.Independent is null then 0 else adl.Independent end as Independent, 
                case when adl.NeedsHelp is null then 0 else adl.NeedsHelp end as NeedsHelp, 
                case when adl.Dependent is null then 0 else adl.Dependent end as Dependent, 
                case when adl.CannotDo is null then 0 else adl.CannotDo end as CannotDo
                from ADLCategory c
                inner join ADLFunction af on c.ADLCategoryId = af.ADLCategoryId
                inner join ADLLookup adl on af.ADLFunctionId = adl.ADLFunctionId 
                where adl.ProviderId = " + ProviderId + " and adl.PatientId = " + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query2 = connection.Query<ADLLookupDto>(sql1).ToList();
                if (query2.Count > 0)
                {
                    query = connection.Query<ADLFunctionDto>(sql).ToList();
                    queryCat = connection.Query<ADLCategor>(sqlCat).ToList();

                    foreach (var item in queryCat)
                    {
                        GetADLListDto n = new GetADLListDto();
                        var obj = query.Where(x => x.ADLCategoryId == item.ADLCategoryId).ToList();
                        List<ADLFunctionDto> query5 = new List<ADLFunctionDto>();
                        query5 = obj.ConvertAll(a =>
                        {
                            return new ADLFunctionDto()
                            {
                                ADLFunctionId = a.ADLFunctionId,
                                ADLCategoryId = a.ADLCategoryId,
                                ADLFunctionName = a.ADLFunctionName,
                                Independent = a.Independent,
                                Dependent = a.Dependent,
                                NeedsHelp = a.NeedsHelp,
                                CannotDo = a.CannotDo,
                            };
                        });

                        n.ADLCategoryId = item.ADLCategoryId;
                        n.ADLCategoryName = item.ADLCategoryName;
                        n._ADLFunctionList = query5;
                        queryList.Add(n);
                    }
                }
                else
                {
                    query3 = connection.Query<ADLFunctionListDto>(sqlADL).ToList();
                    queryCat = connection.Query<ADLCategor>(sqlCat).ToList();

                    foreach (var item in queryCat)
                    {
                        GetADLListDto n = new GetADLListDto();
                        var obj = query3.Where(x=> x.ADLCategoryId == item.ADLCategoryId).ToList();
                        List<ADLFunctionDto> query6 = new List<ADLFunctionDto>();
                        query6 = obj.ConvertAll(a =>
                        {
                            return new ADLFunctionDto()
                            {
                                ADLFunctionId = a.ADLFunctionId,
                                ADLCategoryId = a.ADLCategoryId,
                                ADLFunctionName = a.ADLFunctionName,
                                Independent = false,
                                Dependent = false,
                                NeedsHelp = false,
                                CannotDo = false,
                            };
                        });

                        n.ADLCategoryId = item.ADLCategoryId;
                        n.ADLCategoryName = item.ADLCategoryName;
                        n._ADLFunctionList = query6;
                        queryList.Add(n);
                    }
                }
            }

            return queryList;
        }
    }
}
