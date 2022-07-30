using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.InsurancePayerTypeEntity
{
    public class InsurancePayerType : IEntity
    {
        [Key]
        public int InsurancePayerTypeId { get; set; }
        public string PayerTypeDescription { get; set; }
    }
}
