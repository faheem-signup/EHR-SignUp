using Core.DataAccess;
using Entities.Concrete.PatientEmploymentEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientEmploymentsRepository
{
    public interface IPatientEmploymentsRepository : IEntityRepository<PatientEmployment>
    {
    }
}
