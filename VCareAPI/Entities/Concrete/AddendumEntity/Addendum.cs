using Core.Entities;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ProviderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.AddendumEntity
{
    public class Addendum : IEntity
    {
        public int AddendumId { get; set; }
        public int? ProviderId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? CreatedByDate { get; set; }
        public DateTime? ModifiedByDate { get; set; }
        public DateTime? CreatedByTime { get; set; }
        public DateTime? ModifiedByTime { get; set; }
        public string Comments { get; set; }
        public string ProviderSignature { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider _provider { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient _patient { get; set; }
    }
}
