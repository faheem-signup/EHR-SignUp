using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPracticesRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.InsuranceEntity;
using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.InsuranceDto;
using Entities.Dtos.PracticeDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PracticesRepository
{
    public class PracticesRepository : EfEntityRepositoryBase<Practice, ProjectDbContext>, IPracticesRepository
    {
        public PracticesRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<List<GetPracticePayersDto>> GetPracticePayersById(int PracticeId)
        {
            List<GetPracticePayersDto> query = new List<GetPracticePayersDto>();
            var sql = $@"select * from PracticePayers where PracticeId =" + PracticeId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetPracticePayersDto>(sql).ToList();
            }

            return query;
        }

        public async Task<IEnumerable<Insurance>> GetPracticePayerSearchParams(string PayerName, int? PayerID)
        {
            var query = Context.Insurances
                .Include(x => x.insurancePayerType)
                .ToList();

            if (!string.IsNullOrEmpty(PayerName))
            {
                query = query.Where(s => s.Name == PayerName).ToList();
            }
            if (PayerID > 0)
            {
                query = query.Where(s => s.PayerId == PayerID).ToList();
            }

            return query;
        }

        public async Task<IEnumerable<PracticeDto>> GetPracticeSearchParams(string LegalBusinessName, string TaxIDNumber, string BillingNPI, int? StatusId)
        {
            List<PracticeDto> query = new List<PracticeDto>();
            List<GetPracticesLocationsDto> sqlQueryData = new List<GetPracticesLocationsDto>();

            var sql = $@"select p.PracticeId, p.LegalBusinessName, p.TaxIDNumber, p.BillingNPI, p.StatusId, 
					case when s.StatusName is null then 'InActive' else s.StatusName end as StatusName,
                    l.LocationId, l.LocationName, l.Address, l.Email, l.Fax, l.NPI, l.Phone
					from Practices p
                    left outer join Statuses s on p.StatusId = s.StatusId
                    left outer join Locations l on p.PracticeId = l.PracticeId
                    where ISNULL(p.IsDeleted,0)=0";

            if (!string.IsNullOrEmpty(LegalBusinessName))
            {
                sql += "and p.LegalBusinessName like '%" + LegalBusinessName + "%'";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                sqlQueryData = connection.Query<GetPracticesLocationsDto>(sql).ToList();

                if (sqlQueryData.Count() > 0)
                {
                    var practiceIdList = sqlQueryData.Select(x => x.PracticeId).Distinct();

                    foreach (var practiceId in practiceIdList)
                    {
                        PracticeDto practice = new PracticeDto();
                        var data = sqlQueryData.Where(x => x.PracticeId == practiceId).ToList();
                        if (data.Count() > 0)
                        {
                            List<PracticeLocationDto> LocationList = data.ConvertAll(a =>
                            {
                                return new PracticeLocationDto()
                                {
                                    LocationId = a.LocationId,
                                    LocationName = a.LocationName,
                                    Address = a.Address,
                                    Fax = a.Fax,
                                    NPI = a.NPI,
                                    Phone = a.Phone,
                                    Email = a.Email,
                                    PracticeId = a.PracticeId,
                                };
                            });

                            practice.PracticeId = data[0].PracticeId;
                            practice.LegalBusinessName = data[0].LegalBusinessName;
                            practice.TaxIDNumber = data[0].TaxIDNumber;
                            practice.BillingNPI = data[0].BillingNPI;
                            practice.StatusId = data[0].StatusId;
                            practice.StatusName = data[0].StatusName;
                            practice.practiceLocations = LocationList;

                            query.Add(practice);
                        }
                    }
                }
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.PracticeId).ToList();
                if (!string.IsNullOrEmpty(TaxIDNumber))
                {
                    query = query.Where(s => s.TaxIDNumber == TaxIDNumber).ToList();
                }

                if (!string.IsNullOrEmpty(BillingNPI))
                {
                    query = query.Where(s => s.BillingNPI == BillingNPI).ToList();
                }

                if (StatusId > 0)
                {
                    query = query.Where(s => s.StatusId == StatusId).ToList();
                }
            }

            return query;
        }

        public async Task<IEnumerable<InsuranceLookupDto>> GetPracticeInsuranceList(int PracticeId)
        {
            List<InsuranceLookupDto> query = new List<InsuranceLookupDto>();

            //var sql = $@"SELECT  [InsuranceId]
            //         ,[Name] as InsuranceName
            //     FROM [dbo].[Insurances] where ISNULL(IsDeleted,0) = 0 and PracticeId ="+ PracticeId;

            var sql = $@"SELECT distinct api.[InsuranceId]
                         ,ins.Name as InsuranceName
                     FROM [dbo].[AssignPracticeInsurance] api 
                     INNER JOIN dbo.Insurances ins
                     ON ins.InsuranceId = api.InsuranceId
                     where api.PracticeId =" + PracticeId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<InsuranceLookupDto>(sql).ToList();
            }

            return query;
        }

        public async Task<IEnumerable<InsuranceLookupDto>> GetCredentialedInsurancesList(int ProviderId)
        {
            List<InsuranceLookupDto> query = new List<InsuranceLookupDto>();

            var sql = $@"SELECT [ProviderClinicalInfoId] as InsuranceId
                         ,[CredentialedInsurances] as InsuranceName
                     FROM [dbo].[ProviderClinicalInfo]
                     where ISNULL(IsDeleted,0) = 0 and ProviderId=" + ProviderId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<InsuranceLookupDto>(sql).ToList();
            }

            return query;
        }
    }
}
