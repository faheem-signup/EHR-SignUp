USE [VCareEHRDB]
GO
SET IDENTITY_INSERT [dbo].[TblPage] ON 

INSERT [dbo].[TblPage] ([PageId], [PageName]) VALUES (1, N'Setting')
INSERT [dbo].[TblPage] ([PageId], [PageName]) VALUES (2, N'Practice')
INSERT [dbo].[TblPage] ([PageId], [PageName]) VALUES (3, N'Scheduler')
SET IDENTITY_INSERT [dbo].[TblPage] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (5, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (6, N'Staff')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (7, N'fff')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (8, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (9, N'Staff')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleToPermissions] ON 

INSERT [dbo].[RoleToPermissions] ([Id], [RoleId], [PageId], [CanAdd], [CanEdit], [CanView], [CanDelete], [CanSearch]) VALUES (9, 5, 1, 1, 0, 1, 1, 1)
INSERT [dbo].[RoleToPermissions] ([Id], [RoleId], [PageId], [CanAdd], [CanEdit], [CanView], [CanDelete], [CanSearch]) VALUES (10, 5, 2, 1, 0, 1, 1, 1)
INSERT [dbo].[RoleToPermissions] ([Id], [RoleId], [PageId], [CanAdd], [CanEdit], [CanView], [CanDelete], [CanSearch]) VALUES (11, 5, 3, 1, 0, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[RoleToPermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[UserToPermission] ON 

INSERT [dbo].[UserToPermission] ([UserToPermissionId], [UserId], [CanView], [CanEdit], [CanAdd], [CanSearch], [CanDelete], [RoleToPermissionsId]) VALUES (1006, 17, 1, 1, 1, 1, 1, 9)
INSERT [dbo].[UserToPermission] ([UserToPermissionId], [UserId], [CanView], [CanEdit], [CanAdd], [CanSearch], [CanDelete], [RoleToPermissionsId]) VALUES (1008, 17, 1, 1, 1, 1, 1, 10)
INSERT [dbo].[UserToPermission] ([UserToPermissionId], [UserId], [CanView], [CanEdit], [CanAdd], [CanSearch], [CanDelete], [RoleToPermissionsId]) VALUES (2014, 17, 1, 1, 1, 1, 1, 11)
SET IDENTITY_INSERT [dbo].[UserToPermission] OFF
GO


SET IDENTITY_INSERT [dbo].[UserApp] ON 
INSERT [dbo].[UserApp] ([UserId], [FirstName], [LastName], [MI], [UserSSN], [Address], [CellNumber], [PersonalEmail], [HourlyRate], [IsProvider], [State], [City], [DOB], [UserTypeId], [AutoLockTime], [StatusId], [RoleId], [PasswordSalt], [PasswordHash], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [PracticeId]) VALUES (17, N'Admin', N'Admin', N'string', N'string', N'string', N'string', N'test@test.com', CAST(0 AS Decimal(18, 0)), 1, 0, 0, CAST(N'2022-02-16T17:05:54.647' AS DateTime), 1, 0, 1, 5, 0xE554F88905DDCA6AD21814FF10AFB3D652C346019AF4D2C494DEA1F71878F41C1A124DD5083A7630A87631355A3CDDA17EFDEE020B3EFE4FB4CEDA4033AA390C, 0x730FCE595F131B35CFE5B87DA8250ACF18180600D194F1FB7C9309F93C40029573FF727F284967E7F1C63455248FE1CF711307A2C9185195C0D17E8FC5E17283B7A590288082E5AF2D66E536563A03FEBA7C0B512B63244AD8D1D9C2271BCD3539ECCF5577E7187F2FEFCC91A9721E2DBB64EABB8DE38DF3BFF0EE18E8570008, 1, CAST(N'2022-02-16T22:09:46.430' AS DateTime), 1, CAST(N'2022-02-16T22:09:46.457' AS DateTime), 0, NULL)
SET IDENTITY_INSERT [dbo].[UserApp] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureGroup] ON 

INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (1, N'Evaluation and Management', N'99201 — 99499')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (2, N'Pathotogy and Laboratory', N'80047 — 89398')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (3, N'Surgery', N'10021 — 69990')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (4, N'Macbctno', N'90281 — 99199')
SET IDENTITY_INSERT [dbo].[ProcedureGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[ProcedureGroupToPractices] ON 

INSERT [dbo].[ProcedureGroupToPractices] ([ProcedureToGroupId], [PracticeId], [ProcedureGroupId], [ProcedureSubGroupId]) VALUES (4002, 14083, 1, 1)
INSERT [dbo].[ProcedureGroupToPractices] ([ProcedureToGroupId], [PracticeId], [ProcedureGroupId], [ProcedureSubGroupId]) VALUES (4003, 14083, 3, 6)
INSERT [dbo].[ProcedureGroupToPractices] ([ProcedureToGroupId], [PracticeId], [ProcedureGroupId], [ProcedureSubGroupId]) VALUES (4004, 14083, 3, 8)
SET IDENTITY_INSERT [dbo].[ProcedureGroupToPractices] OFF
GO
SET IDENTITY_INSERT [dbo].[AppointmentAutoReminder] ON 

INSERT [dbo].[AppointmentAutoReminder] ([AppointmentAutoReminderId], [AutoReminderDescription]) VALUES (1, N'Call')
INSERT [dbo].[AppointmentAutoReminder] ([AppointmentAutoReminderId], [AutoReminderDescription]) VALUES (2, N'Message')
INSERT [dbo].[AppointmentAutoReminder] ([AppointmentAutoReminderId], [AutoReminderDescription]) VALUES (3, N'Email')
SET IDENTITY_INSERT [dbo].[AppointmentAutoReminder] OFF
GO
SET IDENTITY_INSERT [dbo].[AppointmentStatuses] ON 

INSERT [dbo].[AppointmentStatuses] ([AppointmentStatusId], [AppointmentStatusName]) VALUES (1, N'Active')
INSERT [dbo].[AppointmentStatuses] ([AppointmentStatusId], [AppointmentStatusName]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[AppointmentStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[AccidentTypeLookup] ON 

INSERT [dbo].[AccidentTypeLookup] ([AccidentTypeLookupId], [Description]) VALUES (1, N'Bus')
INSERT [dbo].[AccidentTypeLookup] ([AccidentTypeLookupId], [Description]) VALUES (2, N'Car')
INSERT [dbo].[AccidentTypeLookup] ([AccidentTypeLookupId], [Description]) VALUES (3, N'Bike')
SET IDENTITY_INSERT [dbo].[AccidentTypeLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[AppointmentTypes] ON 

INSERT [dbo].[AppointmentTypes] ([AppointmentTypeId], [AppointmentTypeName]) VALUES (1, N'Face To Face')
INSERT [dbo].[AppointmentTypes] ([AppointmentTypeId], [AppointmentTypeName]) VALUES (2, N'Telehealth')
SET IDENTITY_INSERT [dbo].[AppointmentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[AreaLookup] ON 

INSERT [dbo].[AreaLookup] ([AreaId], [AreaName]) VALUES (1, N'ICU')
INSERT [dbo].[AreaLookup] ([AreaId], [AreaName]) VALUES (2, N'OPD')
INSERT [dbo].[AreaLookup] ([AreaId], [AreaName]) VALUES (3, N'Cardiology')
INSERT [dbo].[AreaLookup] ([AreaId], [AreaName]) VALUES (4, N'Orthopadic')
INSERT [dbo].[AreaLookup] ([AreaId], [AreaName]) VALUES (5, N'Xray')
SET IDENTITY_INSERT [dbo].[AreaLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[CityStateLookup] ON 

INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (1, 100, N'Adak', 200, N'Aleutians West Census Area', 300, N'AK', N'Alaska', 400, N'99546', N'51.88000', N'-176.65806')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (2, 101, N'Akhiok', 201, N'Kodiak Island Borough', 301, N'AK', N'Alaska', 401, N'99615', N'56.94556', N'-154.17028')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (3, 102, N'Badger', 202, N'Fairbanks North Star Borough', 302, N'AK', N'Alaska', 402, N'99705', N'64.80000', N'-147.53333')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (4, 103, N'Cantwell', 203, N'Denali Borough', 303, N'AK', N'Alaska', 403, N'99729', N'63.39167', N'-148.95083')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (5, 104, N'Central', 204, N'Yukon-Koyukuk Census Area', 304, N'AK', N'Alaska', 404, N'99730', N'65.57250', N'-144.80306')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (6, 105, N'Montgomery', NULL, NULL, 305, N'AL', N'Alabama', 404, N'36104', N'65.57250', N'-144.80306')
INSERT [dbo].[CityStateLookup] ([ID], [CityId], [CityName], [CountyId], [County], [StateId], [StateCode], [State], [ZipId], [ZipCode], [Latitude], [Longitude]) VALUES (7, 106, N'Sacramento', NULL, NULL, 306, N'CA', N'California', 405, N'95814', N'0', N'0')
SET IDENTITY_INSERT [dbo].[CityStateLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[CommunicationCallDetailType] ON 

INSERT [dbo].[CommunicationCallDetailType] ([CallDetailTypeId], [CallDetail]) VALUES (1, N'Incomming')
INSERT [dbo].[CommunicationCallDetailType] ([CallDetailTypeId], [CallDetail]) VALUES (2, N'Outgoing')
SET IDENTITY_INSERT [dbo].[CommunicationCallDetailType] OFF
GO
SET IDENTITY_INSERT [dbo].[CLIAType] ON 

INSERT [dbo].[CLIAType] ([CLIATypeId], [Description]) VALUES (1, N'Certificate of waiver')
INSERT [dbo].[CLIAType] ([CLIATypeId], [Description]) VALUES (2, N'Certificate for Provider Performed Microscopic Procedures')
INSERT [dbo].[CLIAType] ([CLIATypeId], [Description]) VALUES (3, N'Certificate of Compliance')
INSERT [dbo].[CLIAType] ([CLIATypeId], [Description]) VALUES (4, N'Certificate of Accreditation')
SET IDENTITY_INSERT [dbo].[CLIAType] OFF
GO
SET IDENTITY_INSERT [dbo].[ContactType] ON 

INSERT [dbo].[ContactType] ([ContactTypeId], [Description]) VALUES (1, N'Home')
INSERT [dbo].[ContactType] ([ContactTypeId], [Description]) VALUES (2, N'Personal')
INSERT [dbo].[ContactType] ([ContactTypeId], [Description]) VALUES (3, N'Work')
SET IDENTITY_INSERT [dbo].[ContactType] OFF
GO
SET IDENTITY_INSERT [dbo].[CountLookup] ON 

INSERT [dbo].[CountLookup] ([CountLookupId], [Description]) VALUES (1, N'One')
INSERT [dbo].[CountLookup] ([CountLookupId], [Description]) VALUES (2, N'Two')
INSERT [dbo].[CountLookup] ([CountLookupId], [Description]) VALUES (3, N'Three')
SET IDENTITY_INSERT [dbo].[CountLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentParentCategoryLookup] ON 

INSERT [dbo].[DocumentParentCategoryLookup] ([ParentCategoryId], [Name]) VALUES (4, N'Provider')
INSERT [dbo].[DocumentParentCategoryLookup] ([ParentCategoryId], [Name]) VALUES (5, N'Lab Order')
SET IDENTITY_INSERT [dbo].[DocumentParentCategoryLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[EmploymentStatusLookup] ON 

INSERT [dbo].[EmploymentStatusLookup] ([EmploymentStatusLookupId], [Description]) VALUES (1, N'Active')
INSERT [dbo].[EmploymentStatusLookup] ([EmploymentStatusLookupId], [Description]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[EmploymentStatusLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[EthnicityLookup] ON 

INSERT [dbo].[EthnicityLookup] ([EthnicityLookupId], [Description]) VALUES (1, N'Ethnicity 1')
INSERT [dbo].[EthnicityLookup] ([EthnicityLookupId], [Description]) VALUES (2, N'Ethnicity 2')
INSERT [dbo].[EthnicityLookup] ([EthnicityLookupId], [Description]) VALUES (3, N'Ethnicity 3')
SET IDENTITY_INSERT [dbo].[EthnicityLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[HospitalizationStatusLookup] ON 

INSERT [dbo].[HospitalizationStatusLookup] ([HospitalizationStatusLookupId], [Description]) VALUES (1, N'Older than 12 months')
SET IDENTITY_INSERT [dbo].[HospitalizationStatusLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[InsurancePayerType] ON 

INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (1, N'COMMERCIAL')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (2, N'BLUE CROSS/BLUE SHIELD')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (3, N'MEDICAID')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (4, N'MEDICARE')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (5, N'CHAMPUS')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (6, N'WORKERS COMPENSATION')
INSERT [dbo].[InsurancePayerType] ([InsurancePayerTypeId], [PayerTypeDescription]) VALUES (7, N'AUTOMOBILE MEDICAL')
SET IDENTITY_INSERT [dbo].[InsurancePayerType] OFF
GO
SET IDENTITY_INSERT [dbo].[ICDCategory] ON 

INSERT [dbo].[ICDCategory] ([ICDCategoryId], [CategoryName], [CategoryCode]) VALUES (3, N'Certain infectious and parasitic diseases', N'A00-B99')
INSERT [dbo].[ICDCategory] ([ICDCategoryId], [CategoryName], [CategoryCode]) VALUES (4, N'Intestinal infectious diseases', N'C00-D99')
INSERT [dbo].[ICDCategory] ([ICDCategoryId], [CategoryName], [CategoryCode]) VALUES (5, N'Acute Chronic', N'E00-F99')
SET IDENTITY_INSERT [dbo].[ICDCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[LocationWorkConfigStatuses] ON 

INSERT [dbo].[LocationWorkConfigStatuses] ([LocationWorkConfigStatusId], [LocationWorkConfigStatusName]) VALUES (1, N'Open')
INSERT [dbo].[LocationWorkConfigStatuses] ([LocationWorkConfigStatusId], [LocationWorkConfigStatusName]) VALUES (2, N'Close')
SET IDENTITY_INSERT [dbo].[LocationWorkConfigStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[MaritalStatusLookup] ON 

INSERT [dbo].[MaritalStatusLookup] ([MaritalStatusLookupId], [Description]) VALUES (1, N'Single')
INSERT [dbo].[MaritalStatusLookup] ([MaritalStatusLookupId], [Description]) VALUES (2, N'Married')
SET IDENTITY_INSERT [dbo].[MaritalStatusLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[PacksLookup] ON 

INSERT [dbo].[PacksLookup] ([PacksLookupId], [Description]) VALUES (1, N'Active')
INSERT [dbo].[PacksLookup] ([PacksLookupId], [Description]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[PacksLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[PatientDisabilityStatusLookup] ON 

INSERT [dbo].[PatientDisabilityStatusLookup] ([PatientDisabilityStatusLookupId], [Description]) VALUES (1, N'Disabled')
INSERT [dbo].[PatientDisabilityStatusLookup] ([PatientDisabilityStatusLookupId], [Description]) VALUES (2, N'Enabled')
SET IDENTITY_INSERT [dbo].[PatientDisabilityStatusLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[PatientInsuranceType] ON 

INSERT [dbo].[PatientInsuranceType] ([PatientInsuranceTypeId], [Description]) VALUES (1, N'Primary Insurance')
INSERT [dbo].[PatientInsuranceType] ([PatientInsuranceTypeId], [Description]) VALUES (2, N'Secondary Insurance')
INSERT [dbo].[PatientInsuranceType] ([PatientInsuranceTypeId], [Description]) VALUES (3, N'Other Insurance')
INSERT [dbo].[PatientInsuranceType] ([PatientInsuranceTypeId], [Description]) VALUES (4, N'Rx Insurance')
SET IDENTITY_INSERT [dbo].[PatientInsuranceType] OFF
GO
SET IDENTITY_INSERT [dbo].[POS] ON 

INSERT [dbo].[POS] ([POSId], [Description]) VALUES (1, N'2-TELEHEALTH')
INSERT [dbo].[POS] ([POSId], [Description]) VALUES (2, N'3-SCHOOL')
INSERT [dbo].[POS] ([POSId], [Description]) VALUES (3, N'13-ASSISTED LIVING FACILITY')
SET IDENTITY_INSERT [dbo].[POS] OFF
GO
SET IDENTITY_INSERT [dbo].[PracticeAssignmentLookup] ON 

INSERT [dbo].[PracticeAssignmentLookup] ([PracticeAssignmentLookupId], [Description]) VALUES (1, N'One')
INSERT [dbo].[PracticeAssignmentLookup] ([PracticeAssignmentLookupId], [Description]) VALUES (2, N'Two')
SET IDENTITY_INSERT [dbo].[PracticeAssignmentLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[PracticeType] ON 

INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (1, N'Solo Practice')
INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (2, N'Group Practices')
INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (3, N'Employed Physician Practices')
INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (4, N'Direct Primary Care')
INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (5, N'Independent Contractor')
INSERT [dbo].[PracticeType] ([PracticeTypeId], [Description]) VALUES (6, N'Locum Tenens')
SET IDENTITY_INSERT [dbo].[PracticeType] OFF
GO
SET IDENTITY_INSERT [dbo].[PreferredCommsLookup] ON 

INSERT [dbo].[PreferredCommsLookup] ([PreferredCommsLookupId], [Description]) VALUES (1, N'Preferred Comms 1')
INSERT [dbo].[PreferredCommsLookup] ([PreferredCommsLookupId], [Description]) VALUES (2, N'Preferred Comms 2')
INSERT [dbo].[PreferredCommsLookup] ([PreferredCommsLookupId], [Description]) VALUES (3, N'Preferred Comms 3')
SET IDENTITY_INSERT [dbo].[PreferredCommsLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[PreferredLanguageLookup] ON 

INSERT [dbo].[PreferredLanguageLookup] ([PreferredLanguageLookupId], [Description]) VALUES (1, N'English')
INSERT [dbo].[PreferredLanguageLookup] ([PreferredLanguageLookupId], [Description]) VALUES (2, N'Spanish')
INSERT [dbo].[PreferredLanguageLookup] ([PreferredLanguageLookupId], [Description]) VALUES (3, N'French')
SET IDENTITY_INSERT [dbo].[PreferredLanguageLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[RaceLookup] ON 

INSERT [dbo].[RaceLookup] ([RaceLookupId], [Description]) VALUES (1, N'Race 1')
INSERT [dbo].[RaceLookup] ([RaceLookupId], [Description]) VALUES (2, N'Race 2')
INSERT [dbo].[RaceLookup] ([RaceLookupId], [Description]) VALUES (3, N'Race 3')
SET IDENTITY_INSERT [dbo].[RaceLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[ReferralProviderType] ON 

INSERT [dbo].[ReferralProviderType] ([ReferralProviderTypeId], [TypeDescription]) VALUES (1, N'ILS Practitioner')
INSERT [dbo].[ReferralProviderType] ([ReferralProviderTypeId], [TypeDescription]) VALUES (2, N'Surgeon')
INSERT [dbo].[ReferralProviderType] ([ReferralProviderTypeId], [TypeDescription]) VALUES (3, N'BLS Practitioner')
INSERT [dbo].[ReferralProviderType] ([ReferralProviderTypeId], [TypeDescription]) VALUES (4, N'Doctor')
SET IDENTITY_INSERT [dbo].[ReferralProviderType] OFF
GO
SET IDENTITY_INSERT [dbo].[ReminderDaysLookup] ON 

INSERT [dbo].[ReminderDaysLookup] ([ReminderDaysLookupId], [ReminderDaysDescription]) VALUES (1, N'Days')
INSERT [dbo].[ReminderDaysLookup] ([ReminderDaysLookupId], [ReminderDaysDescription]) VALUES (2, N'Weeks')
INSERT [dbo].[ReminderDaysLookup] ([ReminderDaysLookupId], [ReminderDaysDescription]) VALUES (1002, N'Months')
SET IDENTITY_INSERT [dbo].[ReminderDaysLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[ReminderType] ON 

INSERT [dbo].[ReminderType] ([ReminderTypeId], [Description]) VALUES (1, N'Call')
INSERT [dbo].[ReminderType] ([ReminderTypeId], [Description]) VALUES (2, N'Email')
INSERT [dbo].[ReminderType] ([ReminderTypeId], [Description]) VALUES (3, N'SMS')
SET IDENTITY_INSERT [dbo].[ReminderType] OFF
GO
SET IDENTITY_INSERT [dbo].[SchedulerStatus] ON 

INSERT [dbo].[SchedulerStatus] ([SchedulerStatusId], [SchedulerStatusName]) VALUES (1, N'Pending')
INSERT [dbo].[SchedulerStatus] ([SchedulerStatusId], [SchedulerStatusName]) VALUES (2, N'Approved')
INSERT [dbo].[SchedulerStatus] ([SchedulerStatusId], [SchedulerStatusName]) VALUES (3, N'Checkin')
INSERT [dbo].[SchedulerStatus] ([SchedulerStatusId], [SchedulerStatusName]) VALUES (4, N'Checkout')
INSERT [dbo].[SchedulerStatus] ([SchedulerStatusId], [SchedulerStatusName]) VALUES (5, N'Completed')
SET IDENTITY_INSERT [dbo].[SchedulerStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[SmokingStatusLookup] ON 

INSERT [dbo].[SmokingStatusLookup] ([SmokingStatusLookupId], [Description]) VALUES (1, N'Active')
INSERT [dbo].[SmokingStatusLookup] ([SmokingStatusLookupId], [Description]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[SmokingStatusLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([StatusId], [StatusName]) VALUES (1, N'Active')
INSERT [dbo].[Statuses] ([StatusId], [StatusName]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[SubscriberRelationshiplookup] ON 

INSERT [dbo].[SubscriberRelationshiplookup] ([SubscriberRelationshiplookupId], [Description]) VALUES (1, N'Father')
INSERT [dbo].[SubscriberRelationshiplookup] ([SubscriberRelationshiplookupId], [Description]) VALUES (2, N'Other')
SET IDENTITY_INSERT [dbo].[SubscriberRelationshiplookup] OFF
GO
SET IDENTITY_INSERT [dbo].[TaxType] ON 

INSERT [dbo].[TaxType] ([TaxTypeId], [Description]) VALUES (1, N'SSN')
INSERT [dbo].[TaxType] ([TaxTypeId], [Description]) VALUES (2, N'EIN')
INSERT [dbo].[TaxType] ([TaxTypeId], [Description]) VALUES (3, N'ITN')
INSERT [dbo].[TaxType] ([TaxTypeId], [Description]) VALUES (4, N'ATIN')
INSERT [dbo].[TaxType] ([TaxTypeId], [Description]) VALUES (5, N'PTIN')
SET IDENTITY_INSERT [dbo].[TaxType] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([UserTypeId], [Description]) VALUES (1, N'Administrator')
INSERT [dbo].[UserType] ([UserTypeId], [Description]) VALUES (2, N'Provider')
INSERT [dbo].[UserType] ([UserTypeId], [Description]) VALUES (3, N'Staff')
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
SET IDENTITY_INSERT [dbo].[WeekTypeLookup] ON 

INSERT [dbo].[WeekTypeLookup] ([WeekTypeId], [WeekTypeName]) VALUES (1, N'Week')
INSERT [dbo].[WeekTypeLookup] ([WeekTypeId], [WeekTypeName]) VALUES (2, N'Month')
INSERT [dbo].[WeekTypeLookup] ([WeekTypeId], [WeekTypeName]) VALUES (3, N'Year')
SET IDENTITY_INSERT [dbo].[WeekTypeLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkStatusLookup] ON 

INSERT [dbo].[WorkStatusLookup] ([WorkStatusLookupId], [Description]) VALUES (1, N'Active')
INSERT [dbo].[WorkStatusLookup] ([WorkStatusLookupId], [Description]) VALUES (2, N'InActive')
SET IDENTITY_INSERT [dbo].[WorkStatusLookup] OFF
GO


GO
SET IDENTITY_INSERT [dbo].[ProcedureGroup] ON 

INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (1, N'Evaluation and Management', N'99201 — 99499')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (2, N'Pathotogy and Laboratory', N'80047 — 89398')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (3, N'Surgery', N'10021 — 69990')
INSERT [dbo].[ProcedureGroup] ([ProcedureGroupId], [ProcedureGroupName], [ProcedureGroupCode]) VALUES (4, N'Macbctno', N'90281 — 99199')
SET IDENTITY_INSERT [dbo].[ProcedureGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[ProcedureSubGroup] ON 

INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (1, N'Office/other outpatient services', N'99201 – 99215', 1)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (2, N'Hospital observation services', N'99217 – 99220', 1)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (3, N'Office/other outpatient services', N'99201 – 99215', 2)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (4, N'Hospital observation services', N'99217 – 99220', 2)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (5, N'Hospital inpatient services', N'99221 – 99239', 2)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (6, N'Office/other outpatient services', N'99201 – 99215', 3)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (7, N'Hospital observation services', N'99217 – 99220', 3)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (8, N'Hospital inpatient services', N'99221 – 99239', 3)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (9, N'Office/other outpatient services', N'99201 – 99215', 4)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (10, N'Hospital observation services', N'99217 – 99220', 4)
INSERT [dbo].[ProcedureSubGroup] ([ProcedureSubGroupId], [ProcedureSubGroupName], [ProcedureSubGroupCode], [ProcedureGroupId]) VALUES (11, N'Hospital inpatient services', N'99221 – 99239', 4)
SET IDENTITY_INSERT [dbo].[ProcedureSubGroup] OFF
GO