using Core.DataAccess;
using Entities.Concrete.PatientProviderEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientProvidersRepository
{
    public interface IPatientProvidersRepository : IEntityRepository<PatientProvider>
    {
    }
}
