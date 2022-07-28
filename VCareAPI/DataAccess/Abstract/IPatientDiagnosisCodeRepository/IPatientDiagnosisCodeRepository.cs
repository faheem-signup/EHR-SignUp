using Core.DataAccess;
using Entities.Concrete.PatientDiagnosisCodeEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientDiagnosisCodeRepository
{
  public  interface IPatientDiagnosisCodeRepository : IEntityRepository<PatientDiagnosisCode>
    {
        Task<IEnumerable<PatientDiagnosisCode>> GetPatientDiagnosisCodeSearchParams(int? patientId, int? providerId, int? diagnosisId);
    }
}
