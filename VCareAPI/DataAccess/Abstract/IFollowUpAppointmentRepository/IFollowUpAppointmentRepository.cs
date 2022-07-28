using Core.DataAccess;
using Entities.Concrete.FollowUpAppointmentEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IFollowUpAppointmentRepository
{
    public interface IFollowUpAppointmentRepository : IEntityRepository<FollowUpAppointment>
    {
    }
}
