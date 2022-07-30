using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class GetPracticePayersDto
    {
        public int PracticePayerId { get; set; }
        public string PayerName { get; set; }
        public int? PayerId { get; set; }
        public string TypeQualifier { get; set; }
        public string Location { get; set; }
        public int? PracticeId { get; set; }
    }
}
