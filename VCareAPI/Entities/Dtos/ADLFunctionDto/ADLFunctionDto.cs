using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ADLFunctionDto
{
    public class ADLFunctionDto
    {
        public int ADLLookupId { get; set; }
        public int? ADLFunctionId { get; set; }
        public int? ProviderId { get; set; }
        public int? PatientId { get; set; }
        public int? ADLCategoryId { get; set; }
        public bool? Independent { get; set; }
        public bool? NeedsHelp { get; set; }
        public bool? Dependent { get; set; }
        public bool? CannotDo { get; set; }
        public string ADLFunctionName { get; set; }
        public string ADLCategoryName { get; set; }
    }
}
