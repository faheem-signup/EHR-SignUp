using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IServiceProfileRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Dtos.ServiceProfileDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ServiceProfileRepository
{
    public class ServiceProfileRepository : EfEntityRepositoryBase<ServiceProfile, ProjectDbContext>, IServiceProfileRepository
    {
        public ServiceProfileRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task BulkInsert(List<ServiceProfile> serviceProfileList)
        {
            try
            {
                await Context.ServiceProfile.AddRangeAsync(serviceProfileList);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ServiceProfile>> GetByRowId(string Row_Id)
        {
            return await Context.ServiceProfile.Include(a => a.DiagnosisCode).Include(b => b.Procedure).Include(c => c.FormTemplate).Where(x => x.Row_Id == Guid.Parse(Row_Id)).ToListAsync();
        }

        public async Task<IEnumerable<ServiceProfilesDto>> GetAllServiceProfile()
        {
            string sql = $@"SELECT sp.ServiceProfileId, sp.Row_Id,sp.[ServiceProfileName],dc.Code as DiagnosisCode,
                    pc.Code as ProcedureCode,ft.TemplateName as Template
                    FROM [dbo].[ServiceProfile] sp
                    LEFT JOIN [dbo].DiagnosisCodes dc ON sp.ICDId = dc.DiagnosisId
                    LEFT JOIN [dbo].[Procedures] pc ON sp.ProcedureId = pc.ProcedureId
                    LEFT JOIN [dbo].FormTemplates ft ON sp.TemplateId = ft.TemplateId";
            var connection = Context.Database.GetDbConnection();

            try
            {
                var data = await connection.QueryAsync<ServiceProfilesDto>(sql, new[]
                {
                    typeof(ServiceProfilesDto)
                },
                objects =>
                {
                    var sp = objects[0] as ServiceProfilesDto;
                    return sp;
                }
                );

                if (data.Count() > 0)
                {
                    data = data.OrderByDescending(x => x.ServiceProfileId).ToList();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveServiceProfile(List<ServiceProfile> serviceProfileList)
        {
            Context.ServiceProfile.RemoveRange(serviceProfileList);
        }
    }
}
