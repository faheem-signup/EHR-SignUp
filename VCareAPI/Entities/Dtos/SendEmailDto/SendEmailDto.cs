using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.SendEmailDto
{
    public class SendEmailDto
    {
        public int EmailId { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailContent { get; set; }
        public int EmailStatusId { get; set; }
        public int MessageStatusId { get; set; }
        public string Attachments { get; set; }
        public int UserId { get; set; }
    }
}
