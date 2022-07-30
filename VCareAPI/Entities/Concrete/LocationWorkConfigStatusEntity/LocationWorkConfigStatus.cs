using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.LocationWorkConfigStatusEntity
{
    public class LocationWorkConfigStatus : IEntity
    {
        public int LocationWorkConfigStatusId { get; set; }
        public string LocationWorkConfigStatusName { get; set; }

    }
}
