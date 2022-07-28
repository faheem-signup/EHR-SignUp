using AutoMapper;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Dtos.RoleToPermissionsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.RolesToPermissions
{
   public class RoleToPermissionDtoMapping : Profile
    {
        public RoleToPermissionDtoMapping()
        {
            CreateMap<RoleToPermission, AllRoleToPermissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.RoleId, opt => opt.MapFrom(x => x.RoleId))
                .ForMember(x => x.PageId, opt => opt.MapFrom(x => x.PageId))
                .ForMember(x => x.SubPageId, opt => opt.MapFrom(x => x.SubPageId))
                //.ForMember(x => x.CanAdd, opt => opt.MapFrom(x => x.CanAdd))
                //.ForMember(x => x.CanEdit, opt => opt.MapFrom(x => x.CanEdit))
                //.ForMember(x => x.CanView, opt => opt.MapFrom(x => x.CanView))
                //.ForMember(x => x.CanDelete, opt => opt.MapFrom(x => x.CanDelete))
                //.ForMember(x => x.CanSearch, opt => opt.MapFrom(x => x.CanSearch))
                .ForMember(x => x.PageName, opt => opt.MapFrom(x => x.tblPage.PageName));
        }
    }
}
