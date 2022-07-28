using Core.DataAccess;
using Entities.Concrete.PatientDocsFileUploadEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientDocsFileUploadRepository
{
   public interface IPatientDocsFileUploadRepository : IEntityRepository<PatientDocsFileUpload>
    {
    }
}
