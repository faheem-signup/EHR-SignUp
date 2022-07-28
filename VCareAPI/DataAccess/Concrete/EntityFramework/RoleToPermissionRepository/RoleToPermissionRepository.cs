using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IRoleToPermissionRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Concrete.UserToPermissions;
using Entities.Dtos.UserPermissionsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.RoleToPermissionRepository
{
    public class RoleToPermissionRepository : EfEntityRepositoryBase<RoleToPermission, ProjectDbContext>, IRoleToPermissionRepository
    {
        public RoleToPermissionRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoleToPermission>> GetRoleToPermissionListByRoleId(int roleId)
        {
            var list = await Context.RoleToPermissions.Include(x=>x.tblPage)
                .Where(x => x.RoleId == roleId)
                .OrderByDescending(x => x.RoleId)
                .ToListAsync();
            return list;
        }

        public async Task BulkInsert(IEnumerable<RoleToPermission> existingList, IEnumerable<RoleToPermission> roleToPermissionList)
        {
            try
            {
                if (existingList.Count() > 0)
                {
                    Context.RoleToPermissions.RemoveRange(existingList);
                }

                await Context.RoleToPermissions.AddRangeAsync(roleToPermissionList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<GetUserToPermissionDto>> GetUserToPermissionList(int roleId, int userId)
        {
            List<GetUserToPermissionDto> query = new List<GetUserToPermissionDto>();

            var sql = $@"select distinct up.RoleToPermissionsId, up.UserId, up.CanAdd, up.CanDelete, up.CanEdit, 
                up.CanSearch, up.CanView, p.PageName, rp.PageId, rp.RoleId 
                from  UserToPermission up 
                inner join RoleToPermissions rp on up.RoleToPermissionsId = rp.Id
                inner join TblPage p on rp.PageId = p.PageId
                where rp.RoleId = " + roleId + " and up.UserId = " + userId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<GetUserToPermissionDto>(sql).ToList();
            }

            return query;
        }


        public async Task DeleteUserToPermission(IEnumerable<UserToPermission> existingList)
        {
            try
            {
                if (existingList.Count() > 0)
                {
                    Context.UserToPermission.RemoveRange(existingList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
