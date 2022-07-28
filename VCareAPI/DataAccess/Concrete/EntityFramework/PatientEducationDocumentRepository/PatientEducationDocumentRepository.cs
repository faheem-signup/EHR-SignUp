using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientEducationDocumentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientEducationEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PatientEducationDocumentRepository
{
    public class PatientEducationDocumentRepository : EfEntityRepositoryBase<PatientEducationDocument, ProjectDbContext>, IPatientEducationDocumentRepository
    {
        public PatientEducationDocumentRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
