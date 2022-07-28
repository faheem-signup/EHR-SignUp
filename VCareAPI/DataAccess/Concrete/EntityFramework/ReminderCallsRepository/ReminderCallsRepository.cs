using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IReminderCallsRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ReminderCallEntity;
using Entities.Dtos.ReminderCallDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ReminderCallsRepository
{
    public class ReminderCallsRepository : EfEntityRepositoryBase<ReminderCall, ProjectDbContext>, IReminderCallsRepository
    {
        public ReminderCallsRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<ReminderCallDto>> GetReminderCallsById(int? PatientId, DateTime? AppointmentFrom, DateTime? AppointmentTo, int? ReminderStatusId)
        {
            List<ReminderCallDto> query = new List<ReminderCallDto>();
            var sql = $@"select r.ReminderCallId, r.AppointmentDate, r.PatientId, p.FirstName + ' ' + p.LastName as PatientName, p.DOB, rs.ReminderStatusId, 
                rs.[Description] as ReminderStatusName, rt.ReminderTypeId, rt.[Description] as ReminderTypeName
                from ReminderCalls r
                inner join Patients p on r.PatientId = p.PatientId
                inner join Appointment a on a.PatientId = p.PatientId
                inner join ReminderStatuses rs on r.ReminderStatusId = rs.ReminderStatusId
                inner join ReminderType rt on r.ReminderTypeId = rt.ReminderTypeId";

            using (var connection = Context.Database.GetDbConnection())
            {
                if (AppointmentFrom != null && AppointmentTo != null)
                {
                    sql = sql + "where a.AppointmentDate between '" + AppointmentFrom + "' and '" + AppointmentTo + "'";
                }

                query = connection.Query<ReminderCallDto>(sql).ToList();
            }

            if (query.Count > 0)
            {
                if (query.Count() > 0)
                {
                    query = query.OrderByDescending(x => x.ReminderCallId).ToList();
                }

                if (PatientId > 0)
                {
                    query = query.Where(s => s.PatientId == PatientId).ToList();
                }

                if (ReminderStatusId > 0)
                {
                    query = query.Where(s => s.ReminderStatusId == ReminderStatusId).ToList();
                }
            }

            return query;
        }
    }
}
