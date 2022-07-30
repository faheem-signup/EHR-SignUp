using Core.DataAccess;
using Entities.Concrete.PatientProviderEntity;
using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientProvideReferralRepository
{
    public interface IPatientProvideReferralRepository : IEntityRepository<PatientProvideReferring>
    {
        Task BulkInsert(IEnumerable<PatientProvideReferring> existingPatientProvideReferral, IEnumerable<PatientProvideReferring> newPatientProvideReferralList);
        Task<List<PatientReferralProviderDto>> GetPatientReferralProviderList(int PatientProviderId);
        Task<List<PatientProvideReferringDto>> GetPatientProviderReferringList(int PatientId);
    }
}
