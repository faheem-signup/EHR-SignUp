using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientDispensingDosingRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDispensingDosingEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientDispensingDosingRepository
{
   public class PatientDispensingDosingRepository : EfEntityRepositoryBase<PatientDispensingDosing, ProjectDbContext>, IPatientDispensingDosingRepository
    {
        public PatientDispensingDosingRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<List<PatientDispensingDosingDto>> GetPatientDispencingDosingList(int PatientId)
        {
            List<PatientDispensingDosingDto> query = new List<PatientDispensingDosingDto>();
            var sql = $@"SELECT [DispensingDosingId]
                      ,pdd.[PatientID]
                      ,pdd.[ProgramId]
                      ,pdd.[StartDate]
                      ,pdd.[EndDate]
                      ,pdd.[TherapistId]
                      ,pdd.[LastMidVisit]
                      ,pdd.[LastDose]
                      ,pdd.[TakeHome]
                      ,pdd.[MedicatedThru]
                      ,pdd.[MedThruDose]
                      ,pdd.[LevelOfCare]
                      ,pdd.[LastUAResult]
                      ,pdd.[Status]
                      ,pdd.[MedicationId]
                      ,pdd.[ScheduleId]
                    FROM [dbo].[PatientDispensingDosing] pdd
                	   INNER JOIN dbo.Patients pt
                	   ON pdd.PatientID = pt.PatientId
                	   Where pdd.PatientID =" + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDispensingDosingDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.DispensingDosingId).ToList();
            }

            return query;
        }
    }
}
