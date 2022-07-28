using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IAppointmentReasonsRepository;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AppointmentReasonsEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.AppointmentReasonsRepository
{
   public class AppointmentReasonsRepository : EfEntityRepositoryBase<AppointmentReasons, ProjectDbContext>, IAppointmentReasonsRepository
    {
        public AppointmentReasonsRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
