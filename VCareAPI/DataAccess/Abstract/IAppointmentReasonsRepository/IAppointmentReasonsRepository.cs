using Core.DataAccess;
using Entities.Concrete.AppointmentReasonsEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IAppointmentReasonsRepository
{
    public interface IAppointmentReasonsRepository : IEntityRepository<AppointmentReasons>
    {
    }
}
