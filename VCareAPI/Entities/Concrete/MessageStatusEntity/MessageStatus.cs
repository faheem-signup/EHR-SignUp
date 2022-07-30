using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.MessageStatusEntity
{
    public class MessageStatus : IEntity
    {
        [Key]
        public int MessageStatusId { get; set; }
        public string MessageStatusName { get; set; }
    }
}
