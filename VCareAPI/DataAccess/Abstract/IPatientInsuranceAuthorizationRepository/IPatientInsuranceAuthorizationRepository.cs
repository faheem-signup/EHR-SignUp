using Core.DataAccess;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientInsuranceAuthorizationRepository
{
   public interface IPatientInsuranceAuthorizationRepository : IEntityRepository<PatientInsuranceAuthorization>
    {
    }
}
