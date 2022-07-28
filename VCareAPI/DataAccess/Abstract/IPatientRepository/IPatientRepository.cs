using Core.DataAccess;
using Entities.Concrete.PatientEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientRepository
{
    public interface IPatientRepository : IEntityRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetPatientsSearchParams(string Search, string Type);
        Task<Patient> GetPatientById(int PatientId);
    }
}
