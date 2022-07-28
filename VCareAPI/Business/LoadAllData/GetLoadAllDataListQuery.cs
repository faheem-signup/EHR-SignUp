using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract.IGenericLookupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.AppointmentReasonsEntity;
using Entities.Concrete.AreaTypeEntity;
using Entities.Concrete.CLIATypeEntity;
using Entities.Concrete.ContactTypeEntity;
using Entities.Concrete.CountLookupEntity;
using Entities.Concrete.DispensedUnitLookupEntity;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using Entities.Concrete.DozingScheduleLookupEntity;
using Entities.Concrete.DozingStatusLookupEntity;
using Entities.Concrete.EmailStatusEntity;
using Entities.Concrete.GenderEntity;
using Entities.Concrete.ICDCategoryEntity;
using Entities.Concrete.InsurancePayerTypeEntity;
using Entities.Concrete.LocationWorkConfigStatusEntity;
using Entities.Concrete.LookupsEntity;
using Entities.Concrete.MedicationLookupEntity;
using Entities.Concrete.MessageStatusEntity;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientInsuranceTypeEntity;
using Entities.Concrete.POSEntity;
using Entities.Concrete.PracticeTypeEntity;
using Entities.Concrete.ProcedureGroupEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using Entities.Concrete.ProgramLookupEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderServiceEntity;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Concrete.ReferralProviderTypeEntity;
using Entities.Concrete.ReminderDaysLookupEntity;
using Entities.Concrete.ReminderStatusEntity;
using Entities.Concrete.ReminderTypeEntity;
using Entities.Concrete.RoomEntity;
using Entities.Concrete.SchedulerStatusEntity;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Concrete.SubscriberRelationshiplookupEntity;
using Entities.Concrete.TaxTypeEntity;
using Entities.Concrete.TherapistLookupEntity;
using Entities.Concrete.User;
using Entities.Concrete.UserTypeEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LoadAllData.Queries
{
    public class GetLoadAllDataQuery : IRequest<IDataResult<IEnumerable<object>>>
    {
        public string EntityName { get; set; }
        public class GetLoadAllDataQueryHandler : IRequestHandler<GetLoadAllDataQuery, IDataResult<IEnumerable<object>>>
        {

            private readonly IGenericLookupRepository _genericLookupRepository;
            public GetLoadAllDataQueryHandler(IGenericLookupRepository genericLookupRepository)
            {
                _genericLookupRepository = genericLookupRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<object>>> Handle(GetLoadAllDataQuery request, CancellationToken cancellationToken)
            {
                List<object> list = new List<object>();

                switch (request.EntityName.ToLower())
                {
                    case "status":
                        var locationData = _genericLookupRepository.GetAllData<Status>();
                        return new SuccessDataResult<IEnumerable<object>>(locationData);
                        break;

                    case "practicetype":
                        var practicetypedata = _genericLookupRepository.GetAllData<PracticeType>();
                        return new SuccessDataResult<IEnumerable<object>>(practicetypedata);
                        break;

                    case "usertype":
                        var usertypedata = _genericLookupRepository.GetAllData<UserType>();
                        return new SuccessDataResult<IEnumerable<object>>(usertypedata);
                        break;

                    case "cliatype":
                        var cLIAtypeData = _genericLookupRepository.GetAllData<CLIAType>();
                        return new SuccessDataResult<IEnumerable<object>>(cLIAtypeData);
                        break;

                    case "taxtype":
                        var taxtypeData = _genericLookupRepository.GetAllData<TaxType>();
                        return new SuccessDataResult<IEnumerable<object>>(taxtypeData);
                        break;

                    case "pos":
                        var posData = _genericLookupRepository.GetAllData<POS>();
                        return new SuccessDataResult<IEnumerable<object>>(posData);
                        break;

                    case "contacttype":
                        var contacttypeData = _genericLookupRepository.GetAllData<ContactType>();
                        return new SuccessDataResult<IEnumerable<object>>(contacttypeData);
                        break;

                    case "remindertype":
                        var remindertypeData = _genericLookupRepository.GetAllData<ReminderType>();
                        return new SuccessDataResult<IEnumerable<object>>(remindertypeData);
                        break;

                    case "insurancepayertype":
                        var insurancepayertypeData = _genericLookupRepository.GetAllData<InsurancePayerType>();
                        return new SuccessDataResult<IEnumerable<object>>(insurancepayertypeData);
                        break;

                    case "icdcategory":
                        var icdcategoryData = _genericLookupRepository.GetAllData<ICDCategory>();
                        return new SuccessDataResult<IEnumerable<object>>(icdcategoryData);
                        break;

                    case "referralprovidertype":
                        var referralprovidertypeData = _genericLookupRepository.GetAllData<ReferralProviderType>();
                        return new SuccessDataResult<IEnumerable<object>>(referralprovidertypeData);
                        break;

                    case "referralprovider":
                        var referralproviderData = _genericLookupRepository.GetAllData<ReferralProvider>();
                        return new SuccessDataResult<IEnumerable<object>>(referralproviderData);
                        break;

                    case "reminderdayslookup":
                        var reminderdayslookupData = _genericLookupRepository.GetAllData<ReminderDaysLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(reminderdayslookupData);
                        break;

                    case "proceduregroup":
                        var proceduregroupData = _genericLookupRepository.GetAllData<ProcedureGroup>();
                        return new SuccessDataResult<IEnumerable<object>>(proceduregroupData);
                        break;

                    case "proceduresubgroup":
                        var proceduresubgroupData = _genericLookupRepository.GetAllData<ProcedureSubGroup>();
                        return new SuccessDataResult<IEnumerable<object>>(proceduresubgroupData);
                        break;

                    case "arealookup":
                        var aealookupData = _genericLookupRepository.GetAllData<AreaLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(aealookupData);
                        break;

                    case "providerservice":
                        var providerserviceData = _genericLookupRepository.GetAllData<ProviderService>();
                        return new SuccessDataResult<IEnumerable<object>>(providerserviceData);
                        break;

                    case "practiceassignmentlookup":
                        var practiceassignmentlookupData = _genericLookupRepository.GetAllData<PracticeAssignmentLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(practiceassignmentlookupData);
                        break;

                    case "locationworkconfigstatus":
                        var locationworkconfigstatusData = _genericLookupRepository.GetAllData<LocationWorkConfigStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(locationworkconfigstatusData);
                        break;

                    case "patientdisabilitystatuslookup":
                        var patientdisabilitystatuslookupData = _genericLookupRepository.GetAllData<PatientDisabilityStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(patientdisabilitystatuslookupData);
                        break;

                    case "packslookup":
                        var packslookupData = _genericLookupRepository.GetAllData<PacksLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(packslookupData);
                        break;

                    case "hospitalizationstatuslookup":
                        var hospitalizationstatuslookupData = _genericLookupRepository.GetAllData<HospitalizationStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(hospitalizationstatuslookupData);
                        break;

                    case "smokingstatuslookup":
                        var smokingstatuslookupData = _genericLookupRepository.GetAllData<SmokingStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(smokingstatuslookupData);
                        break;

                    case "accidenttypelookup":
                        var accidenttypelookupData = _genericLookupRepository.GetAllData<AccidentTypeLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(accidenttypelookupData);
                        break;

                    case "workstatuslookup":
                        var workstatuslookupData = _genericLookupRepository.GetAllData<WorkStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(workstatuslookupData);
                        break;

                    case "employmentstatuslookup":
                        var employmentstatuslookupData = _genericLookupRepository.GetAllData<EmploymentStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(employmentstatuslookupData);
                        break;

                    case "illicitsubstanceslookup":
                        var illicitsubstanceslookupData = _genericLookupRepository.GetAllData<IllicitSubstancesLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(illicitsubstanceslookupData);
                        break;

                    case "alcohollookup":
                        var alcohollookupData = _genericLookupRepository.GetAllData<AlcoholLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(alcohollookupData);
                        break;

                    case "substanceabusestatuslookup":
                        var substanceabusestatuslookupData = _genericLookupRepository.GetAllData<SubstanceAbuseStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(substanceabusestatuslookupData);
                        break;

                    case "preferredlanguagelookup":
                        var preferredlanguagelookupData = _genericLookupRepository.GetAllData<PreferredLanguageLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(preferredlanguagelookupData);
                        break;

                    case "preferredcommslookup":
                        var preferredcommslookupData = _genericLookupRepository.GetAllData<PreferredCommsLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(preferredcommslookupData);
                        break;

                    case "maritalstatuslookup":
                        var maritalstatuslookupData = _genericLookupRepository.GetAllData<MaritalStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(maritalstatuslookupData);
                        break;

                    case "ethnicitylookup":
                        var ethnicitylookupData = _genericLookupRepository.GetAllData<EthnicityLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(ethnicitylookupData);
                        break;

                    case "racelookup":
                        var racelookupData = _genericLookupRepository.GetAllData<RaceLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(racelookupData);
                        break;

                    case "gender":
                        var genderData = _genericLookupRepository.GetAllData<Gender>();
                        return new SuccessDataResult<IEnumerable<object>>(genderData);
                        break;

                    case "subscriberrelationshiplookup":
                        var subscriberRelationshiplookupData = _genericLookupRepository.GetAllData<SubscriberRelationshiplookup>();
                        return new SuccessDataResult<IEnumerable<object>>(subscriberRelationshiplookupData);
                        break;

                    case "patientinsurancetype":
                        var patientInsuranceType = _genericLookupRepository.GetAllData<PatientInsuranceType>();
                        return new SuccessDataResult<IEnumerable<object>>(patientInsuranceType);
                        break;

                    case "countlookupentity":
                        var countlookupentity = _genericLookupRepository.GetAllData<CountLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(countlookupentity);
                        break;

                    case "userapp":
                        var userappData = _genericLookupRepository.GetAllData<UserApp>();
                        userappData = userappData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(userappData);
                        break;

                    case "documentparentcategorylookup":
                        var documentParentCategoryLookup = _genericLookupRepository.GetAllData<DocumentParentCategoryLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(documentParentCategoryLookup);
                        break;

                    case "provider":
                        var providerData = _genericLookupRepository.GetAllData<Provider>();
                        providerData = providerData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(providerData);
                        break;

                    case "patient":
                        var patientData = _genericLookupRepository.GetAllData<Patient>();
                        patientData = patientData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(patientData);
                        break;

                    case "serviceprofile":
                        var serviceprofileData = _genericLookupRepository.GetAllData<ServiceProfile>();
                        return new SuccessDataResult<IEnumerable<object>>(serviceprofileData);
                        break;

                    case "room":
                        var roomData = _genericLookupRepository.GetAllData<Room>();
                        roomData = roomData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(roomData);
                        break;

                    case "appointmentreasons":
                        var appointmentreasonsData = _genericLookupRepository.GetAllData<AppointmentReasons>();
                        appointmentreasonsData = appointmentreasonsData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(appointmentreasonsData);
                        break;

                    case "appointmentstatus":
                        var appointmentstatusData = _genericLookupRepository.GetAllData<AppointmentStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(appointmentstatusData);
                        break;

                    case "appointmenttype":
                        var appointmenttypeData = _genericLookupRepository.GetAllData<AppointmentType>();
                        return new SuccessDataResult<IEnumerable<object>>(appointmenttypeData);
                        break;

                    case "locations":
                        var locationsData = _genericLookupRepository.GetAllData<Entities.Concrete.Location.Locations>();
                        return new SuccessDataResult<IEnumerable<object>>(locationsData);
                        break;

                    case "weektypelookup":
                        var weektypelookupData = _genericLookupRepository.GetAllData<WeekTypeLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(weektypelookupData);
                        break;

                    case "appointmentautoreminder":
                        var appointmentautoreminderData = _genericLookupRepository.GetAllData<AppointmentAutoReminder>();
                        return new SuccessDataResult<IEnumerable<object>>(appointmentautoreminderData);
                        break;

                    case "diagnosiscode":
                        var diagnosiscodeData = _genericLookupRepository.GetAllData<DiagnosisCode>();
                        diagnosiscodeData = diagnosiscodeData.Where(x => x.IsDeleted != true);
                        return new SuccessDataResult<IEnumerable<object>>(diagnosiscodeData);
                        break;

                    case "schedulerstatus":
                        var schedulerstatusData = _genericLookupRepository.GetAllData<SchedulerStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(schedulerstatusData);
                        break;
                    case "programlookup":
                        var programlookupData = _genericLookupRepository.GetAllData<ProgramLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(programlookupData);
                        break;

                    case "dispensedunitlookup":
                        var dispensedunitlookupData = _genericLookupRepository.GetAllData<DispensedUnitLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(dispensedunitlookupData);
                        break;

                    case "therapistlookup":
                        var therapistlookupData = _genericLookupRepository.GetAllData<TherapistLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(therapistlookupData);
                        break;

                    case "dozingstatuslookup":
                        var dozingstatuslookupData = _genericLookupRepository.GetAllData<DozingStatusLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(dozingstatuslookupData);
                        break;

                    case "medicationlookup":
                        var medicationlookupData = _genericLookupRepository.GetAllData<MedicationLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(medicationlookupData);
                        break;

                    case "reminderstatus":
                        var reminderstatusData = _genericLookupRepository.GetAllData<ReminderStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(reminderstatusData);
                        break;

                    case "emailstatus":
                        var emailstatusData = _genericLookupRepository.GetAllData<EmailStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(emailstatusData);
                        break;

                    case "messagestatus":
                        var messagestatusData = _genericLookupRepository.GetAllData<MessageStatus>();
                        return new SuccessDataResult<IEnumerable<object>>(messagestatusData);
                        break;

                    case "dozingdchedulelookup":
                        var dozingdchedulelookupData = _genericLookupRepository.GetAllData<DozingScheduleLookup>();
                        return new SuccessDataResult<IEnumerable<object>>(dozingdchedulelookupData);
                        break;

                    default:
                        return new ErrorDataResult<IEnumerable<object>>(list);
                        break;
                }

            }

        }
    }
}
