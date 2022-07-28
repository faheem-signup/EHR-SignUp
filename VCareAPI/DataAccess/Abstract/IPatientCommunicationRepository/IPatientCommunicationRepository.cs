using Core.DataAccess;
using Entities.Concrete.PatientCommunicationEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientCommunicationRepository
{
   public interface IPatientCommunicationRepository : IEntityRepository<PatientCommunication>
    {
        Task<IEnumerable<PatientCommunication>> GetPatientCommunication();
    }
}
