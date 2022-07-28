using Core.DataAccess;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.AppointmentDto;
using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IAppointmentRepository
{
  public interface IAppointmentRepository : IEntityRepository<Appointment>
    {
        Task<AppointmentDTO> GetAppointmentDetailById(int appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int patientId);
    }
}
