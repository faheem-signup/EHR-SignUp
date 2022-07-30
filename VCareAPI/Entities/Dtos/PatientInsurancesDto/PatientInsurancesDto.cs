using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientInsurancesDto
{
   public class PatientInsurancesDto
    {
        public int PatientInsuranceId { get; set; }
        public int? InsuranceType { get; set; }
        public string InsuranceTypeName { get; set; }

        public string InsuranceName { get; set; }
        public string InsuranceAddress { get; set; }
        public string InsurancePhone { get; set; }
        public string SubscriberAddress { get; set; }
        public string SubscriberPhone { get; set; }
        public string Copay { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientId { get; set; }
        public string GroupNumber { get; set; }
        public string InsuranceImagePath { get; set; }
        public int? InsuredPay { get; set; }
        public DateTime? EligibilityDate { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string SubscriberName { get; set; }
        public int? SubscriberRelationship { get; set; }
        public int? SubscriberCity { get; set; }
        public int? SubscriberState { get; set; }
        public int? SubscriberZip { get; set; }
        public string RxPayerId { get; set; }
        public string RxGroupNo { get; set; }
        public string RxBinNo { get; set; }
        public string RxPCN { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public byte[] InsuranceImage { get; set; }

    }
}
