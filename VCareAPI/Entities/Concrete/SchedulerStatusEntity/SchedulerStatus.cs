using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.SchedulerStatusEntity
{
    public class SchedulerStatus : IEntity
    {
        [Key]
        public int SchedulerStatusId { get; set; }
        public string SchedulerStatusName { get; set; }
    }
}
