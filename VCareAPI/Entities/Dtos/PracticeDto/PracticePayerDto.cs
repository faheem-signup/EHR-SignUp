using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PracticeDto
{
    public class PracticePayerDto
    {
        public Practice practice { get; set; }
        public List<GetPracticePayersDto> practicePayers { get; set; }
    }
}
