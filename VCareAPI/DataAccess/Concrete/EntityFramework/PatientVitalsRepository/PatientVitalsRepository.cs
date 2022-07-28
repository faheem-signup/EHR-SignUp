using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientVitalsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientVitalsEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientVitalsRepository
{
    public class PatientVitalsRepository : EfEntityRepositoryBase<PatientVitals, ProjectDbContext>, IPatientVitalsRepository
    {
        public PatientVitalsRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<PatientVitals> GetPatientVitalsById(int PatientVitalsId)
        {
            var patientVitalsList = await Context.PatientVitals
                .Where(x => x.PatientVitalsId == PatientVitalsId)
                .FirstOrDefaultAsync();
            return patientVitalsList;
        }

        public async Task<IEnumerable<PatientVitals>> GetPatientVitalsSearchParams(int? patientId, int? providerId)
        {
            var patientVitalsList = await Context.PatientVitals
                .Include(x => x._provider)
                .Where(x => x.PatientId == patientId)
                .ToListAsync();

            if (patientVitalsList.Count() > 0)
            {
                patientVitalsList = patientVitalsList.OrderByDescending(x => x.PatientVitalsId).ToList();
                if (patientId != 0 && patientId != null)
                {
                    patientVitalsList = patientVitalsList.Where(x => x.PatientId == patientId).ToList();
                }

                if (providerId != 0 && providerId != null)
                {
                    patientVitalsList = patientVitalsList.Where(x => x.ProviderId == providerId).ToList();
                }
            }

            return patientVitalsList;
        }
    }
}
