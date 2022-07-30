using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RoomDto
{
    public class RoomPatientDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomNumber { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public int? ProviderId { get; set; }
    }
}
