using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientDocsFileUploadRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDocsFileUploadEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PatientDocsFileUploadRepository
{
    public class PatientDocsFileUploadRepository : EfEntityRepositoryBase<PatientDocsFileUpload, ProjectDbContext>, IPatientDocsFileUploadRepository
    {
        public PatientDocsFileUploadRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
