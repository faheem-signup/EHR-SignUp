using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RoomDto
{
    public class AvialableRoomDto
    {
        public int RoomId { get; set; }
        public string CreatedDate { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string RoomNumber { get; set; }
    }
}
