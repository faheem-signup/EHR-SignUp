using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IClinicalTemplateDataRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ClinicalTemplateDataEntity;
using Entities.Dtos.FormTemplateDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ClinicalTemplateDataRepository
{
    public class ClinicalTemplateDataRepository : EfEntityRepositoryBase<ClinicalTemplateData, ProjectDbContext>, IClinicalTemplateDataRepository
    {
        public ClinicalTemplateDataRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<ClinicalTemplateDataDto> GetClinicalTemplateData(int TemplateId, int PatientId, int ProviderId, int? AppointmentId)
        {
            ClinicalTemplateDataDto query = new ClinicalTemplateDataDto();
            var sql = $@"select top 1 ClinicalTemplateId, TemplateId, PatientId, ProviderId, AppointmentId, FormData 
                from ClinicalTemplateData
                where TemplateId = " + TemplateId + " and PatientId = " + PatientId + " and ProviderId = " + ProviderId;

            if (AppointmentId != null && AppointmentId > 0)
            {
                sql = sql + " and AppointmentId = " + AppointmentId;
            }

            sql = sql + " order by ClinicalTemplateId desc";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ClinicalTemplateDataDto>(sql).FirstOrDefault();
            }

            return query;
        }
    }
}
