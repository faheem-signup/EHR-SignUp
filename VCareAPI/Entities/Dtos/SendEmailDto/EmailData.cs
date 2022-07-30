using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.SendEmailDto
{
    public class EmailData
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailToPassword { get; set; }
    }
}
