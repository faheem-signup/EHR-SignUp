using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.CommunicationCallDetailTypeEntity
{
  public class CommunicationCallDetailType :IEntity
    {
        [Key]
        public int CallDetailTypeId { get; set; }
        public string CallDetail { get; set; }
    }
}
