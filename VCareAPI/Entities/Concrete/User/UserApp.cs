using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Concrete.Role;
using Entities.Concrete.UserTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.User
{
   public class UserApp : IEntity
    {
        [Key]
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
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("City")]
        public virtual CityStateLookup Cities { get; set; }

        [ForeignKey("State")]
        public virtual CityStateLookup states { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status status { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles role { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType userType { get; set; }
        [ForeignKey("PracticeId")]
        public virtual Practice practice { get; set; }
    }
}
