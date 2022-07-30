using Core.Entities;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientInsuranceTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.PatientInsuranceEntity
{
   public class PatientInsurance :IEntity
    {
        [Key]
        public int PatientInsuranceId { get; set; }
        public int? InsuranceType { get; set; }
        public int? InsuredPay { get; set; }
        public string InsuranceName { get; set; }
        public DateTime? EligibilityDate { get; set; }
        public string InsuranceAddress { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string InsurancePhone { get; set; }
        public string Copay { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriberPhone { get; set; }
        public int? SubscriberRelationship { get; set; }
        public int? SubscriberCity { get; set; }
        public int? SubscriberState { get; set; }
        public string SubscriberAddress { get; set; }
        public int? SubscriberZip { get; set; }
        public string RxPayerId { get; set; }
        public string RxGroupNo { get; set; }
        public string RxBinNo { get; set; }
        public string RxPCN { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient patient { get; set; }
        [ForeignKey("InsuranceType")]
        public virtual PatientInsuranceType patientInsuranceType { get; set; }
        public byte[] InsuranceImage { get; set; }
        public string InsuranceImagePath { get; set; }
    }
}
