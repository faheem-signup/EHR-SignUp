using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientCommunicationDto
{
   public class PatientCommunicationDto
    {
        public int CommunicationId { get; set; }
        public DateTime? CommunicationDate { get; set; }
        public DateTime? CommunicationTime { get; set; }
        public int? CommunicateBy { get; set; }
        public int? CallDetailTypeId { get; set; }
        public string CallDetailDescription { get; set; }
        public int? PatientId { get; set; }
    }
}
