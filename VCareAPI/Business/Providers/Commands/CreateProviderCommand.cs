using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderBoardCertificationInfoRepository;
using DataAccess.Abstract.IProviderClinicalInfoRepository;
using DataAccess.Abstract.IProviderDEAInfoRepository;
using DataAccess.Abstract.IProviderRepository;
using DataAccess.Abstract.IProviderSecurityCheckInfoRepository;
using DataAccess.Abstract.IProviderStateLicenseInfoRepository;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using Entities.Concrete;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderClinicalInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.ProviderDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providers.Commands
{
    public class CreateProviderCommand : IRequest<IResult>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string ProviderEmail { get; set; }
        public string OfficeEmail { get; set; }
        public string PreviousName { get; set; }
        public string Degree { get; set; }
        public string NPINumber { get; set; }
        public string TaxonomyNo { get; set; }
        public string Specialty { get; set; }
        public string PLICarrierName { get; set; }
        public string PLINumber { get; set; }
        public DateTime? PLIExpiryDate { get; set; }
        public int? CAQHID { get; set; }
        public string CAQHUsername { get; set; }
        public string CAQHPassword { get; set; }

        public List<ProviderStateLicenseInfo> providerStateLicenseInfo { get; set; }
        public List<ProviderBoardCertificationInfo> providerBoardCertificationInfo { get; set; }
        public List<ProviderDEAInfo> providerDEAInfo { get; set; }
        public ProviderClinicalInfoDto providerClinicalInfo { get; set; }
        public List<GetProviderWorkConfigDto> providerWorkConfigList { get; set; }
        public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, IResult>
        {
            private readonly IProviderRepository _providerRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IProviderBoardCertificationInfoRepository _providerBoardCertificationInfoRepository;
            private readonly IProviderStateLicenseInfoRepository _providerStateLicenseInfoRepository;
            private readonly IProviderSecurityCheckInfoRepository _providerSecurityCheckInfoRepository;
            private readonly IProviderClinicalInfoRepository _providerClinicalInfoRepository;
            private readonly IProviderDEAInfoRepository _providerDEAInfoRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateProviderCommandHandler(
                IProviderRepository providerRepository,
                IMediator mediator,
                IMapper mapper,
                IProviderWorkConfigRepository providerWorkConfigRepository,
                IProviderBoardCertificationInfoRepository providerBoardCertificationInfoRepository,
                IProviderStateLicenseInfoRepository providerStateLicenseInfoRepository,
                IProviderSecurityCheckInfoRepository providerSecurityCheckInfoRepository,
                IProviderClinicalInfoRepository providerClinicalInfoRepository,
                IProviderDEAInfoRepository providerDEAInfoRepository,
                IHttpContextAccessor contextAccessor
                )
            {
                _providerRepository = providerRepository;
                _mediator = mediator;
                _mapper = mapper;
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _providerBoardCertificationInfoRepository = providerBoardCertificationInfoRepository;
                _providerDEAInfoRepository = providerDEAInfoRepository;
                _providerSecurityCheckInfoRepository = providerSecurityCheckInfoRepository;
                _providerClinicalInfoRepository = providerClinicalInfoRepository;
                _providerStateLicenseInfoRepository = providerStateLicenseInfoRepository;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorProvider), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            [TransactionScopeAspectAsync]
            public async Task<IResult> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
            {
                if (request.providerWorkConfigList.Count() > 0)
                {
                  var workConfigData=  request.providerWorkConfigList.FindAll(x => x.LocationId>0);
                    if(workConfigData == null)
                    {
                        return new SuccessDataResult<string>("Please Select Location In Hours Of Service");
                    }
                }
                    var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Provider providerObj = new Provider
                {
                    Title = request.Title,
                    FirstName = request.FirstName,
                    MI = request.MI,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    ProviderEmail = request.ProviderEmail,
                    OfficeEmail = request.OfficeEmail,
                    PreviousName = request.PreviousName,
                    Degree = request.Degree,
                    NPINumber = request.NPINumber,
                    TaxonomyNo = request.TaxonomyNo,
                    Specialty = request.Specialty,
                    PLICarrierName = request.PLICarrierName,
                    PLINumber = request.PLINumber,
                    PLIExpiryDate = request.PLIExpiryDate,
                    CAQHID = request.CAQHID,
                    CAQHUsername = request.CAQHUsername,
                    CAQHPassword = request.CAQHPassword,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                    SSN = request.SSN,
                    DOB = request.DOB,
                    CellNumber = request.CellNumber
                };

                _providerRepository.Add(providerObj);
                await _providerRepository.SaveChangesAsync();

                if (request.providerBoardCertificationInfo.Count() > 0)
                {
                    request.providerBoardCertificationInfo.ToList().ForEach(x => x.ProviderId = providerObj.ProviderId);

                    var list = await _providerBoardCertificationInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                    _providerBoardCertificationInfoRepository.BulkInsert(list, request.providerBoardCertificationInfo);
                    await _providerBoardCertificationInfoRepository.SaveChangesAsync();
                }
                if (request.providerStateLicenseInfo.Count() > 0)
                {
                    request.providerStateLicenseInfo.ToList().ForEach(x => x.ProviderId = providerObj.ProviderId);

                    var list = await _providerStateLicenseInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                    _providerStateLicenseInfoRepository.BulkInsert(list, request.providerStateLicenseInfo);
                    await _providerStateLicenseInfoRepository.SaveChangesAsync();
                }
                if (request.providerDEAInfo.Count() > 0)
                {
                    request.providerDEAInfo.ToList().ForEach(x => x.ProviderId = providerObj.ProviderId);

                    var list = await _providerDEAInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                    _providerDEAInfoRepository.BulkInsert(list, request.providerDEAInfo);
                    await _providerDEAInfoRepository.SaveChangesAsync();
                }

                if (request.providerClinicalInfo != null)
                {
                    ProviderClinicalInfo providerClinicalInfoObj = new ProviderClinicalInfo
                    {
                        LocationId = request.providerClinicalInfo.LocationId,
                        JoiningDate = request.providerClinicalInfo.JoiningDate,
                        ReHiringDate = request.providerClinicalInfo.ReHiringDate,
                        TerminationDate = request.providerClinicalInfo.TerminationDate,
                        ProviderServiceId = request.providerClinicalInfo.ProviderServiceId,
                        ChildAbuseCertificate = request.providerClinicalInfo.ChildAbuseCertificate,
                        ChildAbuseCertExpiryDate = request.providerClinicalInfo.ChildAbuseCertExpiryDate,
                        MandatedReporterCertificate = request.providerClinicalInfo.MandatedReporterCertificate,
                        MandatedReportExpiryDate = request.providerClinicalInfo.MandatedReportExpiryDate,
                        RoomId = request.providerClinicalInfo.RoomId,
                        CredentialedInsurances = request.providerClinicalInfo.CredentialedInsurances,
                        FlatRate = request.providerClinicalInfo.FlatRate,
                        ProcedureBasedRate = request.providerClinicalInfo.ProcedureBasedRate,
                        HourlyRate = request.providerClinicalInfo.HourlyRate,
                        ContinuingEducation = request.providerClinicalInfo.ContinuingEducation,
                        CompletedHours = request.providerClinicalInfo.CompletedHours,
                        ProviderId = providerObj.ProviderId,
                        CreatedBy = int.Parse(userId),
                        CreatedDate = DateTime.Now,
                        ModifiedBy = int.Parse(userId),
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    _providerClinicalInfoRepository.Add(providerClinicalInfoObj);
                    await _providerClinicalInfoRepository.SaveChangesAsync();

                    if (request.providerClinicalInfo.providerSecurityCheckInfo.Count() > 0)
                    {
                        request.providerClinicalInfo.providerSecurityCheckInfo.ToList().ForEach(x => x.ProviderClinicalInfoId = providerClinicalInfoObj.ProviderClinicalInfoId);

                        var list = await _providerSecurityCheckInfoRepository.GetListAsync(x => x.ProviderClinicalInfoId == providerClinicalInfoObj.ProviderClinicalInfoId);
                        _providerSecurityCheckInfoRepository.BulkInsert(list, request.providerClinicalInfo.providerSecurityCheckInfo);
                        await _providerSecurityCheckInfoRepository.SaveChangesAsync();
                    }
                }

                if (request.providerWorkConfigList.Count() > 0)
                {
                    request.providerWorkConfigList.ToList().ForEach(x => x.ProviderId = providerObj.ProviderId);

                    var list = await _providerWorkConfigRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);

                    List<ProviderWorkConfig> ProviderProviderWorkConfigListObj = request.providerWorkConfigList.ConvertAll(a =>
                    {
                        return new ProviderWorkConfig()
                        {
                            Id = a.Id,
                            ProviderId = a.ProviderId,
                            LocationId = a.LocationId,
                            Days = a.Days,
                            FirstShiftWorkFrom = a.FirstShiftWorkFrom,
                            FirstShiftWorkTo = a.FirstShiftWorkTo,
                            BreakShiftWorkFrom = a.BreakShiftWorkFrom,
                            BreakShiftWorkTo = a.BreakShiftWorkTo,
                            SlotSize = string.IsNullOrEmpty(a.SlotSize) ? TimeSpan.Parse("00:00:00") : TimeSpan.Parse(a.SlotSize),
                            IsBreak = a.IsBreak,
                        };
                    });
                    _providerWorkConfigRepository.BulkInsert(list, ProviderProviderWorkConfigListObj);
                    await _providerWorkConfigRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

