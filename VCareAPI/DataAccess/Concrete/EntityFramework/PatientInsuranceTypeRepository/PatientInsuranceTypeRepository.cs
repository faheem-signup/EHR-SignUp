using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IPatientInsuranceTypeRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientInsuranceTypeEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.PatientInsuranceTypeRepository
{
   public class PatientInsuranceTypeRepository : EfEntityRepositoryBase<PatientInsuranceType, ProjectDbContext>, IPatientInsuranceTypeRepository
    {
        public PatientInsuranceTypeRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
