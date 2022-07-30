using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.SubscriberRelationshiplookupEntity
{
  public  class SubscriberRelationshiplookup :IEntity
    {
        [Key]
        public int SubscriberRelationshiplookupId { get; set; }
        public string Description { get; set; }
    }
}
