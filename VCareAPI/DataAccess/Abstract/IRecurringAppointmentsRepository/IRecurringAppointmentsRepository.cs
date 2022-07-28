using Core.DataAccess;
using Entities.Concrete.RecurringAppointmentsEntity;
using Entities.Dtos.RecurringAppointmentsDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IRecurringAppointmentsRepository
{
   public interface IRecurringAppointmentsRepository : IEntityRepository<RecurringAppointments>
    {
        Task<List<RecurringAppointmentsDto>> GetRecurringAppointments();
    }
}
