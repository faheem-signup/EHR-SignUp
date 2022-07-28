using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Concrete.PracticeDocsEntity;
using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Concrete.Location;
using Entities.Concrete.ProceduresEntity;
using Entities.Concrete.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Entities.Concrete.Role;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientInfoDetailsEntity;
using Entities.Concrete.PatientGuardiansEntity;
using Entities.Concrete.PatientEmploymentEntity;
using Entities.Concrete.PatientProviderEntity;
using Entities.Concrete.Permission;
using Entities.Concrete.UserToPermissions;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Entities.Concrete.ReminderProfileEntity;
using Entities.Concrete.RoomEntity;
using Entities.Concrete.RecurringAppointmentsEntity;
using Entities.Concrete.FollowUpAppointmentEntity;
using Entities.Concrete.TemplateCategoryEntity;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using Entities.Concrete.PatientDocumentCategoryEntity;
using Entities.Concrete.PatientDocsFileUploadEntity;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Concrete.InsuranceEntity;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using Entities.Concrete.InsurancePayerTypeEntity;
using Entities.Concrete.ClinicalTemplateDataEntity;
using Entities.Concrete.LocationWorkConfigsEntity;
using Entities.Concrete.GenderEntity;
using Entities.Concrete.AppointmentReasonsEntity;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.PatientVitalsEntity;
using Entities.Concrete.SchedulerStatusEntity;
using Entities.Concrete.SchedulerEntity;
using Entities.Concrete.PatientDiagnosisCodeEntity;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Concrete.CommunicationCallDetailTypeEntity;
using Entities.Concrete.UserToProviderAssignEnity;
using Entities.Concrete.UserToLocationAssignEntity;
using Entities.Concrete.TBLPageEntity;
using Entities.Concrete.PracticeTypeEntity;
using Entities.Concrete.UserTypeEntity;
using Entities.Concrete.CLIATypeEntity;
using Entities.Concrete.TaxTypeEntity;
using Entities.Concrete.POSEntity;
using Entities.Concrete.ContactEntity;
using Entities.Concrete.ContactTypeEntity;
using Entities.Concrete.ReminderTypeEntity;
using Entities.Concrete.ICDCategoryEntity;
using Entities.Concrete.ICDToPracticesEntity;
using Entities.Concrete.ReferralProviderTypeEntity;
using Entities.Concrete.ReminderDaysLookupEntity;
using Entities.Concrete.AreaTypeEntity;
using Entities.Concrete.ProcedureGroupEntity;
using Entities.Concrete.ProcedureSubGroupEntity;
using Entities.Concrete.ProcedureGroupToPracticesEntity;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.ProviderClinicalInfoEntity;
using Entities.Concrete.LocationWorkConfigStatusEntity;
using Entities.Concrete.PatientInsuranceTypeEntity;
using Entities.Concrete.LookupsEntity;
using Entities.Concrete.SubscriberRelationshiplookupEntity;
using Entities.Concrete.CountLookupEntity;
using Entities.Concrete.PatientEducationEntity;
using Entities.Concrete.AppointmentsCheckInEntity;
using Entities.Concrete.PatientDispensingEntity;
using Entities.Concrete.PatientDispensingDosingEntity;
using Entities.Concrete.ProgramLookupEntity;
using Entities.Concrete.DispensedUnitLookupEntity;
using Entities.Concrete.TherapistLookupEntity;
using Entities.Concrete.DozingStatusLookupEntity;
using Entities.Concrete.MedicationLookupEntity;
using Entities.Concrete.DozingScheduleLookupEntity;
using Entities.Concrete.AddendumEntity;
using Entities.Concrete.ADLEntity;
using Entities.Concrete.BillingClaim;
using Entities.Concrete.SendEmailEntity;
using Entities.Concrete.ReminderCallEntity;
using Entities.Concrete.ReminderStatusEntity;
using Entities.Concrete.EmailStatusEntity;
using Entities.Concrete.MessageStatusEntity;
using Entities.Concrete.AuditLogEntity;
using Entities.Concrete.TblSubPageEntity;
using Entities.Concrete.PatientNotesEntity;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
                                                                                : base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
                                                                        : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }

        protected IConfiguration Configuration { get; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<DiagnosisCode> DiagnosisCodes { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<AssignPracticeInsurance> AssignPracticeInsurance { get; set; }
        public DbSet<InsurancePayerType> InsurancePayerType { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<LocationWorkConfigStatus> LocationWorkConfigStatuses { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<PracticePayer> PracticePayers { get; set; }
        public DbSet<PracticeDoc> PracticeDocs { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<RaceLookup> RaceLookup { get; set; }
        public DbSet<EthnicityLookup> EthnicityLookup { get; set; }
        public DbSet<MaritalStatusLookup> MaritalStatusLookup { get; set; }
        public DbSet<PreferredCommsLookup> PreferredCommsLookup { get; set; }
        public DbSet<PreferredLanguageLookup> PreferredLanguageLookup { get; set; }
        public DbSet<SubstanceAbuseStatusLookup> SubstanceAbuseStatusLookup { get; set; }
        public DbSet<AlcoholLookup> AlcoholLookup { get; set; }
        public DbSet<IllicitSubstancesLookup> IllicitSubstancesLookup { get; set; }
        public DbSet<EmploymentStatusLookup> EmploymentStatusLookup { get; set; }
        public DbSet<WorkStatusLookup> WorkStatusLookup { get; set; }
        public DbSet<AccidentTypeLookup> AccidentTypeLookup { get; set; }
        public DbSet<WeekTypeLookup> WeekTypeLookup { get; set; }
        public DbSet<AppointmentAutoReminder> AppointmentAutoReminder { get; set; }
        public DbSet<SmokingStatusLookup> SmokingStatusLookup { get; set; }
        public DbSet<HospitalizationStatusLookup> HospitalizationStatusLookup { get; set; }
        public DbSet<PacksLookup> PacksLookup { get; set; }
        public DbSet<PatientDisabilityStatusLookup> PatientDisabilityStatusLookup { get; set; }
        public DbSet<SendEmail> SendEmail { get; set; }
        public DbSet<MessageStatus> MessageStatuses { get; set; }
        public DbSet<ReminderCall> ReminderCalls { get; set; }
        public DbSet<EmailStatus> EmailStatuses { get; set; }
        public DbSet<ReminderStatus> ReminderStatuses { get; set; }
        public DbSet<SchedulerStatus> SchedulerStatuses { get; set; }
        public DbSet<AppointmentScheduler> AppointmentSchedulers { get; set; }
        public DbSet<UserApp> UserApp { get; set; }
        public DbSet<ReminderProfile> ReminderProfiles { get; set; }
        public DbSet<ReminderDaysLookup> ReminderDaysLookup { get; set; }
        public DbSet<ReferralProviderType> ReferralProviderType { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TemplateCategory> TemplateCategory { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<ClinicalTemplateData> ClinicalTemplateData { get; set; }
        public DbSet<UserWorkHour> UserWorkHourConfig { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientInfoDetail> PatientInfoDetails { get; set; }
        public DbSet<PatientGuardian> PatientGuardians { get; set; }
        public DbSet<PatientEmployment> PatientEmployments { get; set; }
        public DbSet<PatientProvider> PatientProviders { get; set; }
        public DbSet<PatientRefAgency> PatientRefAgency { get; set; }
        public DbSet<PatientProbationOffice> PatientProbationOffice { get; set; }
        public DbSet<PatientPharmacy> PatientPharmacy { get; set; }
        public DbSet<PatientPCP> PatientPCP { get; set; }
        public DbSet<PatientDrugAlchohol> PatientDrugAlchohol { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<UserToPermission> UserToPermission { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<ProviderBoardCertificationInfo> ProviderBoardCertificationInfo { get; set; }
        public DbSet<ProviderDEAInfo> ProviderDEAInfo { get; set; }
        public DbSet<ProviderClinicalInfo> ProviderClinicalInfo { get; set; }
        public DbSet<ProviderSecurityCheckInfo> ProviderSecurityCheckInfo { get; set; }
        public DbSet<ProviderStateLicenseInfo> ProviderStateLicenseInfo { get; set; }
        public DbSet<ProviderWorkConfig> ProviderWorkConfig { get; set; }
        public DbSet<ReferralProvider> ReferralProvider { get; set; }
        public DbSet<PatientInsurance> PatientInsurance { get; set; }
        public DbSet<PatientInsuranceAuthorization> PatientInsuranceAuthorization { get; set; }
        public DbSet<PatientInsuranceAuthorizationCPT> PatientInsuranceAuthorizationCPT { get; set; }
        public DbSet<RoleToPermission> RoleToPermissions { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<PatientEducationWeb> PatientEducationWeb { get; set; }
        public DbSet<GroupPatientAppointment> GroupPatientAppointment { get; set; }
        public DbSet<RecurringAppointments> RecurringAppointments { get; set; }
        public DbSet<FollowUpAppointment> FollowUpAppointment { get; set; }
        public DbSet<DocumentParentCategoryLookup> DocumentParentCategoryLookup { get; set; }
        public DbSet<PatientDocumentCategory> PatientDocumentCategory { get; set; }
        public DbSet<PatientDocsFileUpload> PatientDocsFileUpload { get; set; }
        public DbSet<PatientDocument> PatientDocument { get; set; }
        public DbSet<ServiceProfile> ServiceProfile { get; set; }
        public DbSet<LocationWorkConfigs> LocationWorkConfigs { get; set; }
        public DbSet<AppointmentReasons> AppointmentReasons { get; set; }
        public DbSet<CityStateLookup> CityStateLookup { get; set; }
        public DbSet<Entities.Concrete.PatientVitalsEntity.PatientVitals> PatientVitals { get; set; }
        public DbSet<PatientDiagnosisCode> PatientDiagnosisCode { get; set; }
        public DbSet<ADLCategor> ADLCategor { get; set; }
        public DbSet<ADLFunction> ADLFunction { get; set; }
        public DbSet<ADLLookup> ADLLookup { get; set; }
        public DbSet<Addendum> Addendum { get; set; }
        public DbSet<PatientCommunication> PatientCommunication { get; set; }
        public DbSet<PatientEducationDocument> PatientEducationDocument { get; set; }
        public DbSet<CommunicationCallDetailType> CommunicationCallDetailType { get; set; }
        public DbSet<UserToProviderAssign> UserToProviderAssign { get; set; }
        public DbSet<UserToLocationAssign> UserToLocationAssign { get; set; }
        public DbSet<TblPage> TblPage { get; set; }
        public DbSet<TblSubPage> TblSubPage { get; set; }
        public DbSet<PracticeType> PracticeType { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<CLIAType> CLIAType { get; set; }
        public DbSet<TaxType> TaxType { get; set; }
        public DbSet<POS> POS { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<ReminderType> ReminderType { get; set; }
        public DbSet<ICDCategory> ICDCategory { get; set; }
        public DbSet<ICDToPractices> ICDToPractices { get; set; }
        public DbSet<AreaLookup> AreaLookup { get; set; }
        public DbSet<ProcedureGroup> ProcedureGroup { get; set; }
        public DbSet<ProcedureSubGroup> ProcedureSubGroup { get; set; }
        public DbSet<ProcedureGroupToPractices> ProcedureGroupToPractices { get; set; }
        public DbSet<PracticeAssignmentLookup> PracticeAssignmentLookup { get; set; }
        public DbSet<PatientInsuranceType> PatientInsuranceType { get; set; }
        public DbSet<SubscriberRelationshiplookup> SubscriberRelationshiplookup { get; set; }
        public DbSet<CountLookup> CountLookup { get; set; }
        public DbSet<PatientProvideReferring> PatientProvideReferring { get; set; }
        public DbSet<AppointmentsCheckIn> AppointmentsCheckIn { get; set; }
        public DbSet<PatientDispensing> PatientDispensing { get; set; }
        public DbSet<PatientDispensingDosing> PatientDispensingDosing { get; set; }
        public DbSet<ProgramLookup> ProgramLookup { get; set; }
        public DbSet<DispensedUnitLookup> DispensedUnitLookup { get; set; }
        public DbSet<TherapistLookup> TherapistLookup { get; set; }
        public DbSet<DozingStatusLookup> DozingStatusLookup { get; set; }
        public DbSet<MedicationLookup> MedicationLookup { get; set; }
        public DbSet<DozingScheduleLookup> DozingScheduleLookup { get; set; }
        public DbSet<BillingClaim> BillingClaim { get; set; }
        public DbSet<BillingClaimsAdditionalInfo> BillingClaimsAdditionalInfo { get; set; }
        public DbSet<BillingClaimsDiagnosisCode> BillingClaimsDiagnosisCode { get; set; }
        public DbSet<BillingClaimsPayerInfo> BillingClaimsPayerInfo { get; set; }
        public DbSet<BillingClaimsPaymentDetail> BillingClaimsPaymentDetail { get; set; }
        public DbSet<ClaimsBillingInfo> ClaimsBillingInfo { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<PatientNotes> PatientNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DArchMsContext")).EnableSensitiveDataLogging());

            }
        }

    }
}
 