using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientDispensingRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDispensingEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientDispensingRepository
{
    public class PatientDispensingRepository : EfEntityRepositoryBase<PatientDispensing, ProjectDbContext>, IPatientDispensingRepository
    {
        public PatientDispensingRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<List<PatientDispensingDto>> GetPatientDispencingList(int PatientId)
        {
            List<PatientDispensingDto> query = new List<PatientDispensingDto>();
            var sql = $@"SELECT pd.[DispensingId]
                        ,pd.[PatientID]
                        ,pd.[ProgramId]
                        ,pd.[DrugUsage]
                        ,pd.[StartDate]
                        ,pd.[EndDate]
                        ,pd.[MainBottle]
                        ,pd.[TotalDispensed]
                        ,pd.[TotalDispensedUnitId]
                        ,pd.[Remaining]
                        ,pd.[RemainingUnitId]
                        ,pd.[TotalQuantity]
                        ,pd.[TotalQuantityUnitId]
                        ,(Select Description from dbo.ProgramLookup where ProgramLookupId= pd.ProgramId) as ProgramName
                    FROM [dbo].[PatientDispensing] pd
                    INNER JOIN dbo.Patients pt
                    ON pd.PatientID = pt.PatientId
                    Where pd.PatientID =" + PatientId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDispensingDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.DispensingId).ToList();
            }

            return query;
        }
    }
}
