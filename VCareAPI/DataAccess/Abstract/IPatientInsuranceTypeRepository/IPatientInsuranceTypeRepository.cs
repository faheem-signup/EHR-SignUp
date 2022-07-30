using Core.DataAccess;
using Entities.Concrete.PatientInsuranceTypeEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientInsuranceTypeRepository
{
   public interface IPatientInsuranceTypeRepository : IEntityRepository<PatientInsuranceType>
    {
    }
}
