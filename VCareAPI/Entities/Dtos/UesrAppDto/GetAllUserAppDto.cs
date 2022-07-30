using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
    public class GetAllUserAppDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
        public int? PracticeId { get; set; }
        public string StatusName { get; set; }
        public string RoleName { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
