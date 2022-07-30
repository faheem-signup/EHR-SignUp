using Core.Entities;
using Entities.Concrete.Location;
using Entities.Concrete.LocationWorkConfigStatusEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.LocationWorkConfigsEntity
{
    public class LocationWorkConfigs : IEntity
    {
        [Key]
        public int WorkConfigId { get; set; }
        public string Day { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime EndTo { get; set; }
        public int? LocationId { get; set; }
        public int? LocationWorkConfigStatusId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Locations Locations { get; set; }

        [ForeignKey("LocationWorkConfigStatusId")]
        public virtual LocationWorkConfigStatus locationWorkConfigStatus { get; set; }
    }
}
