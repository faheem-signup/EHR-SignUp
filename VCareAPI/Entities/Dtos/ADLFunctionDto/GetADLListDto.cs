using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ADLFunctionDto
{
    public class GetADLListDto
    {
        public int ADLCategoryId { get; set; }
        public string ADLCategoryName { get; set; }
        public List<ADLFunctionDto> _ADLFunctionList { get; set; }
    }
}
