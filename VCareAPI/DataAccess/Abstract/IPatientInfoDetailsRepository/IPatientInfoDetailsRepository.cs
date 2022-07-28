using Core.DataAccess;
using Entities.Concrete.PatientInfoDetailsEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientInfoDetailsRepository
{
    public interface IPatientInfoDetailsRepository : IEntityRepository<PatientInfoDetail>
    {
    }
}
