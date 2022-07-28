using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Dapper;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Permission;
using Entities.Concrete.Role;
using Entities.Concrete.User;
using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.UserToPermissions;
using Entities.Dtos;
using Entities.Dtos.UesrAppDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserAppRepository
{
   public class UserAppRepository : EfEntityRepositoryBase<UserApp, ProjectDbContext>, IUserAppRepository
    {
        public UserAppRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserApp>> GetUserAppList()
        {
            return Context.UserApp;
        }

        public async Task<List<GetAllUserAppDto>> GetUserAppBySearchParams(string name, int? userId, int? roleId, int? statusId, int? locationId, int? PracticeId)
        {
            List<GetAllUserAppDto> query = new List<GetAllUserAppDto>();

            var sql = $@"drop table if exists #tempUserApp
                    select u.UserId, u.FirstName + ' ' + u.LastName as FirstName, u.RoleId, u.StatusId, u.PracticeId, 
                    s.StatusName, r.RoleName,l.LocationId, l.LocationName
                    into #tempUserApp
					from UserApp u
                    left outer join Statuses s on u.StatusId = s.StatusId
                    inner join Roles r on u.RoleId = r.RoleId
                    left outer join UserToLocationAssign ul on u.UserId = ul.UserId
                    left outer join Locations l on ul.LocationId = l.LocationId  
                    where ISNULL(u.IsDeleted,0)=0 and u.PracticeId =" + PracticeId;

            sql += " select * from #tempUserApp t ";

            if (!string.IsNullOrEmpty(name))
            {
                sql += " where t.FirstName like '%" + name + "%'";
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetAllUserAppDto>(sql).ToList();
            }

            if (userId > 0)
            {
                query = query.Where(s => s.UserId == userId).ToList();
            }

            if (roleId > 0)
            {
                query = query.Where(s => s.RoleId == roleId).ToList();
            }

            if (statusId > 0)
            {
                query = query.Where(s => s.StatusId == statusId).ToList();
            }

            if (locationId > 0)
            {
                query = query.Where(s => s.LocationId == locationId).ToList();
            }

            return query;
        }

        public async Task<UserAppPermissionDto> GetUserAppPermission(int userId)
        {
            UserAppPermissionDto UserToPerObj = new UserAppPermissionDto();
            List<UserToPermissionDto> list = new List<UserToPermissionDto>();

            try
            {
                string sql = $@"SELECT u.UserId, u.FirstName, u.LastName, u.PersonalEmail, u.IsProvider, up.UserToPermissionId, pr.ProviderId, u.PracticeId,
                    up.CanView, up.CanAdd, up.CanSearch, up.CanEdit, up.CanDelete,
                    r.RoleId, r.RoleName,p.PageId,p.PageName, sp.SubPageId, sp.SubpageName                    
                    from UserApp u
                    inner join UserToPermission up on u.UserId = up.UserId
                    inner join Roles r on u.RoleId = r.RoleId
                    left outer join RoleToPermissions rp on up.RoleToPermissionsId = rp.Id
                    left outer join TblPage p on p.PageId = rp.PageId
					left outer join TblSubPage sp on p.PageId = sp.PageId and rp.SubPageId = sp.SubPageId
					left join Provider pr on u.UserId = pr.UserId
                    where ISNULL(u.IsDeleted,0)=0 and u.UserId = {userId}";

                using (var connection = Context.Database.GetDbConnection())
                {
                    list = connection.Query<UserToPermissionDto>(sql).ToList();
                    List<RolesDto> roles = new List<RolesDto>();
                    if (list.Count() > 0)
                    {
                        UserToPerObj.UserId = list[0].UserId;
                        UserToPerObj.FirstName = list[0].FirstName;
                        UserToPerObj.LastName = list[0].LastName;
                        UserToPerObj.PersonalEmail = list[0].PersonalEmail;
                        UserToPerObj.RoleId = list[0].RoleId;
                        UserToPerObj.RoleName = list[0].RoleName;
                        UserToPerObj.IsProvider = list[0].IsProvider;
                        UserToPerObj.PracticeId = list[0].PracticeId;
                        UserToPerObj.ProviderId = list[0].ProviderId;

                        List<PermissionDto> permissions = list.ConvertAll(a =>
                        {
                            return new PermissionDto()
                            {
                                PageId = a.PageId,
                                PageName = a.PageName,
                                CanAdd = a.CanAdd,
                                CanEdit = a.CanEdit,
                                CanDelete = a.CanDelete,
                                CanSearch = a.CanSearch,
                                CanView = a.CanView,
                                SubPageId = a.SubPageId,
                                SubpageName = a.SubpageName,
                            };
                        });

                        UserToPerObj.permissions = permissions;
                    }
                }

                return UserToPerObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }

            return null;
        }

        public async Task<UserApp> GetUserQuery(string email)
        {
            var user = Context.UserApp.Where(x => x.PersonalEmail == email && x.IsDeleted != true).FirstOrDefault();
            return user;
        }

        public async Task<UserApp> GetUserByIdQuery(int userId)
        {
            var user = Context.UserApp.Where(x => x.UserId == userId && x.IsDeleted != true).FirstOrDefault();
            return user;
        }
    }
}
