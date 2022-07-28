using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Business.Services.Authentication;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Abstract.IDiagnosisRepository;
using DataAccess.Abstract.IPracticeDocsRepository;
using DataAccess.Abstract.IPracticePayersRepository;
using DataAccess.Abstract.IPracticesRepository;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.IProceduresRepository;
using DataAccess.Abstract.IUserAppRepository;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.DiagnosisRepository;
using DataAccess.Concrete.EntityFramework.PracticeDocsRepository;
using DataAccess.Concrete.EntityFramework.PracticePayersRepository;
using DataAccess.Concrete.EntityFramework.PracticesRepository;
using DataAccess.Concrete.EntityFramework.LocationRepository;
using DataAccess.Concrete.EntityFramework.ProceduresRepository;
using DataAccess.Concrete.EntityFramework.UserAppRepository;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using DataAccess.Concrete.EntityFramework.UserWorkHourConfigRepository;
using DataAccess.Abstract.IRolesRepository;
using DataAccess.Concrete.EntityFramework.RolesRepository;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Concrete.EntityFramework.PatientRepository;
using DataAccess.Abstract.IPatientInfoDetailsRepository;
using DataAccess.Concrete.EntityFramework.PatientInfoDetailsRepository;
using DataAccess.Abstract.IPatientProvidersRepository;
using DataAccess.Concrete.EntityFramework.PatientProvidersRepository;
using DataAccess.Abstract.IPatientEmploymentsRepository;
using DataAccess.Concrete.EntityFramework.PatientEmploymentsRepository;
using DataAccess.Abstract.IPatientGuardiansRepository;
using DataAccess.Concrete.EntityFramework.PatientGuardiansRepository;
using DataAccess.Abstract.IPermissionRepository;
using DataAccess.Concrete.EntityFramework.PermissionRepository;
using DataAccess.Abstract.IUserToPermissionRepository;
using DataAccess.Concrete.EntityFramework.UserToPermissionRepository;
using DataAccess.Abstract.IProviderRepository;
using DataAccess.Concrete.EntityFramework.ProviderRepository;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Concrete.EntityFramework.ProviderWorkConfigRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using DataAccess.Concrete.EntityFramework.ReferralProviderRepository;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Concrete.EntityFramework.PatientInsuranceRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using DataAccess.Concrete.EntityFramework.PatientInsuranceAuthorizationRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Concrete.EntityFramework.PatientInsuranceAuthorizationCPTRepository;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Concrete.EntityFramework.AppointmentRepository;
using Core.Utilities.Security.Jwt.UserAppToken;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Concrete.EntityFramework.GroupPatientAppointmentRepository;
using DataAccess.Abstract.IRoleToPermissionRepository;
using DataAccess.Concrete.EntityFramework.RoleToPermissionRepository;
using DataAccess.Abstract.IReminderProfileRepository;
using DataAccess.Concrete.EntityFramework.ReminderProfileRepository;
using DataAccess.Abstract.IRoomRepository;
using DataAccess.Concrete.EntityFramework.RoomRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using DataAccess.Concrete.EntityFramework.RecurringAppointmentsRepository;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Concrete.EntityFramework.FollowUpAppointmentRepository;
using DataAccess.Abstract.IFormTemplateRepository;
using DataAccess.Concrete.EntityFramework.FormTemplateRepository;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using DataAccess.Concrete.EntityFramework.DocumentParentCategoryLookupRepository;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using DataAccess.Concrete.EntityFramework.PatientDocumentCategoryRepository;
using DataAccess.Abstract.IPatientDocsFileUploadRepository;
using DataAccess.Concrete.EntityFramework.PatientDocsFileUploadRepository;
using DataAccess.Abstract.IPatientDocumentRepository;
using DataAccess.Concrete.EntityFramework.PatientDocumentRepository;
using DataAccess.Abstract.IServiceProfileRepository;
using DataAccess.Concrete.EntityFramework.ServiceProfileRepository;
using DataAccess.Abstract.IInsuranceRepository;
using DataAccess.Concrete.EntityFramework.InsuranceRepository;
using DataAccess.Abstract.IAssignInsuranceRepository;
using DataAccess.Concrete.EntityFramework.AssignInsuranceRepository;
using DataAccess.Abstract.IClinicalTemplateDataRepository;
using DataAccess.Concrete.EntityFramework.ClinicalTemplateDataRepository;
using DataAccess.Concrete.EntityFramework.LocationWorkConfigsRepository;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using DataAccess.Abstract.IAppointmentReasonsRepository;
using DataAccess.Concrete.EntityFramework.AppointmentReasonsRepository;
using DataAccess.Abstract.ISchedulerRepository;
using DataAccess.Concrete.EntityFramework.SchedulerRepository;
using DataAccess.Abstract.ICityStateLookupRepository;
using DataAccess.Concrete.EntityFramework.CityStateLookupRepository;
using DataAccess.Concrete.EntityFramework.PatientVitalsRepository;
using DataAccess.Abstract.IPatientVitalsRepository;
using DataAccess.Abstract.ISchedulerStatusRepository;
using DataAccess.Concrete.EntityFramework.SchedulerStatusRepository;
using DataAccess.Abstract.IPatientDiagnosisCodeRepository;
using DataAccess.Concrete.EntityFramework.PatientDiagnosisCodeRepository;
using DataAccess.Abstract.IPatientCommunicationRepository;
using DataAccess.Concrete.EntityFramework.PatientCommunicationRepository;
using DataAccess.Abstract.IUserToLocationAssignRepository;
using DataAccess.Concrete.EntityFramework.UserToLocationAssignRepository;
using DataAccess.Abstract.IUserToProviderAssignRepository;
using DataAccess.Concrete.EntityFramework.UserToProviderAssignRepository;
using DataAccess.Abstract.IGenericLookupRepository;
using DataAccess.Concrete.EntityFramework.GenericLookupRepository;
using NPOI.SS.Formula.Functions;
using DataAccess.Concrete.EntityFramework.TBLPageRepository;
using DataAccess.Abstract.ITBLPageRepository;
using DataAccess.Abstract.IContactRepository;
using DataAccess.Concrete.EntityFramework.ContactRepository;
using DataAccess.Abstract.IICDToPracticesRepository;
using DataAccess.Concrete.EntityFramework.ICDToPracticesRepository;

namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
                sp.GetService<IHttpContextAccessor>().HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddTransient<IUserAppTokenHelper, UserAppTokenHelper>();
            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<ProjectDbContext, DArchInMemory>(ServiceLifetime.Transient);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            //services.AddTransient<ITranslateRepository, TranslateRepository>();
            //services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext>();
            services.AddTransient<IDiagnosisRepository, DiagnosisRepository>();
            services.AddTransient<IProceduresRepository, ProceduresRepository>();
            services.AddTransient<IPracticesRepository, PracticesRepository>();
            services.AddTransient<IPracticeDocsRepository, PracticeDocsRepository>();
            services.AddTransient<IPracticePayersRepository, PracticePayersRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IUserAppRepository, UserAppRepository>();
            services.AddTransient<IUserWorkHourRepository, UserWorkHourRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientInfoDetailsRepository, PatientInfoDetailsRepository>();
            services.AddTransient<IPatientGuardiansRepository, PatientGuardiansRepository>();
            services.AddTransient<IPatientEmploymentsRepository, PatientEmploymentsRepository>();
            services.AddTransient<IPatientProvidersRepository, PatientProvidersRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IUserToPermissionRepository, UserToPermissionRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<IProviderWorkConfigRepository, ProviderWorkConfigRepository>();
            services.AddTransient<IReferralProviderRepository, ReferralProviderRepository>();
            services.AddTransient<IPatientInsuranceRepository, PatientInsuranceRepository>();
            services.AddTransient<IPatientInsuranceAuthorizationRepository, PatientInsuranceAuthorizationRepository>();
            services.AddTransient<IPatientInsuranceAuthorizationCPTRepository, PatientInsuranceAuthorizationCPTRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IGroupPatientAppointmentRepository, GroupPatientAppointmentRepository>();
            services.AddTransient<IRoleToPermissionRepository, RoleToPermissionRepository>();
            services.AddTransient<IReminderProfileRepository, ReminderProfileRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<ISchedulerRepository, SchedulerRepository>();
            services.AddTransient<ISchedulerStatusRepository, SchedulerStatusRepository>();
            services.AddTransient<IFormTemplateRepository, FormTemplateRepository>();
            services.AddTransient<IInsuranceRepository, InsuranceRepository>();
            services.AddTransient<IAssignInsuranceRepository, AssignInsuranceRepository>();
            services.AddTransient<IClinicalTemplateDataRepository, ClinicalTemplateDataRepository>();
            services.AddTransient<IRecurringAppointmentsRepository, RecurringAppointmentsRepository>();
            services.AddTransient<IFollowUpAppointmentRepository, FollowUpAppointmentRepository>();
            services.AddTransient<IDocumentParentCategoryLookupRepository, DocumentParentCategoryLookupRepository>();
            services.AddTransient<IPatientDocumentCategoryRepository, PatientDocumentCategoryRepository>();
            services.AddTransient<IPatientDocsFileUploadRepository, PatientDocsFileUploadRepository>();
            services.AddTransient<IPatientDocumentRepository, PatientDocumentRepository>();
            services.AddTransient<IServiceProfileRepository, ServiceProfileRepository>();
            services.AddTransient<ILocationWorkConfigsRepository, LocationWorkConfigsRepository>();
            services.AddTransient<IAppointmentReasonsRepository, AppointmentReasonsRepository>();
            services.AddTransient<ICityStateLookupRepository, CityStateLookupRepository>();
            services.AddTransient<IPatientVitalsRepository, PatientVitalsRepository>();
            services.AddTransient<IPatientDiagnosisCodeRepository, PatientDiagnosisCodeRepository>();
            services.AddTransient<IPatientCommunicationRepository, PatientCommunicationRepository>();
            services.AddTransient<IUserToProviderAssignRepository, UserToProviderAssignRepository>();
            services.AddTransient<IUserToLocationAssignRepository, UserToLocationAssignRepository>();
            services.AddTransient<IGenericLookupRepository, GenericLookupRepository>();
            services.AddTransient<ITBLPageRepository, TBLPageRepository>();
            services.AddTransient<IICDToPracticesRepository, ICDToPracticesRepository>();


            //    services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();


            services.AddDbContext<ProjectDbContext>();

            //   services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}