using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
   public class ServiceProfileDto
    {
        public int [] Procedures { get; set; }
        public int[] CPT { get; set; }
        public string ProfileName { get; set; }
        public int[] templateId { get; set; }
        public Guid Row_id { get; set; }
    }
}
