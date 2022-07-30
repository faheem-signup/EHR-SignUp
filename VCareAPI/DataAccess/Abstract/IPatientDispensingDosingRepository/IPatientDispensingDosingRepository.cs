using Core.DataAccess;
using Entities.Concrete.PatientDispensingDosingEntity;
using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IPatientDispensingDosingRepository
{
    public interface IPatientDispensingDosingRepository : IEntityRepository<PatientDispensingDosing>
    {
        Task<List<PatientDispensingDosingDto>> GetPatientDispencingDosingList(int PatientId);
    }
}
