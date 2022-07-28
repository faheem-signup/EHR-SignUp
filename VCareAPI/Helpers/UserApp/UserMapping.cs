using AutoMapper;
using Entities.Concrete;
using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.UserToPermissions;
using Entities.Concrete.UserToProviderAssignEnity;
using Entities.Dtos.UesrAppDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.UserApp
{
    public class UserMapping:Profile

    {

        public UserMapping() {
            CreateMap<UserToPermission, UserAppPermissionDto>()
                        .ForMember(x => x.UserId, opt => opt.MapFrom(y => y.UserId));




            CreateMap<Entities.Concrete.User.UserApp, UsersAppDto>();
            CreateMap<UserToProviderAssign, UserToProviderAssignDto>();
            CreateMap<UserToLocationAssign, UserToLocationAssignDto>();
            CreateMap<UserWorkHour, UserWorkHourDto>();



            //CreateMap<Azmoon, AzmoonViewModel>()
            //.ForMember(d => d.CreatorUserName, m => m.MapFrom(s =>s.CreatedBy.UserName))
            //.ForMember(d => d.LastModifierUserName, m => m.MapFrom(s =>s.ModifiedBy.UserName));

            //UserId = u.UserId,
            //FirstName = u.FirstName,
            //LastName = u.LastName,
            //PersonalEmail = u.PersonalEmail,
            //UserType = u.UserType,
            //StatusId = u.StatusId,
            //IsFirstLogin = u.IsFirstLogin,
            //RoleId = u.RoleId,
            //RoleName = r.RoleName,
            //PermissionId = p.PermissionId,
            //PermissionDescription = p.PermissionDescription

        }

    }
}
