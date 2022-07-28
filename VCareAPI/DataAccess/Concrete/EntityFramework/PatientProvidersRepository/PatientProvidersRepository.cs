using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientProvidersRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientProviderEntity;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientProvidersRepository
{
    public class PatientProvidersRepository : EfEntityRepositoryBase<PatientProvider, ProjectDbContext>, IPatientProvidersRepository
    {
        public PatientProvidersRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<PatientProviderDropdownDto> GetPatientProviderDropdownName(int? LocationId)
        {
            PatientProviderDropdownDto query = new PatientProviderDropdownDto();
            LocationId = LocationId == null ? 0 : LocationId;
            var sql = $@"select LocationName from Locations where LocationId = " + LocationId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientProviderDropdownDto>(sql).FirstOrDefault();

            return query;
        }
    }
}
