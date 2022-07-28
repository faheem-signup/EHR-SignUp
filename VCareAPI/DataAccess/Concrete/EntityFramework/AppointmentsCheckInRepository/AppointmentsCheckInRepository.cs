using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AppointmentsCheckInEntity;
using Entities.Dtos.AppointmentDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.AppointmentsCheckInRepository
{
   public class AppointmentsCheckInRepository : EfEntityRepositoryBase<AppointmentsCheckIn, ProjectDbContext>, IAppointmentsCheckInRepository
    {
        public AppointmentsCheckInRepository(ProjectDbContext context) : base(context)
        {
           
        }
        public async Task<List<AppointmentsCheckInDto>> GetAppointmentsCheckInList()
        {
            List<AppointmentsCheckInDto> query = new List<AppointmentsCheckInDto>();
            var sql = $@"SELECT apci.[AppointmentsCheckInId]
                        ,apci.[AppointmentId]
                        ,apci.[AppointmentStatusId]
                        ,apci.[AppointmentCheckInDateTime]
                        ,apci.[AppointmentCheckOutDateTime]
	                    ,apt.[AppointmentId]
                        ,apt.[AllowGroupPatient]
                        ,apt.[AppointmentDate]
                        ,apt.[TimeFrom]
                        ,apt.[TimeTo]
                        ,apt.[ProviderId]
                        ,apt.[LocationId]
                        ,apt.[ServiceProfileId]
                        ,apt.[AppointmentStatus]
                        ,apt.[VisitType]
                        ,apt.[VisitReason]
                        ,apt.[RoomNo]
                        ,apt.[GroupAppointmentReason]
                        ,apt.[Notes]
                        ,apt.[PatientId]
                        ,apt.[IsRecurringAppointment]
                        ,apt.[IsFollowUpAppointment]
	                    ,rm.RoomName
                         FROM [dbo].[AppointmentsCheckIn] apci
                         INNER JOIN [dbo].[Appointment] apt
                         ON apt.AppointmentId = apci.AppointmentId
                         LEFT JOIN [dbo].[Rooms] rm
                         ON apt.RoomNo = rm.RoomId
                          where ISNULL(apt.IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AppointmentsCheckInDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.AppointmentDate).ToList();
            }

            return query;
        }
    }
}
