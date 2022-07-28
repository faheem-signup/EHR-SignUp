using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.ITBLPageRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.TBLPageEntity;
using Entities.Dtos.TblPageDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.TBLPageRepository
{
   public class TBLPageRepository : EfEntityRepositoryBase<TblPage, ProjectDbContext>, ITBLPageRepository
    {
        public TBLPageRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TblSubPageDto>> GetAllModulesList()
        {
            List<TblPageDto> query = new List<TblPageDto>();
            List<TblSubPageDto> tblSubPageList = new List<TblSubPageDto>();
            var sql = $@"select tp.PageId, tp.PageName, tsp.SubPageId,  tsp.SubpageName
                    from TblPage tp
                    inner join TblSubPage tsp on tp.PageId = tsp.PageId";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<TblPageDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                var pageIdList = query.Select(x => x.PageId).Distinct();

                foreach (var pageId in pageIdList)
                {
                    TblSubPageDto subPage = new TblSubPageDto();
                    var data = query.Where(x => x.PageId == pageId).ToList();
                    if (data.Count() > 0)
                    {
                        List<TblPageDto> pagesList = data.ConvertAll(a =>
                        {
                            return new TblPageDto()
                            {
                                PageId = a.PageId,
                                PageName = a.PageName,
                                SubPageId = a.SubPageId,
                                SubpageName = a.SubpageName,
                            };
                        });

                        subPage.PageId = data[0].PageId;
                        subPage.PageName = data[0].PageName;
                        subPage._tblPageList = pagesList;

                        tblSubPageList.Add(subPage);
                    }
                }
            }

            return tblSubPageList;
        }
    }
}
