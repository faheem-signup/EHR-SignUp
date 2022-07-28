using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientGuardiansRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientGuardiansEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientGuardiansRepository
{
    public class PatientGuardiansRepository : EfEntityRepositoryBase<PatientGuardian, ProjectDbContext>, IPatientGuardiansRepository
    {
        public PatientGuardiansRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task BulkInsert(IEnumerable<PatientGuardian> existingList, IEnumerable<PatientGuardian> patientGuardians)
        {

            try
            {
                if (existingList.Count() > 0)
                {
                    Context.PatientGuardians.RemoveRange(existingList);
                }

                await Context.PatientGuardians.AddRangeAsync(patientGuardians);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PatientGuardian>> GetPatientGuardianByPatientId(int patientId)
        {
            var _list = await Context.PatientGuardians.Where(x => x.PatientId == patientId).ToListAsync();
            return _list;
        }
    }
}
