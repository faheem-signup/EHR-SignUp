using Core.Entities;
using Entities.Concrete.CommunicationCallDetailTypeEntity;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientCommunicationEntity
{
    public class PatientCommunication : IEntity
    {
        [Key]
        public int CommunicationId { get; set; }
        public DateTime? CommunicationDate { get; set; }
        public DateTime? CommunicationTime { get; set; }
        public int? CommunicateBy { get; set; }
        public int? CallDetailTypeId { get; set; }
        public string CallDetailDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("CallDetailTypeId")]
        public virtual CommunicationCallDetailType _communicationCallDetailType { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
        [ForeignKey("CommunicateBy")]
        public virtual UserApp _userApp { get; set; }
    }
}
