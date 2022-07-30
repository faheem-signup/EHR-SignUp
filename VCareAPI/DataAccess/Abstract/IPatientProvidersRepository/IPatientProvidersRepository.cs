using Core.DataAccess;
using Entities.Concrete.PatientProviderEntity;
using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientProvidersRepository
{
    public interface IPatientProvidersRepository : IEntityRepository<PatientProvider>
    {
        Task<PatientProviderDropdownDto> GetPatientProviderDropdownName(int? LocationId);
    }
}
