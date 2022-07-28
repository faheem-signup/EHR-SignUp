using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientInfoDetailsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientInfoDetailsEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientInfoDetailsRepository
{
    public class PatientInfoDetailsRepository : EfEntityRepositoryBase<PatientInfoDetail, ProjectDbContext>, IPatientInfoDetailsRepository
    {
        public PatientInfoDetailsRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<PatientInfoDetailDropdownDto> GetPatientInfoDetailDropdownName(int? SmokingStatusId, int? PacksId, int? HospitalizationStatusId, int? PatientDisabilityStatusId, int? SubstanceAbuseStatusId, int? AlcoholId, int? IllicitSubstancesId)
        {
            PatientInfoDetailDropdownDto query = new PatientInfoDetailDropdownDto();

            var sql1 = $@"select Description as SmokingStatusDescription from SmokingStatusLookup where SmokingStatusLookupId = " + SmokingStatusId;
            var sql2 = $@"select Description as PacksDescription from PacksLookup where PacksLookupId = " + PacksId;
            var sql3 = $@"select Description as HospitalizationStatusDescription from HospitalizationStatusLookup where HospitalizationStatusLookupId = " + HospitalizationStatusId;
            var sql4 = $@"select Description as PatientDisabilityStatusDescription from PatientDisabilityStatusLookup where PatientDisabilityStatusLookupId = " + PatientDisabilityStatusId;
            var sql5 = $@"select Description as SubstanceAbuseStatusDescription from SubstanceAbuseStatusLookup where SubstanceAbuseStatusLookupId = " + SubstanceAbuseStatusId;
            var sql6 = $@"select Description as AlcoholDescription from AlcoholLookup where AlcoholLookupId = " + AlcoholId;
            var sql7 = $@"select Description as IllicitSubstancesDescription from IllicitSubstancesLookup where IllicitSubstancesLookupId = " + IllicitSubstancesId;

            var connection = Context.Database.GetDbConnection();

            if (SmokingStatusId != null && SmokingStatusId > 0)
            {
                var smokingStatusDescription = connection.Query<string>(sql1).FirstOrDefault();
                query.SmokingStatusDescription = smokingStatusDescription;
            }

            if (PacksId != null && PacksId > 0)
            {
                var packsDescription = connection.Query<string>(sql2).FirstOrDefault();
                query.PacksDescription = packsDescription;
            }

            if (HospitalizationStatusId != null && HospitalizationStatusId > 0)
            {
                var hospitalizationStatusDescription = connection.Query<string>(sql3).FirstOrDefault();
                query.HospitalizationStatusDescription = hospitalizationStatusDescription;
            }

            if (PatientDisabilityStatusId != null && PatientDisabilityStatusId > 0)
            {
                var patientDisabilityStatusDescription = connection.Query<string>(sql4).FirstOrDefault();
                query.PatientDisabilityStatusDescription = patientDisabilityStatusDescription;
            }

            if (SubstanceAbuseStatusId != null && SubstanceAbuseStatusId > 0)
            {
                var substanceAbuseStatusDescription = connection.Query<string>(sql5).FirstOrDefault();
                query.SubstanceAbuseStatusDescription = substanceAbuseStatusDescription;
            }

            if (AlcoholId != null && AlcoholId > 0)
            {
                var alcoholDescription = connection.Query<string>(sql6).FirstOrDefault();
                query.AlcoholDescription = alcoholDescription;
            }

            if (IllicitSubstancesId != null && IllicitSubstancesId > 0)
            {
                var illicitSubstancesDescription = connection.Query<string>(sql7).FirstOrDefault();
                query.IllicitSubstancesDescription = illicitSubstancesDescription;
            }

            return query;
        }
    }
}
