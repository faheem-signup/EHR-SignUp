using Core.DataAccess;
using Entities.Concrete.PatientGuardiansEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientGuardiansRepository
{
    public interface IPatientGuardiansRepository : IEntityRepository<PatientGuardian>
    {
        Task<IEnumerable<PatientGuardian>> GetPatientGuardianByPatientId(int patientId);
        Task BulkInsert(int patientId, IEnumerable<PatientGuardian> patientGuardians);
    }
}
