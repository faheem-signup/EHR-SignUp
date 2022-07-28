using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientDiagnosisCodeRepository;
using DataAccess.Abstract.IPatientVitalsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDiagnosisCodeEntity;
using Entities.Concrete.PatientVitalsEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientDiagnosisCodeRepository
{
    public class PatientDiagnosisCodeRepository : EfEntityRepositoryBase<PatientDiagnosisCode, ProjectDbContext>, IPatientDiagnosisCodeRepository
    {
        public PatientDiagnosisCodeRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PatientDiagnosisCode>> GetPatientDiagnosisCodeSearchParams(int? patientId, int? providerId, int? diagnosisId)
        {
            var patientVitalsList = Context.PatientDiagnosisCode.Include(x => x.Provider).ToList();

            if (patientId != 0 && patientId != null)
            {
                patientVitalsList = patientVitalsList.Where(x => x.PatientId == patientId).ToList();
            }

            if (providerId != 0 && providerId != null)
            {
                patientVitalsList = patientVitalsList.Where(x => x.ProviderId == providerId).ToList();
            }

            if (diagnosisId != 0 && diagnosisId != null)
            {
                patientVitalsList = patientVitalsList.Where(x => x.DiagnosisId == diagnosisId).ToList();
            }

            if (patientVitalsList.Count() > 0)
            {
                patientVitalsList = patientVitalsList.OrderByDescending(x => x.PatientDiagnosisCodeId).ToList();
            }

            return patientVitalsList;
        }
    }
}
