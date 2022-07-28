using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.RecurringAppointmentsEntity;
using Entities.Dtos.RecurringAppointmentsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.RecurringAppointmentsRepository
{
   public class RecurringAppointmentsRepository : EfEntityRepositoryBase<RecurringAppointments, ProjectDbContext>, IRecurringAppointmentsRepository
    {
        public RecurringAppointmentsRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<RecurringAppointmentsDto>> GetRecurringAppointments(string PatientName)
        {
            List<RecurringAppointmentListDto> query = new List<RecurringAppointmentListDto>();
            List<RecurringAppointmentsDto> query2 = new List<RecurringAppointmentsDto>();
            var sql = $@"select ra.RecurringAppointmentId, p.PatientId, p.FirstName +' '+p.LastName as PatientName, FORMAT(a.AppointmentDate, 'MMM dd, yyyy hh:mm:ss tt') as OriginalScheduledTime, 
                'Repeated ' + wt.WeekTypeName + ' On '+ ra.Weekdays as RecurringDescription, r.RoomName, ra.FirstAppointDate, ra.LastAppointDate,
				'ID: ' + CONVERT(varchar, a.CreatedBy) + ' ' + CHAR(13) + ' Created: ' + FORMAT(a.CreatedDate, 'MMM dd, yyyy hh:mm:ss tt') as Detail 
                from RecurringAppointments ra
                inner join Appointment a on ra.AppointmentId = a.AppointmentId and ISNULL(a.IsDeleted,0)=0
                left outer join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0
                left outer join Rooms r on a.RoomNo = r.RoomId and ISNULL(r.IsDeleted,0)=0
				left outer join WeekTypeLookup wt on ra.WeekType = wt.WeekTypeId";

            if (!string.IsNullOrEmpty(PatientName))
            {
                sql += " where (p.FirstName like '%" + PatientName + "%' or p.LastName like '%" + PatientName + "%')";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<RecurringAppointmentListDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    var dates = new List<DateTime>();

                    for (var dt = item.FirstAppointDate; dt <= item.LastAppointDate; dt = dt.AddDays(1))
                    {
                        dates.Add(dt);
                    }

                    RecurringAppointmentsDto recurringAppointmentsObj = new RecurringAppointmentsDto();
                    recurringAppointmentsObj.RecurringAppointmentId = item.RecurringAppointmentId;
                    recurringAppointmentsObj.PatientName = item.PatientName;
                    recurringAppointmentsObj.PatientId = item.PatientId;
                    recurringAppointmentsObj.OriginalScheduledTime = item.OriginalScheduledTime;
                    recurringAppointmentsObj.RecurringDescription = item.RecurringDescription;
                    recurringAppointmentsObj.RoomName = item.RoomName;
                    recurringAppointmentsObj.Detail = item.Detail;
                    recurringAppointmentsObj.FutureInstances = dates;

                    query2.Add(recurringAppointmentsObj);
                }

                if (query2.Count() > 0)
                {
                    query2 = query2.OrderByDescending(x => x.RecurringAppointmentId).ToList();
                }
            }

            return query2;
        }
    }
}
