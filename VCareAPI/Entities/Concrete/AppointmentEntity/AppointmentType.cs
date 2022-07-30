using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.AppointmentEntity
{
    public class AppointmentType : IEntity
    {
        [Key]
        public int AppointmentTypeId { get; set; }
        public string AppointmentTypeName { get; set; }
    }
}
