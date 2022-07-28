using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.FollowUpAppointmentEntity;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Entities.Concrete.RecurringAppointmentsEntity;
using Entities.Dtos.AppointmentDto;
using Entities.Dtos.PatientDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.AppointmentRepository
{
    public class AppointmentRepository : EfEntityRepositoryBase<Appointment, ProjectDbContext>, IAppointmentRepository
    {
        public AppointmentRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<AppointmentDTO> GetAppointmentDetailById(int appointmentId)
        {
            AppointmentDTO appointmentDTOObj = new AppointmentDTO();
            List<AppointmentDTO> appointmentDTOList = new List<AppointmentDTO>();

            try
            {
                string sql = $@"select  apt.AppointmentId, 
                    apt.AllowGroupPatient,
                    apt.AppointmentDate,
                    apt.TimeFrom,
                    apt.TimeTo,
                    apt.ProviderId,
                    apt.LocationId,
                    apt.AppointmentStatus,
                    apt.VisitReason,
                    apt.RoomNo,
                    apt.GroupAppointmentReason,
                    apt.Notes,
                    apt.PatientId,
                    apt.IsRecurringAppointment,
                    apt.IsFollowUpAppointment,
                    ga.GroupAppointmentId,
                    ga.PatientId as PatientId,
                    ra.RecurringAppointmentId,
                    ra.Weekdays,
                    ra.RecurEvery,
                    ra.WeekType,
                    ra.RecurringVisitReason,
                    ra.FirstAppointDate,
                    ra.LastAppointDate,
                    fa.FollowUpId ,
                    fa.FollowUpDate,
                    fa.FollowUpsVisitReason,
                    apt.StatusReasons,
                    apt.Duration
                 
                    from
                    dbo.Appointment apt
                   left join GroupPatientAppointment ga on apt.AppointmentId = ga.AppointmentId
				   left join RecurringAppointments ra on apt.AppointmentId = ra.AppointmentId
				   left  join FollowUpAppointment fa on apt.AppointmentId = fa.AppointmentId

				   where apt.AppointmentId = {appointmentId}";


                var connection = Context.Database.GetDbConnection();

                try
                {

                    var data = await connection.QueryAsync<AppointmentDTO>(sql, new[]
                    {
                    typeof(AppointmentDTO),
                    typeof(GroupPatientAppointment),
                    typeof(RecurringAppointments),
                    typeof(FollowUpAppointment),
                },
                    objects =>
                    {
                        var apt = objects[0] as AppointmentDTO;
                        var ga = objects[1] as GroupPatientAppointment;
                        var ra = objects[2] as RecurringAppointments;
                        var fa = objects[3] as FollowUpAppointment;

                        apt.RecurringAppointments = ra;
                        apt.FollowUpAppointments = fa;
                        if (ga != null)
                        {
                            apt.GroupPatientAppointment = ga;
                        }

                        return apt;
                    },
                    splitOn: "AppointmentId,GroupAppointmentId,RecurringAppointmentId,FollowUpId"
                    );

                    if (data.FirstOrDefault().GroupPatientAppointment != null)
                    {
                        List<GroupPatientAppointment> groupPatientAppointmentList = new List<GroupPatientAppointment>();
                        data.ToList().ForEach(a =>
                        {
                            groupPatientAppointmentList.Add(a.GroupPatientAppointment);
                        });
                        data.FirstOrDefault().GroupPatientAppointmentList = groupPatientAppointmentList;
                        data.FirstOrDefault().GroupPatientAppointment = null;
                    }

                    return data.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int patientId)
        {
            var list = await Context.Appointment
                .Include(x => x.Provider)
                .Include(x => x.Locations)
                .Include(x => x.Patients)
                .Where(x => x.PatientId == patientId)
                .OrderByDescending(x => x.AppointmentId)
                .ToListAsync();
            return list;
        }

        public async Task<List<AppointmentScheduleDto>> GetAppointmentScheduleList(int ProviderId)
        {
            List<AppointmentScheduleDto> query = new List<AppointmentScheduleDto>();
            var sql = $@"SELECT apt.[AppointmentId]
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
	                       ,(ISNULL(pr.FirstName,'')+ ISNULL(' '+pr.LastName,'')) as ProviderName
	                       ,sprf.ServiceProfileName
	                       ,lc.LocationName
	                       ,aptp.AppointmentTypeName
	                       ,aprsn.AppointmentReasonDescription as AppointmentReason
	                       ,rm.RoomName
	                       ,aptst.AppointmentStatusName
	                       ,(ISNULL(pt.FirstName,'')+ ISNULL(' '+pt.MiddleName,'')+ ISNULL(' '+pt.LastName,'')) as PatientName
	                       ,pt.DOB
	                       ,pt.[CellPhone]
                           ,pt.[HomePhone]
                           ,pt.[WorkPhone]
	                       ,(select GenderName from dbo.Genders where GenderId= pt.Gender) as GenderName
                           ,Convert(varchar,apt.Duration) as Duration
						  
						   ,apt.[CreatedBy]
						   ,(select (ISNULL(FirstName,'')+ ISNULL(' '+MiddleName,'')+ ISNULL(' '+LastName,'')) as CreatedBy from dbo.UserApp where UserId = apt.[CreatedBy]) as CreatedBy
						   ,apt.[CreatedDate]
						   ,apt.[ModifiedBy]
						   ,(select (ISNULL(FirstName,'')+ ISNULL(' '+MiddleName,'')+ ISNULL(' '+LastName,'')) as ModifiedBy from dbo.UserApp where UserId =apt.[ModifiedBy]) as ModifiedBy
						   ,apt.[ModifiedDate]
						   ,pr.Degree
                        FROM [dbo].[Appointment] apt

                        left join dbo.[Provider] pr
                        on pr.ProviderId= apt.ProviderId
                        left join dbo.ServiceProfile sprf
                        on sprf.ServiceProfileId= apt.ServiceProfileId
                        left join dbo.Locations lc
                        on lc.LocationId= apt.LocationId
                        left join dbo.AppointmentTypes aptp
                        on aptp.AppointmentTypeId= apt.VisitType
                        left join dbo.AppointmentReasons aprsn
                        on aprsn.AppointmentReasonId= apt.VisitReason
                        left join dbo.Rooms rm
                        on rm.RoomId= apt.RoomNo
                        left join dbo.AppointmentStatuses aptst
                        on aptst.AppointmentStatusId= apt.AppointmentStatus
                        left join dbo.Patients pt
                        on pt.PatientId= apt.PatientId
                        where apt.ProviderId =" + ProviderId + " AND ISNULL(apt.IsDeleted,0)=0";

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<AppointmentScheduleDto>(sql).ToList();
            return query;
        }

        public async Task<List<GroupPatientAppointmentDto>> GetGroupPatientAppointmentList(int AppointmentId)
        {
            List<GroupPatientAppointmentDto> query = new List<GroupPatientAppointmentDto>();
            var sql = $@"SELECT gapt.[GroupAppointmentId]
                         ,gapt.[PatientId]
                         ,gapt.[AppointmentId]
	                     ,(ISNULL(pt.FirstName,'')+ ISNULL(' '+pt.MiddleName,'')+ ISNULL(' '+pt.LastName,'')) as PatientName
	                     ,pt.DOB
	                     ,pt.[CellPhone]
                         ,pt.[HomePhone]
                         ,pt.[WorkPhone]
	                     ,(select GenderName from dbo.Genders where GenderId= pt.Gender) as GenderName
                         FROM [dbo].[GroupPatientAppointment] gapt
                         inner join dbo.Patients pt
                         on pt.PatientId = gapt.PatientId
                         where gapt.AppointmentId=" + AppointmentId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<GroupPatientAppointmentDto>(sql).ToList();
            //using (var connection = Context.Database.GetDbConnection())
            //{
            //    query = connection.Query<GroupPatientAppointmentDto>(sql).ToList();
            //}

            return query;
        }

        public async Task<AppointmentDTO> GetAppointemntById(int AppointmentId)
        {
            List<AppointmentDTO> query = new List<AppointmentDTO>();
            var sql = $@"SELECT [AppointmentId]
                    ,[AllowGroupPatient]
                    ,[AppointmentDate]
                    ,[TimeFrom]
                    ,[TimeTo]
                    ,[ProviderId]
                    ,[LocationId]
                    ,[ServiceProfileId]
                    ,[AppointmentStatus]
                    ,[VisitType]
                    ,[VisitReason]
                    ,[RoomNo]
                    ,[GroupAppointmentReason]
                    ,[Notes]
                    ,[PatientId]
                    ,[IsRecurringAppointment]
                    ,[IsFollowUpAppointment]
	                ,Convert(varchar,Duration) as Duration
                    ,[StatusReasons]
                    
                    FROM [dbo].[Appointment]
                    where ISNULL(IsDeleted,0)=0 and AppointmentId =" + AppointmentId;

            //using (var connection = Context.Database.GetDbConnection())
            //{
            //    query = connection.Query<AppointmentDTO>(sql).ToList();
            //}

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<AppointmentDTO>(sql).ToList();
            return query.FirstOrDefault();
        }

        public async Task<AppointmentStatus> GetAppointmentStatusById(int AppointmentStatusID)
        {
            return Context.AppointmentStatuses.Where(x => x.AppointmentStatusId == AppointmentStatusID).FirstOrDefault();
        }

        public async Task<List<AppointmentScheduleDto>> GetAllAppointments(DateTime FromDate, DateTime ToDate)
        {
            List<AppointmentScheduleDto> query = new List<AppointmentScheduleDto>();
            var sql = $@"SELECT apt.[AppointmentId]
                     ,apt.[AppointmentDate]
                     ,apt.[TimeFrom]
                     ,apt.[TimeTo]
                     ,apt.[ProviderId]
                     ,apt.[LocationId]
                     ,apt.[ServiceProfileId]
                     ,apt.[PatientId]
                	  ,(ISNULL(pr.FirstName,'')+ ISNULL(' '+pr.LastName,'')) as ProviderName
                	  ,lt.LocationName
                 FROM [dbo].[Appointment] apt
                 INNER JOIN dbo.Provider pr
                 ON pr.ProviderId = apt.ProviderId
                 LEFT JOIN dbo.Locations lt
                 ON lt.LocationId = apt.LocationId
                 where ISNULL(apt.IsDeleted,0)=0";// and cast(apt.AppointmentDate as date) BETWEEN cast(" + FromDate + " as date) And cast(" + ToDate + " as date)";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AppointmentScheduleDto>(sql).ToList();
            }

            return query;
        }

        public async Task<List<AppointmentByDateRangeDto>> GetAppointmentByDateRange(int ProviderId, DateTime FromDate, DateTime ToDate, bool flag)
        {
            var sql = $@"select AppointmentId, AppointmentDate, TimeFrom, TimeTo 
                from Appointment 
                where ISNULL(IsDeleted,0)=0 and ProviderId = " + ProviderId + " and AllowGroupPatient = '" + flag
                + "' and AppointmentDate between '" + FromDate + "' and '" + ToDate + "'";

            var connection = Context.Database.GetDbConnection();
            List<AppointmentByDateRangeDto> query = connection.Query<AppointmentByDateRangeDto>(sql).ToList();

            return query;
        }

        public async Task<AppointmentDTO> GetAppointemntDetailByPatientId(int PatientId)
        {
            List<AppointmentDTO> query = new List<AppointmentDTO>();
            var sql = $@"SELECT apt.[AppointmentId]
                     ,apt.[AppointmentDate]
                     ,apt.[TimeFrom]
                     ,apt.[TimeTo]
                     ,apt.[ProviderId]
                     ,apt.[LocationId]
                     ,apt.[ServiceProfileId]
                     ,apt.[PatientId]
                	  ,(ISNULL(pr.FirstName,'')+ ISNULL(' '+pr.LastName,'')) as ProviderName
                	  ,lt.LocationName
					  ,(select AppointmentStatusName from dbo.AppointmentStatuses where AppointmentStatusId= apt.[AppointmentStatus]) as AppointmentStatusName
					  ,(select AppointmentReasonDescription from dbo.AppointmentReasons where AppointmentReasonId = apt.[VisitReason])  as AppointmentReasonDescription
                 FROM [dbo].[Appointment] apt
                 INNER JOIN dbo.Provider pr
                 ON pr.ProviderId = apt.ProviderId
                 LEFT JOIN dbo.Locations lt
                 ON lt.LocationId = apt.LocationId
                 where ISNULL(apt.IsDeleted,0)=0 AND apt.PatientId = " + PatientId + " order by apt.AppointmentDate desc";

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<AppointmentDTO>(sql).ToList();
            return query.FirstOrDefault();
        }

        public async Task<List<AppointmentByDateRangeDto>> GetAppointmentByDateRangeAndProviderId(int ProviderId, DateTime FromDate, DateTime ToDate, int LocationId)
        {
            var sql = $@"select AppointmentId, AppointmentDate, TimeFrom, TimeTo 
                from Appointment 
                where ISNULL(IsDeleted,0)=0 and ProviderId = " + ProviderId + " and LocationId = '" + LocationId
                + "' and AppointmentDate between '" + FromDate + "' and '" + ToDate + "'";

            var connection = Context.Database.GetDbConnection();
            List<AppointmentByDateRangeDto> query = connection.Query<AppointmentByDateRangeDto>(sql).ToList();

            return query;
        }

        public async Task<List<AppointmentScheduleDto>> GetAppointmentScheduleListByDateRangeAndProviderId(int ProviderId, DateTime FromDate, DateTime ToDate)
        {
            List<AppointmentScheduleDto> query = new List<AppointmentScheduleDto>();
            var sql = $@"SELECT apt.[AppointmentId]
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
	                       ,(ISNULL(pr.FirstName,'')+ ISNULL(' '+pr.LastName,'')) as ProviderName
	                       ,sprf.ServiceProfileName
	                       ,lc.LocationName
	                       ,aptp.AppointmentTypeName
	                       ,aprsn.AppointmentReasonDescription as AppointmentReason
	                       ,rm.RoomName
	                       ,aptst.AppointmentStatusName
	                       ,(ISNULL(pt.FirstName,'')+ ISNULL(' '+pt.MiddleName,'')+ ISNULL(' '+pt.LastName,'')) as PatientName
	                       ,pt.DOB
	                       ,pt.[CellPhone]
                           ,pt.[HomePhone]
                           ,pt.[WorkPhone]
	                       ,(select GenderName from dbo.Genders where GenderId= pt.Gender) as GenderName
                           ,Convert(varchar,apt.Duration) as Duration
						  
						   ,apt.[CreatedBy]
						   ,(select (ISNULL(FirstName,'')+ ISNULL(' '+MiddleName,'')+ ISNULL(' '+LastName,'')) as CreatedBy from dbo.UserApp where UserId = apt.[CreatedBy]) as CreatedBy
						   ,apt.[CreatedDate]
						   ,apt.[ModifiedBy]
						   ,(select (ISNULL(FirstName,'')+ ISNULL(' '+MiddleName,'')+ ISNULL(' '+LastName,'')) as ModifiedBy from dbo.UserApp where UserId =apt.[ModifiedBy]) as ModifiedBy
						   ,apt.[ModifiedDate]
						   ,pr.Degree
                        FROM [dbo].[Appointment] apt

                        left join dbo.[Provider] pr
                        on pr.ProviderId= apt.ProviderId
                        left join dbo.ServiceProfile sprf
                        on sprf.ServiceProfileId= apt.ServiceProfileId
                        left join dbo.Locations lc
                        on lc.LocationId= apt.LocationId
                        left join dbo.AppointmentTypes aptp
                        on aptp.AppointmentTypeId= apt.VisitType
                        left join dbo.AppointmentReasons aprsn
                        on aprsn.AppointmentReasonId= apt.VisitReason
                        left join dbo.Rooms rm
                        on rm.RoomId= apt.RoomNo
                        left join dbo.AppointmentStatuses aptst
                        on aptst.AppointmentStatusId= apt.AppointmentStatus
                        left join dbo.Patients pt
                        on pt.PatientId= apt.PatientId
                        where ISNULL(apt.IsDeleted,0)=0 and aptst.AppointmentStatusName !='Cancelled' and apt.ProviderId = " + ProviderId + " and AppointmentDate between '" + FromDate + "' and '" + ToDate + "'";
                       // where ISNULL(apt.IsDeleted,0)=0 and aptst.AppointmentStatusName !='Cancelled' and aptst.AppointmentStatusName !='Rescheduled' and apt.ProviderId = " + ProviderId + " and AppointmentDate between '" + FromDate + "' and '" + ToDate + "'";

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<AppointmentScheduleDto>(sql).ToList();
            return query;
        }
    }
}
