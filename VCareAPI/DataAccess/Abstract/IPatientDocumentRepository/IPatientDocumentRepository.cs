using Core.DataAccess;
using Entities.Concrete.PatientDocumentEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientDocumentRepository
{
   public interface IPatientDocumentRepository :IEntityRepository<PatientDocument>
    {
    }
}
