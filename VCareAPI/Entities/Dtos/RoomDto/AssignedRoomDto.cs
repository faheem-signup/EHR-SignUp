using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RoomDto
{
    public class AssignedRoomDto
    {
        public int RoomId { get; set; }
        public string ProviderName { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public string RoomNumber { get; set; }
    }
}
