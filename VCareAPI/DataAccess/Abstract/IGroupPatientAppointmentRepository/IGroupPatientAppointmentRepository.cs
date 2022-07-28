using Core.DataAccess;
using Entities.Concrete.GroupPatientAppointmentEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IGroupPatientAppointmentRepository
{
   public interface IGroupPatientAppointmentRepository : IEntityRepository<GroupPatientAppointment>
    {
        Task RemoveGroupPatientAppointments(IEnumerable<GroupPatientAppointment> existingGroupPatientAppointment);
    }
}