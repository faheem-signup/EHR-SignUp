using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.ProviderDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.ProviderWorkConfigRepository
{
    public class ProviderWorkConfigRepository : EfEntityRepositoryBase<ProviderWorkConfig, ProjectDbContext>, IProviderWorkConfigRepository
    {
        public ProviderWorkConfigRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task BulkInsert(IEnumerable<ProviderWorkConfig> existingProviderWorkConfigList, IEnumerable<ProviderWorkConfig> newProviderWorkConfigList)
        {
            try
            {
                if (existingProviderWorkConfigList.Count() > 0)
                {
                    Context.ProviderWorkConfig.RemoveRange(existingProviderWorkConfigList);
                }

                await Context.ProviderWorkConfig.AddRangeAsync(newProviderWorkConfigList);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<ProviderWorkConfig> GetAllProviderWorkConfigById(int ProviderId, DateTime CurrentDate)
        {
            var result = Context.ProviderWorkConfig
                .Where(x => x.ProviderId == ProviderId && x.Days == CurrentDate.ToString("dddd"))
                .FirstOrDefault();

            return result;
        }

        public async Task<List<ProviderWorkConfig>> GetProviderWorkConfigByProviderId(int ProviderId)
        {
            List<GetProviderWorkConfigDto> list = new List<GetProviderWorkConfigDto>();
            var sql = $@"select * from ProviderWorkConfig where ProviderId =" + ProviderId;

            using (var connection = Context.Database.GetDbConnection())
            {
                list = connection.Query<GetProviderWorkConfigDto>(sql).ToList();
            }

            List<ProviderWorkConfig> providerWorkConfigList = list.ConvertAll(a =>
            {
                return new ProviderWorkConfig()
                {
                    Id = a.Id,
                    ProviderId = a.ProviderId,
                    LocationId = a.LocationId,
                    Days = a.Days,
                    FirstShiftWorkFrom = a.FirstShiftWorkFrom,
                    FirstShiftWorkTo = a.FirstShiftWorkTo,
                    BreakShiftWorkFrom = a.BreakShiftWorkFrom,
                    BreakShiftWorkTo = a.BreakShiftWorkTo,
                    SlotSize = string.IsNullOrEmpty(a.SlotSize) ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(a.SlotSize),
                    IsBreak = a.IsBreak,
                };
            });

            return providerWorkConfigList;

        }


        public async Task<List<ProviderWorkConfig>> GetProviderWorkConfigListById(int ProviderId)
        {
            //var result = Context.ProviderWorkConfig.Where(x => x.ProviderId == ProviderId);

            //return result;

            List<ProviderWorkConfig> query = new List<ProviderWorkConfig>();
            var sql = $@"SELECT * from dbo.ProviderWorkConfig
                        where ProviderId =" + ProviderId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<ProviderWorkConfig>(sql).ToList();
            //using (var connection = Context.Database.GetDbConnection())
            //{
            //    query = connection.Query<ProviderWorkConfig>(sql).ToList();
            //}
            return query;
        }

        public async Task<List<GetProviderWorkConfigDto>> GetProviderWorkConfigListDetail(int ProviderId, int LocationId)
        {

            List<GetProviderWorkConfigDto> query = new List<GetProviderWorkConfigDto>();
            var sql = $@"SELECT [Id]
                        ,[ProviderId]
                        ,[LocationId]
                        ,[Days]
                        ,[FirstShiftWorkFrom]
                        ,[FirstShiftWorkTo]
                        ,[BreakShiftWorkFrom]
                        ,[BreakShiftWorkTo]
                        ,cast([SlotSize] as nvarchar) as SlotSize
                        ,[IsBreak] from dbo.ProviderWorkConfig
                        where ProviderId =" + ProviderId + "and LocationId =" + LocationId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<GetProviderWorkConfigDto>(sql).ToList();

            return query;
        }

    }
}
