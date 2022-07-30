using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.RoomEntity
{
    public class Room : IEntity
    {
        [Key]
        public int RoomId { get; set; }
        public int? AreaId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }


        [ForeignKey("AreaId")]
        public virtual AreaTypeEntity.AreaLookup AreaLookup { get; set; }
    }
}
