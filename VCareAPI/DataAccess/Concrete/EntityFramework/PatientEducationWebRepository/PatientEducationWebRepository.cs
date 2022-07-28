using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientEducationWebRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientEducationEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PatientEducationWebRepository
{
    public class PatientEducationWebRepository : EfEntityRepositoryBase<PatientEducationWeb, ProjectDbContext>, IPatientEducationWebRepository
    {
        public PatientEducationWebRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
