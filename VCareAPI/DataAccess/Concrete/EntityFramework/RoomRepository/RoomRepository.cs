using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IRoomRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.RoomEntity;
using Entities.Dtos.RoomDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.RoomRepository
{
    public class RoomRepository : EfEntityRepositoryBase<Room, ProjectDbContext>, IRoomRepository
    {
        public RoomRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<ProviderRoomDto>> GetAllRoomsByProviderId(int? ProviderId)
        {
            List<ProviderRoomDto> query = new List<ProviderRoomDto>();

            var sql = $@"select distinct r.RoomId, r.RoomName, r.RoomNumber
                from Rooms r
                left join Appointment a on r.RoomId = a.RoomNo and ISNULL(r.IsDeleted,0)=0
                left join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0 
                where ISNULL(a.IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                if (ProviderId > 0)
                {
                    sql = sql + "and a.ProviderId = " + ProviderId;
                }

                query = connection.Query<ProviderRoomDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.RoomId).ToList();
            }

            return query;
        }

        public async Task<List<AssignedRoomDto>> GetAssignedRoomList()
        {
            List<AssignedRoomDto> query = new List<AssignedRoomDto>();

            var sql = $@"select distinct r.RoomId, r.RoomNumber, p.FirstName + ' ' + p.LastName as ProviderName, r.TimeFrom, r.TimeTo
                from Rooms r
                inner join Appointment a on r.RoomId = a.RoomNo
                inner join Provider p on a.ProviderId = p.ProviderId
                where ISNULL(r.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 and ISNULL(p.IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AssignedRoomDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.RoomId).ToList();
            }

            return query;
        }

        public async Task<List<AvialableRoomDto>> GetAvailableRoomsList(int? RoomId, DateTime? Date)
        {
            List<AvialableRoomDto> query = new List<AvialableRoomDto>();

            var sql = $@"select distinct r.RoomId, r.RoomNumber, r.RoomName, FORMAT(r.CreatedDate, 'MMM dd, yyyy') as CreatedDate, 
                FORMAT(r.TimeFrom, 'hh:mm tt') as TimeFrom, FORMAT(r.TimeTo, 'hh:mm tt') as TimeTo
                from Rooms r
                left join Appointment a on r.RoomId = a.RoomNo
                where a.RoomNo is null and ISNULL(r.IsDeleted,0)=0 ";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AvialableRoomDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                if (RoomId > 0)
                {
                    query = query.Where(s => s.RoomId == RoomId).ToList();
                }

                if (Date != null)
                {
                    DateTime d = (DateTime)Date;
                    var searchByDate = d.ToString("MMM dd, yyyy");
                    query = query.Where(s => s.CreatedDate == searchByDate).ToList();
                }
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.RoomId).ToList();
            }

            return query;
        }

        public async Task<List<RoomPatientCountDto>> GetRoomPatientCountList(int? ProviderId)
        {
            List<RoomPatientDto> query = new List<RoomPatientDto>();
            List<RoomPatientCountDto> query2 = new List<RoomPatientCountDto>();

            var sql = $@"select r.RoomId, r.RoomName, r.RoomNumber, p.PatientId, p.FirstName + ' ' + p.LastName as PatientName, a.ProviderId
                    from Rooms r
                    left join Appointment a on r.RoomId = a.RoomNo and ISNULL(a.IsDeleted,0)=0 and ISNULL(r.IsDeleted,0)=0
                    left join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0 ";

            using (var connection = Context.Database.GetDbConnection())
            {
                if (ProviderId > 0)
                {
                    sql = sql + "left join Provider pd on a.ProviderId = pd.ProviderId and ISNULL(pd.IsDeleted,0)=0 where pd.ProviderId = " + ProviderId;
                }

                query = connection.Query<RoomPatientDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                var roomIds = query.Select(x => x.RoomId).Distinct().ToList();
                foreach (var roomId in roomIds)
                {
                    int patientList = query.Where(x => x.RoomId == roomId).ToList().Count();
                    var rommData = query.Where(x => x.RoomId == roomId).FirstOrDefault();
                    RoomPatientCountDto roomPatientCountDto = new RoomPatientCountDto
                    {
                        RoomId = rommData.RoomId,
                        RoomName = rommData.RoomName,
                        RoomNumber = rommData.RoomNumber,
                        PatientCount = patientList,
                    };

                    query2.Add(roomPatientCountDto);
                }
            }

            return query2;
        }

        public async Task<List<RoomPatientDto>> GetRoomPatientList(int? ProviderId)
        {
            List<RoomPatientDto> query = new List<RoomPatientDto>();

            var sql = $@"select r.RoomId, r.RoomName, r.RoomNumber, p.PatientId, p.FirstName + ' ' + p.LastName as PatientName, a.ProviderId
                    from Rooms r
                    inner join Appointment a on r.RoomId = a.RoomNo and ISNULL(r.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0
                    inner join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0";

            using (var connection = Context.Database.GetDbConnection())
            {
                if (ProviderId > 0)
                {
                    sql = sql + "left join Provider pd on a.ProviderId = pd.ProviderId and ISNULL(pd.IsDeleted,0)=0 where a.ProviderId = " + ProviderId;
                }

                query = connection.Query<RoomPatientDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.RoomId).ToList();
            }

            return query;
        }
    }
}
