using Core.DataAccess;
using Entities.Concrete.PatientNotesEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IPatientNotesRepository
{
    public interface IPatientNotesRepository : IEntityRepository<PatientNotes>
    {
    }
}
