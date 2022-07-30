using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.AppointmentEntity
{
    public class AppointmentStatus : IEntity
    {
        [Key]
        public int AppointmentStatusId { get; set; }
        public string AppointmentStatusName { get; set; }
    }
}
