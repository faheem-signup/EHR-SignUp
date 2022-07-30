using Core.Entities;
using Entities.Concrete.EmailStatusEntity;
using Entities.Concrete.MessageStatusEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.SendEmailEntity
{
    public class SendEmail : IEntity
    {
        [Key]
        public int EmailId { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int EmailStatusId { get; set; }
        public string Attachments { get; set; }
        public int UserId { get; set; }
        public int MessageStatusId { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EmailStatusId")]
        public virtual EmailStatus _emailStatus { get; set; }
        [ForeignKey("MessageStatusId")]
        public virtual MessageStatus _messageStatuses { get; set; }
    }
}
