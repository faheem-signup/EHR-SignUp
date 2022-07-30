using Core.Entities;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ADLEntity
{
    public class ADLLookup : IEntity
    {
        [Key]
        public int ADLLookupId { get; set; }
        public int ADLFunctionId { get; set; }
        public int? ProviderId { get; set; }
        public int? PatientId { get; set; }
        public bool? Independent { get; set; }
        public bool? NeedsHelp { get; set; }
        public bool? Dependent { get; set; }
        public bool? CannotDo { get; set; }

        [ForeignKey("ADLFunctionId")]
        public virtual ADLFunction _ADLFunction { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider _provider { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
    }
}
