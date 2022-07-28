using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.GroupPatientAppointmentRepository
{
    public class GroupPatientAppointmentRepository : EfEntityRepositoryBase<GroupPatientAppointment, ProjectDbContext>, IGroupPatientAppointmentRepository
    {
        public GroupPatientAppointmentRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task RemoveGroupPatientAppointments(IEnumerable<GroupPatientAppointment> existingGroupPatientAppointment)
        {
            Context.GroupPatientAppointment.RemoveRange(existingGroupPatientAppointment);
        }

    }
}
