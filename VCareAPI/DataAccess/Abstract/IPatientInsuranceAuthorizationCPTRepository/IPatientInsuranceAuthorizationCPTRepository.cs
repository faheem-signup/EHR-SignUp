using Core.DataAccess;
using Core.Entities;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository
{
   public interface IPatientInsuranceAuthorizationCPTRepository : IEntityRepository<PatientInsuranceAuthorizationCPT>
    {
        Task BulkInsert(int patientInsuranceAuthorizationId, IEnumerable<PatientInsuranceAuthorizationCPT> patientInsuranceAuthorizationCPT);
    }
}
