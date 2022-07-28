using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.FollowUpAppointmentEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.FollowUpAppointmentRepository
{
   public class FollowUpAppointmentRepository : EfEntityRepositoryBase<FollowUpAppointment, ProjectDbContext>, IFollowUpAppointmentRepository
    {
        public FollowUpAppointmentRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
