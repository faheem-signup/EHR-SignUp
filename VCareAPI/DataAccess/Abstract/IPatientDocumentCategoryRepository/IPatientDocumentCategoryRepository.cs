using Core.DataAccess;
using Entities.Concrete.PatientDocumentCategoryEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientDocumentCategoryRepository
{
   public interface IPatientDocumentCategoryRepository : IEntityRepository<PatientDocumentCategory>
    {
    }
}
