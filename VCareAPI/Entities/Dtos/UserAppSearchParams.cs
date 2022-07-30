using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
   public class UserAppSearchParams
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int LocationId { get; set; }
    }
}
