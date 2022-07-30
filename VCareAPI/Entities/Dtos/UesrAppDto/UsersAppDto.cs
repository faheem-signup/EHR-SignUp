using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.UserToProviderAssignEnity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
  public class UsersAppDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MI { get; set; }
        public string UserSSN { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string PersonalEmail { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? IsProvider { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public DateTime? DOB { get; set; }
        public int? UserTypeId { get; set; }
        public int? AutoLockTime { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
        public int? PracticeId { get; set; }
        public IEnumerable<UserToLocationAssignDto> UserToLocationAssignList { get; set; }
        public IEnumerable<UserToProviderAssignDto> UserToProvderAssignList { get; set; }
        public IEnumerable<UserWorkHourDto> UserWorkHourList { get; set; }
    }
}
