using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.ProviderClinicalInfoEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProviderSecurityCheckInfoEntity
{
    public class ProviderSecurityCheckInfo : IEntity
    {
        [Key]
        public int SecurityCheckId { get; set; }
        public string SecurityCheck { get; set; }
        public int? FBIState { get; set; }
        public string StatePoliceClearance { get; set; }
        public int? ProviderClinicalInfoId { get; set; }
        [ForeignKey("ProviderClinicalInfoId")]
        public virtual ProviderClinicalInfo providerClinicalInfo { get; set; }
        [ForeignKey("FBIState")]
        public virtual CityStateLookup States { get; set; }

    }
}
 