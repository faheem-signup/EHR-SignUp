using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientCommunicationDto
{
    public class PatientCommunicationListDto
    {
        public int CommunicationId { get; set; }
        public DateTime? CommunicationDate { get; set; }
        public DateTime? CommunicationTime { get; set; }
        public string CallDetailDescription { get; set; }
        public string CommunicateByName { get; set; }
        public string CallDetail { get; set; }
        public int? PatientId { get; set; }
    }
}
