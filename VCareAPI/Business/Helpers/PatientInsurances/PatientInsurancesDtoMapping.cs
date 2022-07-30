using AutoMapper;
using Entities.Concrete.Location;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Dtos.LocationDto;
using Entities.Dtos.PatientCommunicationDto;
using Entities.Dtos.PatientInsurancesDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.PatientInsurances
{
    public class PatientInsurancesDtoMapping : Profile
    {

        public PatientInsurancesDtoMapping()
        {
            CreateMap<PatientInsurance, PatientInsurancesDto>()
                .ForMember(x => x.PatientInsuranceId, opt => opt.MapFrom(x => x.PatientInsuranceId))
                .ForMember(x => x.InsuranceType, opt => opt.MapFrom(x => x.InsuranceType))
                .ForMember(x => x.InsuranceTypeName, opt => opt.MapFrom(x => x.patientInsuranceType.Description))
                .ForMember(x => x.InsuranceName, opt => opt.MapFrom(x => x.InsuranceName))
                .ForMember(x => x.InsuranceAddress, opt => opt.MapFrom(x => x.InsuranceAddress))
                .ForMember(x => x.InsurancePhone, opt => opt.MapFrom(x => x.InsurancePhone))
                .ForMember(x => x.SubscriberAddress, opt => opt.MapFrom(x => x.SubscriberAddress))
                .ForMember(x => x.SubscriberPhone, opt => opt.MapFrom(x => x.SubscriberPhone))
                .ForMember(x => x.Copay, opt => opt.MapFrom(x => x.Copay))
                .ForMember(x => x.PolicyNumber, opt => opt.MapFrom(x => x.PolicyNumber))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
                .ForMember(x => x.PatientId, opt => opt.MapFrom(x => x.PatientId))
                .ForMember(x => x.GroupNumber, opt => opt.MapFrom(x => x.GroupNumber))
                .ForMember(x => x.InsuredPay, opt => opt.MapFrom(x => x.InsuredPay))
                .ForMember(x => x.EligibilityDate, opt => opt.MapFrom(x => x.EligibilityDate))
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.City))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.State))
                .ForMember(x => x.ZIP, opt => opt.MapFrom(x => x.ZIP))
                .ForMember(x => x.SubscriberName, opt => opt.MapFrom(x => x.SubscriberName))
                .ForMember(x => x.SubscriberRelationship, opt => opt.MapFrom(x => x.SubscriberRelationship))
                .ForMember(x => x.SubscriberCity, opt => opt.MapFrom(x => x.SubscriberCity))
                .ForMember(x => x.SubscriberState, opt => opt.MapFrom(x => x.SubscriberState))
                .ForMember(x => x.SubscriberZip, opt => opt.MapFrom(x => x.SubscriberZip))
                .ForMember(x => x.RxPayerId, opt => opt.MapFrom(x => x.RxPayerId))
                .ForMember(x => x.RxGroupNo, opt => opt.MapFrom(x => x.RxGroupNo))
                .ForMember(x => x.RxBinNo, opt => opt.MapFrom(x => x.RxBinNo))
                .ForMember(x => x.RxPCN, opt => opt.MapFrom(x => x.RxPCN));


            CreateMap<PatientInsuranceAuthorization, PatientInsuranceAuthorizationDto>()
                .ForMember(x => x.PatientInsuranceAuthorizationId, opt => opt.MapFrom(x => x.PatientInsuranceAuthorizationId))
                .ForMember(x => x.PatientInsuranceId, opt => opt.MapFrom(x => x.PatientInsuranceId))
                .ForMember(x => x.AuthorizationNo, opt => opt.MapFrom(x => x.AuthorizationNo))
                .ForMember(x => x.Count, opt => opt.MapFrom(x => x.Count))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
                .ForMember(x => x.WarningDate, opt => opt.MapFrom(x => x.WarningDate))
                .ForMember(x => x.InsuranceName, opt => opt.MapFrom(x => x.patientInsurance.InsuranceName))
                .ForMember(x => x.AuthorizationUnitsVsits, opt => opt.MapFrom(x => x.AuthorizationUnitsVsits))
                .ForMember(x => x.Comment, opt => opt.MapFrom(x => x.Comment));

            CreateMap<PatientInsuranceAuthorizationCPT, PatientInsuranceAuthorizationCPTDto>()
                //.ForMember(x => x.PatientInsuranceAuthorizationId, opt => opt.MapFrom(x => x.PatientInsuranceAuthorizationId))
                //.ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.DiagnosisId, opt => opt.MapFrom(x => x.DiagnosisId))
                .ForMember(x => x.ProcedureId, opt => opt.MapFrom(x => x.ProcedureId));

        }
    }
}
