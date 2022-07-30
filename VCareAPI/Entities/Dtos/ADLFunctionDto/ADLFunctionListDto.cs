using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ADLFunctionDto
{
    public class ADLFunctionListDto
    {
        public int ADLFunctionId { get; set; }
        public int? ADLCategoryId { get; set; }
        public string ADLFunctionName { get; set; }
    }
}
