using Core.DataAccess;
using Entities.Concrete.PatientVitalsEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientVitalsRepository
{
  public  interface IPatientVitalsRepository : IEntityRepository<PatientVitals>
    {
        Task<IEnumerable<PatientVitals>> GetPatientVitalsSearchParams(int? patientId, int? providerId);
    }
}
