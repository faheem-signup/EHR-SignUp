using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.ISchedulerRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.SchedulerEntity;
using Entities.Dtos.DashboardDto;
using Entities.Dtos.LocationDto;
using Entities.Dtos.ProviderDto;
using Entities.Dtos.SchedulerDto;
using Entities.Dtos.UesrAppDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.SchedulerRepository
{
    public class SchedulerRepository : EfEntityRepositoryBase<AppointmentScheduler, ProjectDbContext>, ISchedulerRepository
    {
        public SchedulerRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<ProviderStatusSummaryDto>> GetProviderStatusSummary(int? ProviderId, string fromDate, string toDate)
        {
            List<ProviderStatusSummaryDto> query = new List<ProviderStatusSummaryDto>();
            List<ProviderStatusSummaryDto> query2 = new List<ProviderStatusSummaryDto>();
            var sql = $@"select a.AppointmentId, a.AppointmentStatus, aps.AppointmentStatusName,  p.ProviderId, p.FirstName + ' ' + p.LastName as ProviderName,p.ProviderEmail
                from Appointment a
                left join Provider p on a.ProviderId = p.ProviderId
                left join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId
                where ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 and a.AppointmentDate between '" + fromDate + "' and '" + toDate + "' ";
            //using (var connection = Context.Database.GetDbConnection())
            //{
            var connection = Context.Database.GetDbConnection();
                if (ProviderId > 0 && ProviderId != null)
                {
                    sql += " and a.ProviderId = " + ProviderId;
                }

                query = connection.Query<ProviderStatusSummaryDto>(sql).ToList();
            //}

            if (query.Count() > 0)
            {
                int totalAppointment = 0;
                int checkedIn = 0;
                int checkedOut = 0;
                int cancelled = 0;
                int rescheduled = 0;
                int scheduled = 0;
                int noShow = 0;
                var providerIds = query.Select(x => x.ProviderId).Distinct().ToList();
                foreach (var providerId in providerIds)
                {
                    totalAppointment = query.Where(x => x.ProviderId == providerId).Select(x => x.AppointmentId).ToList().Count();
                    var providerData = query.Where(x => x.ProviderId == providerId).FirstOrDefault();
                    checkedIn = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Checked In").ToList().Count();
                    checkedOut = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Checked Out").ToList().Count();
                    cancelled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Cancelled").ToList().Count();
                    rescheduled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Rescheduled").ToList().Count();
                    noShow = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "No Show").ToList().Count();
                    scheduled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Scheduled").ToList().Count();

                    ProviderStatusSummaryDto providerStatusSummary = new ProviderStatusSummaryDto
                    {
                        ProviderId = providerData.ProviderId,
                        ProviderName = providerData.ProviderName,
                        TotalProvierAppointment = totalAppointment,
                        TotalProvierCompletedAppointment = checkedOut,
                        TotalProvierScheduledAppointment = scheduled,
                        TotalProvierCancelledAppointment = cancelled,
                        TotalProvierCheckedInAppointment = checkedIn,
                        TotalProvierRescheduledAppointment = rescheduled,
                        TotalProvierNoShowAppointment = noShow,
                        TotalAppointment = totalAppointment,
                        TotalCheckedIn = checkedIn,
                        TotalCheckedOut = checkedOut,
                        TotalCancelled = cancelled,
                        TotalScheduled = scheduled,
                        TotalNoShow = noShow,
                        TotalRescheduled = rescheduled,
                        ProviderEmail = providerData.ProviderEmail
                    };

                    query2.Add(providerStatusSummary);
                }

                totalAppointment = (int)query2.Select(x => x.TotalAppointment).Sum();
                checkedIn = (int)query2.Select(x => x.TotalCheckedIn).Sum();
                checkedOut = (int)query2.Select(x => x.TotalCheckedOut).Sum();
                cancelled = (int)query2.Select(x => x.TotalCancelled).Sum();
                rescheduled = (int)query2.Select(x => x.TotalRescheduled).Sum();
                scheduled = (int)query2.Select(x => x.TotalScheduled).Sum();
                noShow = (int)query2.Select(x => x.TotalNoShow).Sum();

                query2.ToList().ForEach(x => x.TotalAppointment = totalAppointment);
                query2.ToList().ForEach(x => x.TotalCheckedIn = checkedIn);
                query2.ToList().ForEach(x => x.TotalCheckedOut = checkedOut);
                query2.ToList().ForEach(x => x.TotalCancelled = cancelled);
                query2.ToList().ForEach(x => x.TotalRescheduled = rescheduled);
                query2.ToList().ForEach(x => x.TotalScheduled = scheduled);
                query2.ToList().ForEach(x => x.TotalNoShow = noShow);
            }
            else
            {
                ProviderStatusSummaryDto providerStatusSummary = new ProviderStatusSummaryDto
                {
                    TotalProvierAppointment = 0,
                    TotalProvierCompletedAppointment = 0,
                    TotalProvierScheduledAppointment = 0,
                    TotalProvierCancelledAppointment = 0,
                    TotalProvierCheckedInAppointment = 0,
                    TotalProvierRescheduledAppointment = 0,
                    TotalProvierNoShowAppointment = 0,
                    TotalAppointment = 0,
                    TotalCheckedIn = 0,
                    TotalCheckedOut = 0,
                    TotalCancelled = 0,
                    TotalRescheduled = 0,
                    TotalScheduled = 0,
                    TotalNoShow = 0,
                };

                query2.Add(providerStatusSummary);
            }

            if (query2.Count() > 0)
            {
                query2 = query2.OrderByDescending(x => x.ProviderId).ToList();
            }

            return query2;
        }

        public async Task<ProviderStatusSummaryDto> GetProviderStatusSummaryById(int ProviderId, string fromDate, string toDate)
        {
            List<ProviderStatusSummaryDto> query = new List<ProviderStatusSummaryDto>();
            ProviderStatusSummaryDto query2 = new ProviderStatusSummaryDto();
            var sql = $@"select a.AppointmentId, a.AppointmentStatus, aps.AppointmentStatusName,  p.ProviderId, p.FirstName + ' ' + p.LastName as ProviderName
                from Appointment a
                inner join Provider p on a.ProviderId = p.ProviderId
                inner join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId 
                where ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 
                and a.ProviderId = " + ProviderId + " and a.AppointmentDate between '" + fromDate + "' and '" + toDate + "' ";
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ProviderStatusSummaryDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                int totalAppointment = 0;
                int checkedIn = 0;
                int checkedOut = 0;
                int cancelled = 0;
                int rescheduled = 0;
                int scheduled = 0;
                int noShow = 0;
                var providerIds = query.Select(x => x.ProviderId).Distinct().ToList();
                foreach (var providerId in providerIds)
                {
                    totalAppointment = query.Where(x => x.ProviderId == providerId).Select(x => x.AppointmentId).ToList().Count();
                    var providerData = query.Where(x => x.ProviderId == providerId).FirstOrDefault();
                    checkedIn = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Checked In").ToList().Count();
                    checkedOut = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Checked Out").ToList().Count();
                    cancelled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Cancelled").ToList().Count();
                    rescheduled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Rescheduled").ToList().Count();
                    noShow = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "No Show").ToList().Count();
                    scheduled = query.Where(x => x.ProviderId == providerId && x.AppointmentStatusName == "Scheduled").ToList().Count();

                    query2 = new ProviderStatusSummaryDto
                    {
                        ProviderId = providerData.ProviderId,
                        ProviderName = providerData.ProviderName,
                        TotalProvierAppointment = totalAppointment,
                        TotalProvierCompletedAppointment = checkedOut,
                        TotalProvierScheduledAppointment = scheduled,
                        TotalProvierCancelledAppointment = cancelled,
                        TotalProvierCheckedInAppointment = checkedIn,
                        TotalProvierRescheduledAppointment = rescheduled,
                        TotalProvierNoShowAppointment = noShow,
                        TotalAppointment = totalAppointment,
                        TotalCheckedIn = checkedIn,
                        TotalCheckedOut = checkedOut,
                        TotalCancelled = cancelled,
                        TotalRescheduled = rescheduled,
                        TotalScheduled = scheduled,
                        TotalNoShow = noShow,
                    };
                }
            }
            else
            {
                query2 = new ProviderStatusSummaryDto
                {
                    TotalProvierAppointment = 0,
                    TotalProvierCompletedAppointment = 0,
                    TotalProvierScheduledAppointment = 0,
                    TotalProvierCancelledAppointment = 0,
                    TotalProvierCheckedInAppointment = 0,
                    TotalProvierRescheduledAppointment = 0,
                    TotalProvierNoShowAppointment = 0,
                    TotalAppointment = 0,
                    TotalCheckedIn = 0,
                    TotalCheckedOut = 0,
                    TotalCancelled = 0,
                    TotalRescheduled = 0,
                    TotalScheduled = 0,
                    TotalNoShow = 0,
                };
            }

            return query2;
        }

        public async Task<List<AppiontmentStatusDetailDto>> GetAppointmentStatusDetailById(int? ProviderId, string fromDate, string toDate, string AppointmentStatus)
        {
            List<AppiontmentStatusDetailDto> query = new List<AppiontmentStatusDetailDto>();
            var sql = $@"select a.AppointmentId, a.AppointmentDate, a.AppointmentStatus, aps.AppointmentStatusName,  
                p.ProviderId, p.FirstName + ' ' + p.LastName as ProviderName
                from Appointment a
                inner join Provider p on a.ProviderId = p.ProviderId
                inner join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId 
                where ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0 
                and aps.AppointmentStatusName = '" + AppointmentStatus + "' and a.AppointmentDate between '" + fromDate + "' and '" + toDate + "' ";

            using (var connection = Context.Database.GetDbConnection())
            {
                if (ProviderId > 0 && ProviderId != null)
                {
                    sql += " and a.ProviderId = " + ProviderId;
                }

                query = connection.Query<AppiontmentStatusDetailDto>(sql).ToList();
            }

            return query;
        }

        public async Task<List<LocationLookupDto>> GetLocationByProviderId(int ProviderId)
        {
            List<LocationLookupDto> query = new List<LocationLookupDto>();
            var sql = $@"select pci.LocationId ,lc.LocationName from dbo.ProviderClinicalInfo pci
                        inner join dbo.Locations lc
                        on lc.LocationId = pci.LocationId
                        where pci.ProviderId =" + ProviderId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<LocationLookupDto>(sql).ToList();
            }

            return query;
        }

        public async Task<List<LocationLookupDto>> GetLocationByPracticeId(int PracticeId)
        {
            List<LocationLookupDto> query = new List<LocationLookupDto>();
            var sql = $@"select LocationId,LocationName from dbo.Locations where PracticeId =" + PracticeId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<LocationLookupDto>(sql).ToList();
            }

            return query;
        }

        public async Task<RolesDto> GetRoleByUserId(int UserId)
        {
            RolesDto query = new RolesDto();
            var sql = $@"select rl.RoleName 
                        from UserApp ua 
                        inner join Roles rl on rl.RoleId = ua.RoleId
                        where ua.UserId =" + UserId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<RolesDto>(sql).FirstOrDefault();
            return query;
        }

        public async Task<List<ProviderIDsDto>> GetProviderIDs(int? ProviderId)
        {
            List<ProviderIDsDto> query = new List<ProviderIDsDto>();
            var sql = $@"select distinct pr.ProviderId as providerID 
                    from Provider pr
                    inner join ProviderWorkConfig prw on prw.ProviderId = pr.ProviderId
                    inner join Appointment apt on apt.ProviderId = apt.ProviderId 
                    and ISNULL(pr.IsDeleted,0)=0 and ISNULL(apt.IsDeleted,0)=0 ";
            if (ProviderId > 0 && ProviderId != null)
            {
                sql += " where pr.ProviderId = " + ProviderId;
            }

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<ProviderIDsDto>(sql).ToList();
            return query;
        }

        public async Task<List<PatientAppointmentStatusDto>> GetAppointmentCheckinStatus(int ProviderId)
        {
            List<PatientAppointmentStatusDto> query = new List<PatientAppointmentStatusDto>();
            var sql = $@"select a.AppointmentId, a.AppointmentStatus, aps.AppointmentStatusName, p.FirstName + ' ' + p.LastName as PatientName, 
				p.PatientId, a.RoomNo as RoomId, r.RoomNumber, apc.AppointmentCheckInDateTime, a.ProviderId
                from Appointment a
                left join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0
                left join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId and ISNULL(a.IsDeleted,0)=0
				left join AppointmentsCheckIn apc on a.AppointmentId = apc.AppointmentId 
				left join Rooms r on a.RoomNo = r.RoomId and ISNULL(r.IsDeleted,0)=0
				where p.PatientId is not null and a.AppointmentStatus = (select AppointmentStatusId from AppointmentStatuses where AppointmentStatusName = 'Checked In') 
				and apc.AppointmentCheckInDateTime is not null and a.ProviderId = " + ProviderId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientAppointmentStatusDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.AppointmentId).ToList();
            }

            return query;
        }

        public async Task<ProviderStatusSummaryDto> GetProviderAppointmentsDashboard(int ProviderId, string fromDate, string toDate)
        {
            List<ProviderStatusSummaryDto> query = new List<ProviderStatusSummaryDto>();
            ProviderStatusSummaryDto query2 = new ProviderStatusSummaryDto();
            var sql = $@"select a.AppointmentId, a.AppointmentStatus, aps.AppointmentStatusName,  p.ProviderId, p.FirstName + ' ' + p.LastName as ProviderName
                from Appointment a
                left join Provider p on a.ProviderId = p.ProviderId and ISNULL(p.IsDeleted,0)=0
                left join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId and ISNULL(a.IsDeleted,0)=0
                where a.ProviderId = " + ProviderId + "and a.AppointmentDate between '" + fromDate + "' and '" + toDate + "'";
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ProviderStatusSummaryDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                int totalAppointment = 0;
                int checkedIn = 0;
                int checkedOut = 0;
                int cancelled = 0;
                int rescheduled = 0;
                int scheduled = 0;
                int noShow = 0;

                checkedIn = query.Where(x => x.AppointmentStatusName == "Checked In").ToList().Count();
                checkedOut = query.Where(x => x.AppointmentStatusName == "Checked Out").ToList().Count();
                cancelled = query.Where(x => x.AppointmentStatusName == "Cancelled").ToList().Count();
                rescheduled = query.Where(x => x.AppointmentStatusName == "Rescheduled").ToList().Count();
                noShow = query.Where(x => x.AppointmentStatusName == "No Show").ToList().Count();
                scheduled = query.Where(x => x.AppointmentStatusName == "Scheduled").ToList().Count();
                //   totalAppointment = checkedIn + checkedOut + cancelled + rescheduled + noShow;
                totalAppointment = query.Count();
                query2 = new ProviderStatusSummaryDto
                {
                    TotalAppointment = totalAppointment,
                    TotalCheckedIn = checkedIn,
                    TotalCheckedOut = checkedOut,
                    TotalCancelled = cancelled,
                    TotalRescheduled = rescheduled,
                    TotalNoShow = noShow,
                    TotalScheduled = scheduled
                };
            }
            else
            {
                query2 = new ProviderStatusSummaryDto
                {
                    TotalAppointment = 0,
                    TotalCheckedIn = 0,
                    TotalCheckedOut = 0,
                    TotalCancelled = 0,
                    TotalRescheduled = 0,
                    TotalNoShow = 0,
                    TotalScheduled = 0
                };
            }

            return query2;
        }

        public async Task<List<AdminAppointmentsPieChartDto>> GetAdminAppointmentsPieChartDashboard(string fromDate, string toDate)
        {
            List<AdminAppointmentsPieChartDto> query = new List<AdminAppointmentsPieChartDto>();
            var sql = $@"select COUNT(a.AppointmentId) as TotalAppointment, aps.AppointmentStatusName,
                case 
                when aps.AppointmentStatusName = 'Checked In' then COUNT(aps.AppointmentStatusName)
                when aps.AppointmentStatusName = 'Checked Out' then COUNT(aps.AppointmentStatusName)
                when aps.AppointmentStatusName = 'Cancelled' then COUNT(aps.AppointmentStatusName)
                when aps.AppointmentStatusName = 'Rescheduled' then COUNT(aps.AppointmentStatusName) 
                when aps.AppointmentStatusName = 'No Show' then COUNT(aps.AppointmentStatusName) 
                when aps.AppointmentStatusName = 'Scheduled' then COUNT(aps.AppointmentStatusName) 
                else 0
                end as AppointmentStatusCount
                
                from Appointment a
                left join Provider p on a.ProviderId = p.ProviderId and ISNULL(p.IsDeleted,0)=0
                left join AppointmentStatuses aps on a.AppointmentStatus = aps.AppointmentStatusId and ISNULL(a.IsDeleted,0)=0
                where a.AppointmentDate between '" + fromDate + "' and '" + toDate + "' " +
                "group by aps.AppointmentStatusName";
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<AdminAppointmentsPieChartDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                int total = 0;
                total = query.Sum(x => x.TotalAppointment);
                query.ToList().ForEach(x => x.TotalAppointment = total);

                //percentageCheckedIn = (int)Math.Round((double)(100 * checkedIn) / totalAppointment);
            }
            else
            {
                AdminAppointmentsPieChartDto pieChartObj = new AdminAppointmentsPieChartDto
                {
                    TotalAppointment = 0,
                    AppointmentStatusCount = 0,
                    AppointmentStatusName = "Scheduled",
                };

                query.Add(pieChartObj);
            }

            return query;
        }

        public async Task<List<AdminAppointmentsGraphChartDto>> GetAdminAppointmentsGraphChartDashboard()
        {
            List<AdminAppointmentsGraphChartDto> data = new List<AdminAppointmentsGraphChartDto>();

            int currentYear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++)
            {
                var fromDate = i + "/1/" + currentYear;
                DateTime date = DateTime.Parse(fromDate, CultureInfo.InvariantCulture);
                DateTime toDate = date.AddMonths(1).AddDays(-1);
                var toDateString = i + "/" + toDate.Day + "/" + currentYear;

                var sql = @"select COUNT(a.AppointmentId) as TotalAppointment, COUNT(a.PatientId) as TotalPatients
                        from Appointment a
                        inner join Patients pt on a.PatientId = pt.PatientId and ISNULL(pt.IsDeleted, 0) = 0 and ISNULL(a.IsDeleted,0)= 0
                        where a.AppointmentDate between '" + fromDate + "' and '" + toDateString + "'";

                var connection = Context.Database.GetDbConnection();
                PatientAppointmentCountDto query = connection.Query<PatientAppointmentCountDto>(sql).FirstOrDefault();

                if (query != null)
                {
                    AdminAppointmentsGraphChartDto patientCountObj = new AdminAppointmentsGraphChartDto
                    {
                        TotalAppointment = query.TotalAppointment,
                        TotalPatients = query.TotalPatients,
                        EndDate = toDateString,
                    };

                    data.Add(patientCountObj);
                }
                else
                {
                    AdminAppointmentsGraphChartDto patientCountObj = new AdminAppointmentsGraphChartDto
                    {
                        TotalAppointment = 0,
                        TotalPatients = 0,
                        EndDate = toDateString,
                    };

                    data.Add(patientCountObj);
                }
            }

            return data;
        }

        public async Task<List<AdminAppointmentsGraphChartDto>> GetProviderAppointmentsGraphChartDashboard(int ProviderId)
        {
            List<AdminAppointmentsGraphChartDto> data = new List<AdminAppointmentsGraphChartDto>();
            int currentYear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++)
            {
                var fromDate = i + "/1/" + currentYear;
                DateTime date = DateTime.Parse(fromDate, CultureInfo.InvariantCulture);
                DateTime toDate = date.AddMonths(1).AddDays(-1);
                var toDateString = i + "/" + toDate.Day + "/" + currentYear;

                var sql = $@"select COUNT(a.AppointmentId) as TotalAppointment, COUNT(a.PatientId) as TotalPatients
                        FROM Appointment a
                        inner join Provider p on a.ProviderId = p.ProviderId and ISNULL(p.IsDeleted,0)=0 and ISNULL(a.IsDeleted,0)=0
                        inner join Patients pt on a.PatientId = pt.PatientId and ISNULL(pt.IsDeleted,0)=0
                        where p.ProviderId = " + ProviderId + " and a.AppointmentDate between '" + fromDate + "' and '" + toDateString + "'";

                var connection = Context.Database.GetDbConnection();
                PatientAppointmentCountDto query = connection.Query<PatientAppointmentCountDto>(sql).FirstOrDefault();

                if (query != null)
                {
                    AdminAppointmentsGraphChartDto patientCountObj = new AdminAppointmentsGraphChartDto
                    {
                        TotalAppointment = query.TotalAppointment,
                        TotalPatients = query.TotalPatients,
                        EndDate = toDateString,
                    };

                    data.Add(patientCountObj);
                }
                else
                {
                    AdminAppointmentsGraphChartDto patientCountObj = new AdminAppointmentsGraphChartDto
                    {
                        TotalAppointment = 0,
                        TotalPatients = 0,
                        EndDate = toDateString,
                    };

                    data.Add(patientCountObj);
                }
            }

            return data;
        }

        public async Task<List<ProviderTypeDto>> GetAllProviderTypesByIdList(int ProviderId)
        {
            List<ProviderTypeDto> query = new List<ProviderTypeDto>();
            var sql = $@"select distinct Degree from Provider where ISNULL(IsDeleted,0)=0 and Degree is not null and ProviderId =" + ProviderId;
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ProviderTypeDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.Degree).ToList();
            }

            return query;
        }

        public async Task<List<ProviderTypeDto>> GetAllProviderTypesList()
        {
            List<ProviderTypeDto> query = new List<ProviderTypeDto>();
            var sql = $@"select distinct Degree from Provider where ISNULL(IsDeleted,0)=0 and Degree is not null";
            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<ProviderTypeDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.Degree).ToList();
            }

            return query;
        }

    }
}
