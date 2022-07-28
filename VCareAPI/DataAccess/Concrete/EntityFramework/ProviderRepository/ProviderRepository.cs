using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IProviderRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.LookUpDto;
using Entities.Dtos.ProviderDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ProviderRepository
{
    public class ProviderRepository : EfEntityRepositoryBase<Provider, ProjectDbContext>, IProviderRepository
    {
        public ProviderRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProviderListDto>> GetProviderBySearchParams(string firstName, string type, string title, int? statusId, int? locationId)
        {
            List<GetAllProviderDto> query = new List<GetAllProviderDto>();
            List<ProviderListDto> providerList = new List<ProviderListDto>();

            var sql = $@"drop table if exists #temptable
                        select pr.[ProviderId]
                        ,pr.[Title]
                        ,(pr.[FirstName] +' '+pr.[LastName]) as FirstName
                        ,pr.[MI]
                        ,pr.[LastName]
                        ,pr.[Address]
                        ,pr.[City]
                        ,pr.[State]
                        ,pr.[ZIP]
                        ,pr.[ProviderEmail]
                        ,pr.[OfficeEmail]
                        ,pr.[PreviousName]
                        ,pr.[Degree]
                        ,pr.[NPINumber]
                        ,pr.[TaxonomyNo]
                        ,pr.[Specialty]
                        ,pr.[PLICarrierName]
                        ,pr.[PLINumber]
                        ,pr.[PLIExpiryDate]
                        ,pr.[CAQHID]
                        ,pr.[CAQHUsername]
                        ,pr.[CAQHPassword]
                        ,pr.[CreatedBy]
                        ,pr.[CreatedDate]
                        ,pr.[ModifiedBy]
                        ,pr.[ModifiedDate]
                        ,pr.[IsDeleted]
                        ,pr.[SSN]
                        ,pr.[DOB]
                        ,pr.[CellNumber]
                        ,pr.[UserId], psli.StateLicenseId, psli.StateLicenseNo, psli.StateLicense, psli.StateLicenseExpiryDate, 
                        psli.ProviderId as StateLicenseProviderId, pdea.DEAInfoId, pdea.DEAInfo, pdea.DEAExpiryDate, pdea.ProviderId as DEAProviderId,
                        l.LocationId, l.LocationName,pci.LocationId as AssignedLocationId
						into #temptable
                        from Provider pr
                        left outer join ProviderStateLicenseInfo psli on pr.ProviderId = psli.ProviderId
                        left outer join ProviderDEAInfo pdea on pr.ProviderId = pdea.ProviderId
                        left outer join ProviderClinicalInfo pci on pr.ProviderId = pci.ProviderId
                        left outer join Locations l on pci.LocationId = l.LocationId
                        where ISNULL(pr.IsDeleted,0)=0  

						select * from #temptable t";

            if (!string.IsNullOrEmpty(firstName))
            {
                sql += " where t.FirstName like '%" + firstName + "%'";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetAllProviderDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                var providerIdList = query.Select(x => x.ProviderId).Distinct();

                foreach (var providerId in providerIdList)
                {
                    ProviderListDto provider = new ProviderListDto();
                    var data = query.Where(x => x.ProviderId == providerId).ToList();
                    if (data.Count() > 0)
                    {
                        List<ProviderDEAInfoDto> providerDEAInfoList = data.ConvertAll(a =>
                        {
                            return new ProviderDEAInfoDto()
                            {
                                DEAInfoId = a.DEAInfoId,
                                DEAInfo = a.DEAInfo,
                                DEAExpiryDate = a.DEAExpiryDate,
                                DEAProviderId = a.DEAProviderId,
                            };
                        });

                        List<ProviderStateLicenseInfoDto> providerStateLicenseInfoList = data.ConvertAll(a =>
                        {
                            return new ProviderStateLicenseInfoDto()
                            {
                                StateLicenseId = a.StateLicenseId,
                                StateLicenseNo = a.StateLicenseNo,
                                StateLicense = a.StateLicense,
                                StateLicenseExpiryDate = a.StateLicenseExpiryDate,
                                StateLicenseProviderId = a.StateLicenseProviderId,
                            };
                        });

                        provider.ProviderId = data[0].ProviderId;
                        provider.Title = data[0].Title;
                        provider.FirstName = data[0].FirstName;
                        provider.LastName = data[0].LastName;
                        provider.SSN = data[0].SSN;
                        provider.DOB = data[0].DOB;
                        provider.ProviderEmail = data[0].ProviderEmail;
                        provider.OfficeEmail = data[0].OfficeEmail;
                        provider.NPINumber = data[0].NPINumber;
                        provider.CAQHID = data[0].CAQHID;
                        provider.LocationId = data[0].LocationId;
                        provider.LocationName = data[0].LocationName;
                        provider.Degree = data[0].Degree;
                        provider.AssignedLocationId = data[0].LocationId;
                        provider.providerDEAInfoList = providerDEAInfoList;
                        provider.providerStateLicenseInfoList = providerStateLicenseInfoList;

                        providerList.Add(provider);
                    }
                }

                if (providerList.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(title))
                    {
                        providerList = providerList.Where(s => s.Title == title).ToList();
                    }

                    if (!string.IsNullOrEmpty(type))
                    {
                        providerList = providerList.Where(s => s.Degree == type).ToList();
                    }

                    if (locationId > 0)
                    {
                        providerList = providerList.Where(s => s.AssignedLocationId == locationId).ToList();
                    }
                }
            }

            return providerList;
        }

        public async Task<GetProviderByIdDto> GetProviderById(int ProviderId)
        {
            GetProviderByIdDto provider = new GetProviderByIdDto();
            List<ProviderWorkConfigDto> ProviderWorkConfig = new List<ProviderWorkConfigDto>();
            List<ProviderBoardCertificationInfoDto> ProviderBoardCertificationInfo = new List<ProviderBoardCertificationInfoDto>();
            List<ProviderDEAInfoDto> ProviderDEAInfo = new List<ProviderDEAInfoDto>();
            List<ProviderSecurityCheckInfoDto> ProviderSecurityCheckInfo = new List<ProviderSecurityCheckInfoDto>();
            List<ProviderStateLicenseInfoDto> ProviderStateLicenseInfo = new List<ProviderStateLicenseInfoDto>();

            var sql = $@"select pr.*, pci.ProviderClinicalInfoId, pci.LocationId, pci.JoiningDate, pci.ReHiringDate, pci.TerminationDate,
                    pci.ProviderServiceId, pci.ChildAbuseCertificate, pci.ChildAbuseCertExpiryDate, pci.MandatedReporterCertificate, 
                    pci.MandatedReportExpiryDate, pci.RoomId, pci.CredentialedInsurances, pci.FlatRate, pci.ProcedureBasedRate,
                    pci.HourlyRate, pci.ContinuingEducation, pci.CompletedHours, pci.ProviderId as ClinicalInfoProviderId
                    from Provider pr
                    left outer join ProviderClinicalInfo pci on pr.ProviderId = pci.ProviderId
                    where ISNULL(pr.IsDeleted,0)=0 and pr.ProviderId =" + ProviderId;

            using (var connection = Context.Database.GetDbConnection())
            {
                provider = connection.Query<GetProviderByIdDto>(sql).FirstOrDefault();

                var sqlBoardCertificationInfo = $@"select * from ProviderBoardCertificationInfo where ProviderId =" + ProviderId;
                var sqlDEAInfo = $@"select DEAInfoId, DEAInfo, DEAExpiryDate, ProviderId as DEAProviderId from ProviderDEAInfo where ProviderId =" + ProviderId;
                var sqlStateLicenseInfo = $@"select StateLicenseId, StateLicenseNo, StateLicense, StateLicenseExpiryDate, ProviderId as StateLicenseProviderId 
                                            from ProviderStateLicenseInfo where ProviderId = " + ProviderId;

                provider.ProviderClinicalInfoId = provider.ProviderClinicalInfoId == null ? 0 : provider.ProviderClinicalInfoId;
                var sqlSecurityCheckInfo = $@"select * from ProviderSecurityCheckInfo where ProviderClinicalInfoId =" + provider.ProviderClinicalInfoId;
                var sqlProviderWorkConfig = $@"select * from ProviderWorkConfig where ProviderId =" + ProviderId;

                ProviderWorkConfig = connection.Query<ProviderWorkConfigDto>(sqlProviderWorkConfig).ToList();
                ProviderBoardCertificationInfo = connection.Query<ProviderBoardCertificationInfoDto>(sqlBoardCertificationInfo).ToList();
                ProviderDEAInfo = connection.Query<ProviderDEAInfoDto>(sqlDEAInfo).ToList();
                ProviderSecurityCheckInfo = connection.Query<ProviderSecurityCheckInfoDto>(sqlSecurityCheckInfo).ToList();
                ProviderStateLicenseInfo = connection.Query<ProviderStateLicenseInfoDto>(sqlStateLicenseInfo).ToList();
            }

            if (ProviderWorkConfig.Count() > 0)
            {
                List<GetProviderWorkConfigDto> providerWorkConfigList = ProviderWorkConfig.ConvertAll(a =>
                {
                    return new GetProviderWorkConfigDto()
                    {
                        Id = (int)a.Id,
                        ProviderId = a.ProviderId,
                        LocationId = a.LocationId,
                        Days = a.Days,
                        FirstShiftWorkFrom = a.FirstShiftWorkFrom,
                        FirstShiftWorkTo = a.FirstShiftWorkTo,
                        BreakShiftWorkFrom = a.BreakShiftWorkFrom,
                        BreakShiftWorkTo = a.BreakShiftWorkTo,
                        SlotSize = a.SlotSize.ToString(),
                        IsBreak = a.IsBreak,
                    };
                });
                provider.ProviderWorkConfig = providerWorkConfigList;
            }

            if (ProviderBoardCertificationInfo.Count() > 0)
            {
                provider.ProviderBoardCertificationInfo = ProviderBoardCertificationInfo;
            }

            if (ProviderDEAInfo.Count() > 0)
            {
                provider.ProviderDEAInfo = ProviderDEAInfo;
            }

            if (ProviderSecurityCheckInfo.Count() > 0)
            {
                provider.ProviderSecurityCheckInfo = ProviderSecurityCheckInfo;
            }

            if (ProviderStateLicenseInfo.Count() > 0)
            {
                provider.ProviderStateLicenseInfo = ProviderStateLicenseInfo;
            }

            return provider;
        }

        public async Task<List<LookupDto>> GetProviderLookupList()
        {
            List<LookupDto> query = new List<LookupDto>();
            var sql = $@"SELECT [ProviderId] as LookupId
                        ,(ISNULL(FirstName,'')+ ISNULL(' '+LastName,'')) as LookupName
                        FROM [dbo].[Provider] 
                        where ISNULL(IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<LookupDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.LookupId).ToList();
            }

            return query;
        }

        public async Task<List<string>> GetProviderOffDaysList(int ProviderId, int LocationId)
        {
            List<string> query = new List<string>();
            string[] daysListArray = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", };
            List<string> daysList = new List<string>();
            daysList = daysListArray.ToList();

            var sql = $@"select Days from ProviderWorkConfig where ProviderId = " + ProviderId + " and LocationId = " + LocationId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<string>(sql).ToList();
            }

            var days = daysList.Except(query).ToList();

            return days;
        }
    }
}
