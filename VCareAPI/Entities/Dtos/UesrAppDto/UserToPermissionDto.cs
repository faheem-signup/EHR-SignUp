using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
    public class UserToPermissionDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalEmail { get; set; }
        public int? UserToPermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanDelete { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsProvider { get; set; }
        public int? ProviderId { get; set; }
        public int? PracticeId { get; set; }
        public int? PageId { get; set; }
        public string PageName { get; set; }
        public int? SubPageId { get; set; }
        public string SubpageName { get; set; }
    }
}
