using Core.DataAccess;
using Entities.Concrete.AppointmentsCheckInEntity;
using Entities.Dtos.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IAppointmentsCheckInRepository
{
    public interface IAppointmentsCheckInRepository : IEntityRepository<AppointmentsCheckIn>
    {
        Task<List<AppointmentsCheckInDto>> GetAppointmentsCheckInList();
    }
}
