using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientNotesRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientNotesEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PatientNotesRepository
{
   public class PatientNotesRepository : EfEntityRepositoryBase<PatientNotes, ProjectDbContext>, IPatientNotesRepository
    {
        public PatientNotesRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
