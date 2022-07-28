using Core.DataAccess;
using Entities.Concrete.PatientInsuranceEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientInsuranceRepository
{
   public interface IPatientInsuranceRepository : IEntityRepository<PatientInsurance>
    {
    }
}
