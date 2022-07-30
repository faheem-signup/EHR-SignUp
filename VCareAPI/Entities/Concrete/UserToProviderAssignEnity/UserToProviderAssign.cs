using Core.Entities;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.UserToProviderAssignEnity
{
  public class UserToProviderAssign : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        [ForeignKey("UserId")]
        public virtual UserApp UserApp { get; set; }
    }
}
