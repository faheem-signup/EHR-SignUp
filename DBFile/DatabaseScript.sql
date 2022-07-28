USE [master]
GO
/****** Object:  Database [VCareEHRDB]    Script Date: 1/12/2022 4:28:21 PM ******/
CREATE DATABASE [VCareEHRDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VCareEHRDB', FILENAME = N'E:\VCareEHRDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VCareEHRDB_log', FILENAME = N'E:\VCareEHRDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VCareEHRDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VCareEHRDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VCareEHRDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VCareEHRDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VCareEHRDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VCareEHRDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VCareEHRDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [VCareEHRDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VCareEHRDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VCareEHRDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VCareEHRDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VCareEHRDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VCareEHRDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VCareEHRDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VCareEHRDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VCareEHRDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VCareEHRDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VCareEHRDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VCareEHRDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VCareEHRDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VCareEHRDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VCareEHRDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VCareEHRDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VCareEHRDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VCareEHRDB] SET RECOVERY FULL 
GO
ALTER DATABASE [VCareEHRDB] SET  MULTI_USER 
GO
ALTER DATABASE [VCareEHRDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VCareEHRDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VCareEHRDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VCareEHRDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VCareEHRDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VCareEHRDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VCareEHRDB', N'ON'
GO
ALTER DATABASE [VCareEHRDB] SET QUERY_STORE = OFF
GO
USE [VCareEHRDB]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [varchar](50) NULL,
	[StatusId] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diagnosises]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diagnosises](
	[DiagnosisId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[ShortDescription] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Diagnosis] PRIMARY KEY CLUSTERED 
(
	[DiagnosisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ICDToPractices]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ICDToPractices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DiagnosisId] [int] NULL,
	[PracticeId] [int] NULL,
 CONSTRAINT [PK_ICDToPractices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insurances]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurances](
	[InsuranceId] [int] IDENTITY(1,1) NOT NULL,
	[PayerId] [int] NULL,
	[PayerType] [int] NULL,
	[Name] [varchar](50) NULL,
	[AddressLine1] [varchar](200) NULL,
	[AddressLine2] [varchar](200) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[DenialResponseLimit] [int] NULL,
	[TimlyFilingLimit] [int] NULL,
	[BillingProvider] [int] NULL,
 CONSTRAINT [PK_Insurance] PRIMARY KEY CLUSTERED 
(
	[InsuranceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](50) NULL,
	[Address] [varchar](200) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [int] NULL,
	[NPI] [varchar](50) NULL,
	[POS] [int] NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[StatusId] [int] NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationWorkConfigs]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationWorkConfigs](
	[WorkConfigId] [int] IDENTITY(1,1) NOT NULL,
	[Day] [varchar](50) NULL,
	[StartFrom] [datetime] NULL,
	[EndTo] [datetime] NULL,
	[OpenAt] [int] NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_LocationWorkConfig] PRIMARY KEY CLUSTERED 
(
	[WorkConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientEmployments]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientEmployments](
	[PatientEmploymentId] [int] IDENTITY(1,1) NOT NULL,
	[EmploymentStatus] [int] NULL,
	[WorkStatus] [int] NULL,
	[EmployerName] [varchar](50) NULL,
	[EmployerAddress] [varchar](200) NULL,
	[EmployerPhone] [varchar](50) NULL,
	[AccidentDate] [datetime] NULL,
	[AccidentType] [int] NULL,
	[Wc] [nchar](10) NULL,
	[PatientId] [int] NULL,
 CONSTRAINT [PK_PatientEmployment] PRIMARY KEY CLUSTERED 
(
	[PatientEmploymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientGuardians]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientGuardians](
	[GuardianId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NULL,
	[GuardianName] [varchar](50) NULL,
	[GuardianRelation] [varchar](50) NULL,
	[GuardianAddress] [varchar](50) NULL,
	[GuardianPhone] [varchar](50) NULL,
 CONSTRAINT [PK_PatientGuardian] PRIMARY KEY CLUSTERED 
(
	[GuardianId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientInfoDetails]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientInfoDetails](
	[PatientInfoId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NULL,
	[SmokingStatus] [int] NULL,
	[Packs] [int] NULL,
	[HospitalizationStatus] [int] NULL,
	[LastHospitalizationDate] [datetime] NULL,
	[DisabilityDate] [datetime] NULL,
	[DisabilityStatus] [int] NULL,
	[DeathDate] [datetime] NULL,
	[DeathReason] [varchar](200) NULL,
 CONSTRAINT [PK_PatientInfoDetail] PRIMARY KEY CLUSTERED 
(
	[PatientInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientProviders]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientProviders](
	[PatientProviderId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NULL,
	[AttendingPhysician] [int] NULL,
	[SupervisingProvider] [int] NULL,
	[ReferringProvider] [int] NULL,
	[PCPName] [int] NULL,
	[Pharmacy] [varchar](50) NULL,
	[ReferringAgency] [int] NULL,
	[DrugsAgency] [int] NULL,
	[ProbationOfficer] [int] NULL,
	[SubstanceAbuseStatus] [int] NULL,
	[Alcohol] [int] NULL,
	[IllicitSubstances] [int] NULL,
 CONSTRAINT [PK_PatientProvider] PRIMARY KEY CLUSTERED 
(
	[PatientProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Suffix] [varchar](50) NULL,
	[DOB] [datetime] NULL,
	[Gender] [int] NULL,
	[SSN] [varchar](12) NULL,
	[AddressLine1] [varchar](300) NULL,
	[AddressLine2] [varchar](300) NULL,
	[Country] [int] NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[ZIP] [varchar](50) NULL,
	[Race] [int] NULL,
	[Ethnicity] [int] NULL,
	[MaritalStatus] [int] NULL,
	[CellPhone] [varchar](50) NULL,
	[HomePhone] [varchar](50) NULL,
	[WorkPhone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[PreferredCom] [int] NULL,
	[PreferredLanguage] [int] NULL,
	[EmergencyConName] [varchar](50) NULL,
	[EmergencyConAddress] [varchar](50) NULL,
	[EmergencyConRelation] [varchar](50) NULL,
	[EmergencyConPhone] [varchar](50) NULL,
	[StatusId] [int] NULL,
	[Comment] [varchar](200) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] NOT NULL,
	[PermissionDescription] [varchar](50) NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PracticeDocs]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PracticeDocs](
	[DocmentId] [int] IDENTITY(1,1) NOT NULL,
	[PracticeId] [int] NULL,
	[DocumentName] [varchar](50) NULL,
	[Description] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_PracticeDocs] PRIMARY KEY CLUSTERED 
(
	[DocmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PracticePayers]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PracticePayers](
	[PracticePayerId] [int] IDENTITY(1,1) NOT NULL,
	[PayerName] [varchar](50) NULL,
	[PayerId] [int] NULL,
	[TypeQualifier] [varchar](50) NULL,
	[Location] [varchar](50) NULL,
	[PracticeId] [int] NULL,
 CONSTRAINT [PK_PracticePayer] PRIMARY KEY CLUSTERED 
(
	[PracticePayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Practices]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practices](
	[PracticeId] [int] IDENTITY(1,1) NOT NULL,
	[LegalBusinessName] [varchar](50) NULL,
	[DBA] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[PhysicalAddress] [varchar](200) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [int] NULL,
	[PhoneNumber] [varchar](50) NULL,
	[FaxNumber] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[OfficeEmail] [varchar](50) NULL,
	[ContactPersonEmail] [varchar](50) NULL,
	[ContactPerson] [varchar](50) NULL,
	[ContactPersonPhone] [varchar](50) NULL,
	[CLIANumber] [varchar](50) NULL,
	[CLIAType] [int] NULL,
	[LiabilityInsuranceID] [varchar](50) NULL,
	[LiabilityInsuranceExpiryDate] [datetime] NULL,
	[LiabilityInsuranceCarrier] [varchar](50) NULL,
	[StateLicense] [varchar](50) NULL,
	[StateLicenseExpiryDate] [datetime] NULL,
	[DeaNumber] [varchar](50) NULL,
	[DeaNumberExpiryDate] [datetime] NULL,
	[BillingAddress] [varchar](200) NULL,
	[BillingPhone] [varchar](50) NULL,
	[TaxIdType] [int] NULL,
	[BillingCity] [int] NULL,
	[BillingState] [int] NULL,
	[BillingZIP] [int] NULL,
	[BillingFax] [varchar](50) NULL,
	[BillingNPI] [varchar](50) NULL,
	[TaxIDNumber] [varchar](50) NULL,
	[AcceptAssignment] [int] NULL,
	[Taxonomy] [varchar](50) NULL,
	[StatusId] [int] NULL,
 CONSTRAINT [PK_Practices] PRIMARY KEY CLUSTERED 
(
	[PracticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcedureGroupToPractices]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureGroupToPractices](
	[ProcedureGroupId] [int] IDENTITY(1,1) NOT NULL,
	[PracticeId] [int] NULL,
	[ProcedureId] [int] NULL,
 CONSTRAINT [PK_CPTGroupToPractice] PRIMARY KEY CLUSTERED 
(
	[ProcedureGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Procedures]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procedures](
	[ProcedureId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[NDCNumber] [varchar](50) NULL,
	[ShortDescription] [varchar](200) NULL,
	[Description] [varchar](200) NULL,
	[POS] [int] NULL,
	[IsExpired] [bit] NULL,
	[Date] [datetime] NULL,
	[DefaultCharges] [decimal](18, 0) NULL,
	[MedicareAllowance] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Procedure] PRIMARY KEY CLUSTERED 
(
	[ProcedureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provider](
	[ProviderId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[MI] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Address] [varchar](200) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [varchar](50) NULL,
	[ProviderEmail] [varchar](50) NULL,
	[OfficeEmail] [varchar](50) NULL,
	[PreviousName] [varchar](50) NULL,
	[Degree] [varchar](50) NULL,
	[NPINumber] [varchar](50) NULL,
	[StateLicenseNo] [varchar](50) NULL,
	[StateLicenseExpiryDate] [datetime] NULL,
	[TaxonomyNo] [varchar](50) NULL,
	[Specialty] [varchar](50) NULL,
	[BoardCertified] [varchar](50) NULL,
	[CertBody] [varchar](50) NULL,
	[CertNumber] [varchar](50) NULL,
	[CertExpiryDate] [datetime] NULL,
	[DEANumber] [varchar](50) NULL,
	[DEAExpiryDate] [datetime] NULL,
	[PLICarrierName] [varchar](50) NULL,
	[PLINumber] [varchar](50) NULL,
	[PLIExpiryDate] [datetime] NULL,
	[CAQHID] [int] NULL,
	[CAQHUsername] [varchar](50) NULL,
	[CAQHPassword] [varchar](max) NULL,
	[AssignedLocations] [nchar](10) NULL,
	[JoiningDate] [datetime] NULL,
	[ReHiringDate] [datetime] NULL,
	[TerminationDate] [datetime] NULL,
	[AssignedService] [int] NULL,
	[ChildAbuseCertificate] [varchar](max) NULL,
	[ChildAbuseCertExpiryDate] [datetime] NULL,
	[MandatedReporterCertificate] [varchar](max) NULL,
	[MandatedReportExpiryDate] [datetime] NULL,
	[SecurityCheck] [varchar](max) NULL,
	[FBIState] [varchar](max) NULL,
	[StatePoliceClearance] [varchar](max) NULL,
	[AssignedRoom] [int] NULL,
	[CredentialedInsurances] [varchar](max) NULL,
	[FlatRate] [decimal](18, 0) NULL,
	[ProcedureBasedRate] [decimal](18, 0) NULL,
	[HourlyRate] [decimal](18, 0) NULL,
	[ContinuingEducation] [varchar](max) NULL,
	[CompletedHours] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderWorkConfig]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderWorkConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderId] [int] NULL,
	[LocationId] [int] NULL,
	[Days] [varchar](50) NULL,
	[FirstShiftWorkFrom] [datetime] NULL,
	[FirstShiftWorkTo] [datetime] NULL,
	[BreakShiftWorkFrom] [datetime] NULL,
	[BreakShiftWorkTo] [datetime] NULL,
	[SlotSize] [time](7) NULL,
 CONSTRAINT [PK_ProviderWorkConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferralProvider]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferralProvider](
	[ReferralProviderId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MI] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[ZIP] [int] NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Speciality] [varchar](50) NULL,
	[TaxID] [varchar](50) NULL,
	[License] [varchar](50) NULL,
	[SSN] [varchar](50) NULL,
	[NPI] [varchar](50) NULL,
	[ContactPerson] [varchar](50) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_ReferralProvider] PRIMARY KEY CLUSTERED 
(
	[ReferralProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleToPermissions]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleToPermissions](
	[PracticeToRoleId] [int] NOT NULL,
	[RoleId] [int] NULL,
	[PermissionId] [int] NULL,
 CONSTRAINT [PK_RoleToPermissions] PRIMARY KEY CLUSTERED 
(
	[PracticeToRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](50) NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MI] [varchar](50) NULL,
	[UserSSN] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[PersonalEmail] [varchar](50) NULL,
	[HourlyRate] [decimal](18, 0) NULL,
	[IsProvider] [bit] NULL,
	[State] [int] NULL,
	[City] [int] NULL,
	[DOB] [datetime] NULL,
	[UserType] [varchar](50) NULL,
	[AutoLockTime] [int] NULL,
	[StatusId] [int] NULL,
	[RoleId] [int] NULL,
	[Password] [varchar](max) NULL,
	[IsFirstLogin] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToLocationAssign]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToLocationAssign](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_UserToLocationAssign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToPermission]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToPermission](
	[UserToRoleId] [int] NOT NULL,
	[RoleId] [int] NULL,
	[UserId] [int] NULL,
	[PermissionId] [int] NULL,
 CONSTRAINT [PK_UserToPermission] PRIMARY KEY CLUSTERED 
(
	[UserToRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToProviderAssign]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToProviderAssign](
	[Id] [nchar](10) NOT NULL,
	[UserId] [int] NULL,
	[ProviderId] [int] NULL,
 CONSTRAINT [PK_UserToProviderAssign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserWorkHourConfig]    Script Date: 1/12/2022 4:28:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserWorkHourConfig](
	[Id] [int] NOT NULL,
	[UserId] [int] NULL,
	[LocationId] [int] NULL,
	[Days] [varchar](50) NULL,
	[FirstShiftWorkFrom] [datetime] NULL,
	[FirstShiftWorkTo] [datetime] NULL,
	[SecondShiftWorkFrom] [datetime] NULL,
	[SecondShiftWorkTo] [datetime] NULL,
 CONSTRAINT [PK_UserWorkHourConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Statuses]
GO
ALTER TABLE [dbo].[ICDToPractices]  WITH CHECK ADD  CONSTRAINT [FK_ICDToPractices_Diagnosises] FOREIGN KEY([DiagnosisId])
REFERENCES [dbo].[Diagnosises] ([DiagnosisId])
GO
ALTER TABLE [dbo].[ICDToPractices] CHECK CONSTRAINT [FK_ICDToPractices_Diagnosises]
GO
ALTER TABLE [dbo].[ICDToPractices]  WITH CHECK ADD  CONSTRAINT [FK_ICDToPractices_Practices] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practices] ([PracticeId])
GO
ALTER TABLE [dbo].[ICDToPractices] CHECK CONSTRAINT [FK_ICDToPractices_Practices]
GO
ALTER TABLE [dbo].[LocationWorkConfigs]  WITH CHECK ADD  CONSTRAINT [FK_LocationWorkConfigs_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[LocationWorkConfigs] CHECK CONSTRAINT [FK_LocationWorkConfigs_Locations]
GO
ALTER TABLE [dbo].[PatientEmployments]  WITH CHECK ADD  CONSTRAINT [FK_PatientEmployments_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientEmployments] CHECK CONSTRAINT [FK_PatientEmployments_Patients]
GO
ALTER TABLE [dbo].[PatientGuardians]  WITH CHECK ADD  CONSTRAINT [FK_PatientGuardians_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientGuardians] CHECK CONSTRAINT [FK_PatientGuardians_Patients]
GO
ALTER TABLE [dbo].[PatientInfoDetails]  WITH CHECK ADD  CONSTRAINT [FK_PatientInfoDetails_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientInfoDetails] CHECK CONSTRAINT [FK_PatientInfoDetails_Patients]
GO
ALTER TABLE [dbo].[PatientProviders]  WITH CHECK ADD  CONSTRAINT [FK_PatientProviders_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientProviders] CHECK CONSTRAINT [FK_PatientProviders_Patients]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Statuses]
GO
ALTER TABLE [dbo].[PracticeDocs]  WITH CHECK ADD  CONSTRAINT [FK_PracticeDocs_Practices] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practices] ([PracticeId])
GO
ALTER TABLE [dbo].[PracticeDocs] CHECK CONSTRAINT [FK_PracticeDocs_Practices]
GO
ALTER TABLE [dbo].[PracticePayers]  WITH CHECK ADD  CONSTRAINT [FK_PracticePayers_Practices] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practices] ([PracticeId])
GO
ALTER TABLE [dbo].[PracticePayers] CHECK CONSTRAINT [FK_PracticePayers_Practices]
GO
ALTER TABLE [dbo].[Practices]  WITH CHECK ADD  CONSTRAINT [FK_Practices_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Practices] CHECK CONSTRAINT [FK_Practices_Statuses]
GO
ALTER TABLE [dbo].[ProcedureGroupToPractices]  WITH CHECK ADD  CONSTRAINT [FK_CPTGroupToPractice_Practices] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practices] ([PracticeId])
GO
ALTER TABLE [dbo].[ProcedureGroupToPractices] CHECK CONSTRAINT [FK_CPTGroupToPractice_Practices]
GO
ALTER TABLE [dbo].[ProcedureGroupToPractices]  WITH CHECK ADD  CONSTRAINT [FK_CPTGroupToPractice_Procedure] FOREIGN KEY([ProcedureId])
REFERENCES [dbo].[Procedures] ([ProcedureId])
GO
ALTER TABLE [dbo].[ProcedureGroupToPractices] CHECK CONSTRAINT [FK_CPTGroupToPractice_Procedure]
GO
ALTER TABLE [dbo].[ProviderWorkConfig]  WITH CHECK ADD  CONSTRAINT [FK_ProviderWorkConfig_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO
ALTER TABLE [dbo].[ProviderWorkConfig] CHECK CONSTRAINT [FK_ProviderWorkConfig_Provider]
GO
ALTER TABLE [dbo].[RoleToPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RoleToPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[RoleToPermissions] CHECK CONSTRAINT [FK_RoleToPermissions_Permissions]
GO
ALTER TABLE [dbo].[RoleToPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RoleToPermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RoleToPermissions] CHECK CONSTRAINT [FK_RoleToPermissions_Roles]
GO
ALTER TABLE [dbo].[UserToLocationAssign]  WITH CHECK ADD  CONSTRAINT [FK_UserToLocationAssign_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[UserToLocationAssign] CHECK CONSTRAINT [FK_UserToLocationAssign_Locations]
GO
ALTER TABLE [dbo].[UserToLocationAssign]  WITH CHECK ADD  CONSTRAINT [FK_UserToLocationAssign_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserToLocationAssign] CHECK CONSTRAINT [FK_UserToLocationAssign_Users]
GO
ALTER TABLE [dbo].[UserToPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserToPermission_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[UserToPermission] CHECK CONSTRAINT [FK_UserToPermission_Permissions]
GO
ALTER TABLE [dbo].[UserToPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserToPermission_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserToPermission] CHECK CONSTRAINT [FK_UserToPermission_Roles]
GO
ALTER TABLE [dbo].[UserToPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserToPermission_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserToPermission] CHECK CONSTRAINT [FK_UserToPermission_Users]
GO
ALTER TABLE [dbo].[UserToProviderAssign]  WITH CHECK ADD  CONSTRAINT [FK_UserToProviderAssign_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO
ALTER TABLE [dbo].[UserToProviderAssign] CHECK CONSTRAINT [FK_UserToProviderAssign_Provider]
GO
ALTER TABLE [dbo].[UserToProviderAssign]  WITH CHECK ADD  CONSTRAINT [FK_UserToProviderAssign_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserToProviderAssign] CHECK CONSTRAINT [FK_UserToProviderAssign_Users]
GO
ALTER TABLE [dbo].[UserWorkHourConfig]  WITH CHECK ADD  CONSTRAINT [FK_UserWorkHourConfig_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserWorkHourConfig] CHECK CONSTRAINT [FK_UserWorkHourConfig_Users]
GO
USE [master]
GO
ALTER DATABASE [VCareEHRDB] SET  READ_WRITE 
GO
