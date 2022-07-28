using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientEmploymentsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientEmploymentEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientEmploymentsRepository
{
    public class PatientEmploymentsRepository : EfEntityRepositoryBase<PatientEmployment, ProjectDbContext>, IPatientEmploymentsRepository
    {
        public PatientEmploymentsRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<PatientEmploymentDropdownDto> GetPatientEmploymentDropdownName(int? EmploymentStatusId, int? WorkStatusId, int? AccidentTypeId)
        {
            PatientEmploymentDropdownDto query = new PatientEmploymentDropdownDto();

            var sql = $@"select Description as EmploymentStatusDescription from EmploymentStatusLookup where EmploymentStatusLookupId = " + EmploymentStatusId;
            var sql2 = $@"select Description as WorkStatusDescription from WorkStatusLookup where WorkStatusLookupId = " + WorkStatusId;
            var sql3 = $@"select Description as AccidentTypeDescription from AccidentTypeLookup where AccidentTypeLookupId = " + AccidentTypeId;

            var connection = Context.Database.GetDbConnection();

            if (EmploymentStatusId != null && EmploymentStatusId > 0)
            {
                var employmentStatusDescription = connection.Query<string>(sql).FirstOrDefault();
                query.EmploymentStatusDescription = employmentStatusDescription;
            }

            if (WorkStatusId != null && WorkStatusId > 0)
            {
                var workStatusDescription = connection.Query<string>(sql2).FirstOrDefault();
                query.WorkStatusDescription = workStatusDescription;
            }

            if (AccidentTypeId != null && AccidentTypeId > 0)
            {
                var accidentTypeDescription = connection.Query<string>(sql3).FirstOrDefault();
                query.AccidentTypeDescription = accidentTypeDescription;
            }

            return query;
        }
    }
}
