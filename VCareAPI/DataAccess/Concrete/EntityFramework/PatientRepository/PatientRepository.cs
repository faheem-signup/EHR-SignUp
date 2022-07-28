using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientEntity;
using Entities.Dtos.LookUpDto;
using Entities.Dtos.PatientDiagnosisCodeDto;
using Entities.Dtos.PatientDocumentsDto;
using Entities.Dtos.PatientDto;
using Entities.Dtos.PatientInsurancesDto;
using Entities.Dtos.PatientVitalsDto;
using Entities.Dtos.PracticeDto;
using Entities.Dtos.ProviderDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientRepository
{
    public class PatientRepository : EfEntityRepositoryBase<Patient, ProjectDbContext>, IPatientRepository
    {
        public PatientRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<PatientDto> GetClinicalSummaryPatient(int PatientId)
        {
            PatientDto query = new PatientDto();
            var sql = $@"select p.PatientId, p.FirstName, p.MiddleName, p.LastName, p.DOB, p.Email, p.CellPhone, 
            p.AddressLine1, p.AddressLine2, p.CreatedDate, p.PatientImage, p.PatientImagePath, a.AppointmentDate
            from Patients p
            left outer join Appointment a on p.PatientId = a.PatientId
            where ISNULL(p.IsDeleted,0)=0 and p.PatientId = " + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDto>(sql).FirstOrDefault();
            }

            return query;
        }

        public async Task<PatientDto> GetPatientById(int PatientId)
        {
            PatientDto query = new PatientDto();
            List<PatientGuardianDto> guardianListquery = new List<PatientGuardianDto>();
            var sql = $@"select p.*, s.[Description] as MaritalStatusDescription,g.GenderName,c.CityName,c.County as CountyName,c.StateCode,c.ZipCode
                from Patients p
                left outer join MaritalStatusLookup s on p.StatusId = s.MaritalStatusLookupId
                left outer join Genders g on p.Gender = g.GenderId
                left outer join CityStateLookup c on p.City = c.CityId
                where ISNULL(IsDeleted,0)=0 and p.PatientId = " + PatientId;
            var guardianLisSql = $@"select * from PatientGuardians where PatientId = " + PatientId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDto>(sql).FirstOrDefault();
                guardianListquery = connection.Query<PatientGuardianDto>(guardianLisSql).ToList();
            }
            if (query != null)
            {
                query.PatientGuardianList = guardianListquery;
            }

            return query;
        }

        public async Task<List<PatientGuardianDto>> GetPatientGuardiansById(int PatientId)
        {
            List<PatientGuardianDto> query = new List<PatientGuardianDto>();
            var sql = $@"select * from PatientGuardians where PatientId = " + PatientId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientGuardianDto>(sql).ToList();
            }

            return query;
        }

        public async Task<List<PatientSearchParamDto>> GetPatientsSearchParams(string Search)
        {
            List<PatientSearchParamDto> query = new List<PatientSearchParamDto>();
            var sql = $@"select p.PatientId, p.FirstName, p.LastName, g.GenderName, p.HomePhone, p.CellPhone, p.Email,
                p.AddressLine1, s.StatusName
                from Patients p
                left outer join Statuses s on p.StatusId = s.StatusId
                left outer join Genders g on p.Gender = g.GenderId
                where ISNULL(IsDeleted,0)=0 ";
            using (var connection = Context.Database.GetDbConnection())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    sql = sql +
                        "and ((p.PatientId = 1)" +
                        "or (p.FirstName is not null and p.FirstName = '" + Search + "') " +
                        "or (p.LastName is not null and p.LastName = '" + Search + "')" +
                        "or (g.GenderName is not null and g.GenderName = '" + Search + "') " +
                        "or (p.HomePhone is not null and p.HomePhone = '" + Search + "')" +
                        "or (p.CellPhone is not null and p.CellPhone = '" + Search + "') " +
                        "or (p.Email is not null and p.Email = '" + Search + "')" +
                        "or (p.AddressLine1 is not null and p.AddressLine1 = '" + Search + "')" +
                        "or (s.StatusName is not null and s.StatusName = '" + Search + "') )";
                }

                query = connection.Query<PatientSearchParamDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.PatientId).ToList();
            }

            return query;
        }

        public async Task<List<LookupDto>> GetPatientLookupList()
        {
            List<LookupDto> query = new List<LookupDto>();
            var sql = $@"SELECT [PatientId] as LookupId
                    ,(ISNULL(FirstName,'')+ ISNULL(' '+MiddleName,'')+ ISNULL(' '+LastName,'')) as LookupName
                FROM [VCareEHRDB].[dbo].[Patients]
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

        public async Task<PatientInsuranceDataDto> GetPatientInsuranceDataById(int PatientId, int ProviderId, int? AppointmentId)
        {
            PatientInsuranceDataDto query = new PatientInsuranceDataDto();
            List<ProviderDEAInfoDto> ProviderDEAInfo = new List<ProviderDEAInfoDto>();
            List<ProviderStateLicenseInfoDto> ProviderStateLicenseInfo = new List<ProviderStateLicenseInfoDto>();
            PatientInsuranceNameDto PatientInsurancePrimary = new PatientInsuranceNameDto();
            PatientInsuranceNameDto PatientInsuranceSecondary = new PatientInsuranceNameDto();
            PracticeDataDto PracticeData = new PracticeDataDto();

            var sql = $@"select distinct a.AppointmentId, a.AppointmentDate, p.FirstName + ' ' + p.LastName as PatientName, p.DOB, p.AddressLine1,
                pr.FirstName + ' ' + pr.LastName ProviderName, pr.NPINumber
                from Patients p
                inner join Appointment a on p.PatientId = a.PatientId
			    inner join Provider pr on a.ProviderId = pr.ProviderId
                where ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 and ISNULL(pr.IsDeleted,0)=0 
                and p.PatientId = " + PatientId + " and pr.ProviderId = " + ProviderId;

            if (AppointmentId != null && AppointmentId > 0)
            {
                sql = sql + " and a.AppointmentId = " + AppointmentId;
            }

            var sqlPracticeData = $@"select p.PracticeId, p.LegalBusinessName,p.PhysicalAddress, p.PhoneNumber, p.FaxNumber 
                from Practices p
                inner join UserApp u on p.PracticeId = u.PracticeId
                inner join Provider pr on u.UserId = pr.UserId
                where pr.ProviderId = " + ProviderId;
            var sqlDEAInfo = $@"select DEAInfo from ProviderDEAInfo where ProviderId =" + ProviderId;
            var sqlStateLicenseInfo = $@"select StateLicenseNo from ProviderStateLicenseInfo where ProviderId = " + ProviderId;
            var sqlPatientInsurancePrimary = $@"select PatientInsuranceId,InsuranceName from PatientInsurance 
            where InsuranceType = (select PatientInsuranceTypeId from PatientInsuranceType where Description = 'Primary Insurance') and PatientId = " + PatientId;
            var sqlPatientInsuranceSecondary = $@"select PatientInsuranceId,InsuranceName from PatientInsurance 
            where InsuranceType = (select PatientInsuranceTypeId from PatientInsuranceType where Description = 'Secondary Insurance') and PatientId = " + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientInsuranceDataDto>(sql).FirstOrDefault();
                PracticeData = connection.Query<PracticeDataDto>(sqlPracticeData).FirstOrDefault();
                ProviderDEAInfo = connection.Query<ProviderDEAInfoDto>(sqlDEAInfo).ToList();
                ProviderStateLicenseInfo = connection.Query<ProviderStateLicenseInfoDto>(sqlStateLicenseInfo).ToList();
                PatientInsurancePrimary = connection.Query<PatientInsuranceNameDto>(sqlPatientInsurancePrimary).FirstOrDefault();
                PatientInsuranceSecondary = connection.Query<PatientInsuranceNameDto>(sqlPatientInsuranceSecondary).FirstOrDefault();

                if (query != null)
                {

                    if (PracticeData != null)
                    {
                        query.LegalBusinessName = PracticeData.LegalBusinessName;
                        query.PhysicalAddress = PracticeData.PhysicalAddress;
                        query.PhoneNumber = PracticeData.PhoneNumber;
                        query.FaxNumber = PracticeData.FaxNumber;
                    }

                    if (ProviderDEAInfo != null)
                    {
                        string dEAInfoName = string.Empty;
                        int listCount = 0;
                        foreach (var item in ProviderDEAInfo)
                        {
                            if (listCount > 0)
                            {
                                dEAInfoName = dEAInfoName + "," + item.DEAInfo;
                            }
                            else
                            {
                                dEAInfoName = item.DEAInfo;
                            }

                            listCount++;
                        }

                        query.DEAInfo = dEAInfoName;
                    }

                    if (ProviderStateLicenseInfo != null)
                    {
                        string stateLicenseNo = string.Empty;
                        int listCount = 0;
                        foreach (var item in ProviderStateLicenseInfo)
                        {
                            if (listCount > 0)
                            {
                                stateLicenseNo = stateLicenseNo + "," + item.StateLicenseNo;
                            }
                            else
                            {
                                stateLicenseNo = item.StateLicenseNo;
                            }

                            listCount++;
                        }

                        query.StateLicenseNo = stateLicenseNo;
                    }

                    if (PatientInsurancePrimary != null)
                    {
                        query.PrimaryPatientInsuranceId = PatientInsurancePrimary.PatientInsuranceId;
                        query.PrimaryInsuranceName = PatientInsurancePrimary.InsuranceName;
                    }

                    if (PatientInsuranceSecondary != null)
                    {
                        query.SecondaryPatientInsuranceId = PatientInsuranceSecondary.PatientInsuranceId;
                        query.SecondaryInsuranceName = PatientInsuranceSecondary.InsuranceName;
                    }

                }
            }

            return query;
        }

        public async Task<PatientDto> GetPatientDetailById(int PatientId)
        {
            PatientDto query = new PatientDto();
            List<PatientGuardianDto> guardianListquery = new List<PatientGuardianDto>();

            var sql = $@"SELECT pt.PatientId, pt.[FirstName]
                      ,pt.[MiddleName]
                      ,pt.[LastName]
                      ,pt.[Suffix]
                      ,pt.[DOB]
                      ,pt.[Gender]
                      ,pt.[SSN]
                      ,pt.[AddressLine1]
                      ,pt.[AddressLine2]
                      ,pt.[CellPhone]
                      ,pt.[HomePhone]
                      ,pt.[Email]
                	  ,(select CityName from dbo.CityStateLookup where CityId= pt.City) as CityName
                	  ,(select State from dbo.CityStateLookup where StateId= pt.State) as StateCode
                	  ,(select ZipCode from dbo.CityStateLookup where ZipId= pt.ZIP) as ZipCode
                	
                  FROM [dbo].[Patients] pt 
				where pt.PatientId = " + PatientId;

            var guardianLisSql = $@"select * from PatientGuardians where PatientId = " + PatientId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientDto>(sql).FirstOrDefault();
            guardianListquery = connection.Query<PatientGuardianDto>(guardianLisSql).ToList();

            if (query != null)
            {
                query.PatientGuardianList = guardianListquery;
            }

            return query;
        }

        public async Task<PatientAdditionalInfoDto> GetPatientInfoDetailById(int PatientId)
        {
            PatientAdditionalInfoDto query = new PatientAdditionalInfoDto();

            var sql = $@"SELECT isl.Description as IllicitSubstancesName
	                  ,sasl.Description as SubstanceAbuseStatusName
	                  ,al.AlcoholLookupId as AlcoholName
                     FROM [dbo].[PatientInfoDetails] pid
                     Left Join dbo.SubstanceAbuseStatusLookup sasl
                     ON sasl.SubstanceAbuseStatusLookupId = pid.SubstanceAbuseStatus
                     Left Join dbo.IllicitSubstancesLookup isl
                     ON isl.IllicitSubstancesLookupId = pid.IllicitSubstances
                     Left Join dbo.AlcoholLookup al
                     ON al.AlcoholLookupId= pid.Alcohol
                     where PatientId=" + PatientId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientAdditionalInfoDto>(sql).FirstOrDefault();

            return query;
        }

        public async Task<PatientAdditionalInfoDto> GetPatientProvidersDetailById(int PatientId)
        {
            PatientAdditionalInfoDto query = new PatientAdditionalInfoDto();
            List<PatientProvideReferringDto> PatientProvideReferringObj = new List<PatientProvideReferringDto>();
            var sql = $@"SELECT pp.[AttendingPhysician]
                         ,pp.[SupervisingProvider]
                         ,(select LocationName from dbo.Locations where LocationId=pp.[LocationId]) as LocationName
                     FROM [dbo].[PatientProviders] pp
                     where PatientId=" + PatientId;

            var patientProviderRefrSql = $@"select * from PatientGuardians where PatientId = " + PatientId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientAdditionalInfoDto>(sql).FirstOrDefault();

            PatientProvideReferringObj = connection.Query<PatientProvideReferringDto>(patientProviderRefrSql).ToList();

            if (query != null)
            {
                if (PatientProvideReferringObj != null)
                {
                    query._patientProvideReferring = PatientProvideReferringObj;
                }
            }

            return query;
        }

        public async Task<PatientVitalsDto> GetPatientVitalDetailById(int PatientId)
        {
            PatientVitalsDto query = new PatientVitalsDto();
            var sql = $@"SELECT [VisitDate],[Height],[Weight],[BMI],[Waist],[SystolicBP],[DiaSystolicBP],[HeartRate],[RespiratoryRate],[Temprature],[PainScale],[HeadCircumference],[Comment],[TemplateId]
                     FROM [dbo].[PatientVitals]
                     where PatientId  = " + PatientId + "order by VisitDate desc";

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientVitalsDto>(sql).FirstOrDefault();

            return query;
        }

        public async Task<PatientDiagnosisCodeDto> GetPatientDiagnosisCodeDetailById(int PatientId)
        {
            PatientDiagnosisCodeDto query = new PatientDiagnosisCodeDto();
            var sql = $@"SELECT [DiagnosisDate],[ResolvedDate],[DiagnoseCodeType],[SNOMEDCode],[SNOMEDCodeDesctiption],[Description],[Comments],
                    (select Code from dbo.DiagnosisCodes where DiagnosisId = pdc.DiagnosisId) as DiagnosisCode
                      FROM [PatientDiagnosisCode] pdc
                    
                      where PatientId  = " + PatientId + "order by [DiagnosisDate] desc";

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientDiagnosisCodeDto>(sql).FirstOrDefault();

            return query;
        }

        public async Task<List<PatientInsurancesDto>> GetPatientInsurancesDetailById(int PatientId)
        {
            List<PatientInsurancesDto> query = new List<PatientInsurancesDto>();
            var sql = $@"SELECT pti.[InsuredPay]
                      ,pti.[InsuranceName]
                      ,pti.[EligibilityDate]
                      ,pti.[InsuranceAddress]
                      ,pti.[City]
                      ,pti.[State]
                      ,pti.[Zip]
                      ,pti.[InsurancePhone]
                      ,pti.[Copay]
                      ,pti.[PolicyNumber]
                	  ,pit.Description InsuranceTypeName
					  ,(select CityName from dbo.CityStateLookup where CityId= pti.City) as CityName
                	  ,(select State from dbo.CityStateLookup where StateId= pti.State) as StateName
                	  ,(select ZipCode from dbo.CityStateLookup where ZipId= pti.ZIP) as ZipCode
                  FROM [dbo].[PatientInsurance] pti
                    left outer join dbo.PatientInsuranceType pit on pit.PatientInsuranceTypeId= pti.InsuranceType
                    
                     where ISNULL(pti.IsDeleted,0)=0 and pti.PatientId = " + PatientId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientInsurancesDto>(sql).ToList();

            return query;
        }

        public async Task<IEnumerable<PatientDocumentDto>> GetPatientDocumentById(int PatientID)
        {
            List<PatientDocumentDto> query = new List<PatientDocumentDto>();

            var sql = $@"select pd.[PatientDocumentId]
                        ,pd.[PatientDocCateogryId]
                        ,pd.[UploadDocumentId]
                        ,pd.[DateOfVisit]
                        ,pd.[PatientId] 
                        ,pd.CreatedDate
	                    ,pdf.[DocumentPath]
	                    ,pdc.[CategoryName]
                        ,pdf.[DocumentName]
	                    from dbo.PatientDocument pd
                        LEFT JOIN dbo.PatientDocumentCategory pdc
                        ON pdc.PatientDocCateogryId = pd.PatientDocCateogryId
                        LEFT JOIN dbo.PatientDocsFileUpload pdf
                        ON pdf.UploadDocumentId = pd.UploadDocumentId
                        where pd.PatientId =" + PatientID;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientDocumentDto>(sql).ToList();

            return query;
        }

        public async Task<PatientDespensingDataDto> GetPatientDispensingDataById(int PatientId)
        {
            PatientDespensingDataDto query = new PatientDespensingDataDto();

            var sql = $@"select distinct a.AppointmentId, a.AppointmentDate, p.FirstName + ' ' + p.LastName as PatientName, p.DOB, p.SSN, g.GenderName,
                pr.FirstName + ' ' + pr.LastName ProviderName, pr.NPINumber
                from Patients p
                inner join Appointment a on p.PatientId = a.PatientId
			    inner join Provider pr on a.ProviderId = pr.ProviderId
				left outer join Genders g on p.Gender = g.GenderId
                where ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 and ISNULL(pr.IsDeleted,0)=0 
                and p.PatientId = " + PatientId;

            var referringProviderSql = $@"select Name from PatientProvideReferring where ReferringProvider = 1 and PatientId =" + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDespensingDataDto>(sql).FirstOrDefault();
                var referringProviderData = connection.Query<string>(referringProviderSql).FirstOrDefault();

                if (query != null)
                {
                    query.ReferringProviderName = referringProviderData;
                }
            }

            return query;
        }
    }
}
