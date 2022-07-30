using Core.DataAccess;
using Entities.Concrete.PatientEducationEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientEducationDocumentRepository
{
    public interface IPatientEducationDocumentRepository : IEntityRepository<PatientEducationDocument>
    {
    }
}
