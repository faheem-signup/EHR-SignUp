using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderBoardCertificationInfoRepository;
using DataAccess.Abstract.IProviderClinicalInfoRepository;
using DataAccess.Abstract.IProviderDEAInfoRepository;
using DataAccess.Abstract.IProviderRepository;
using DataAccess.Abstract.IProviderSecurityCheckInfoRepository;
using DataAccess.Abstract.IProviderStateLicenseInfoRepository;
using DataAccess.Abstract.IProviderWorkConfigRepository;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
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
    public class UpdateProviderCommand : IRequest<IResult>
    {
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
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
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string CellNumber { get; set; }
        public List<GetProviderWorkConfigDto> providerWorkConfigList { get; set; }
        public List<ProviderBoardCertificationInfo> providerBoardCertificationInfo { get; set; }
        public List<ProviderStateLicenseInfo> providerStateLicenseInfo { get; set; }
        //public List<ProviderSecurityCheckInfo> providerSecurityCheckInfo { get; set; }
        public List<ProviderDEAInfo> providerDEAInfo { get; set; }
        public ProviderClinicalInfoDto providerClinicalInfo { get; set; }
        public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand, IResult>
        {
            private readonly IProviderRepository _providerRepository;
            private readonly IProviderWorkConfigRepository _providerWorkConfigRepository;
            private readonly IProviderBoardCertificationInfoRepository _providerBoardCertificationInfoRepository;
            private readonly IProviderStateLicenseInfoRepository _providerStateLicenseInfoRepository;
            private readonly IProviderSecurityCheckInfoRepository _providerSecurityCheckInfoRepository;
            private readonly IProviderClinicalInfoRepository _providerClinicalInfoRepository;
            private readonly IProviderDEAInfoRepository _providerDEAInfoRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateProviderCommandHandler(
                IProviderRepository providerRepository,
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
                _providerWorkConfigRepository = providerWorkConfigRepository;
                _providerBoardCertificationInfoRepository = providerBoardCertificationInfoRepository;
                _providerDEAInfoRepository = providerDEAInfoRepository;
                _providerSecurityCheckInfoRepository = providerSecurityCheckInfoRepository;
                _providerClinicalInfoRepository = providerClinicalInfoRepository;
                _providerStateLicenseInfoRepository = providerStateLicenseInfoRepository;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateProvider), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
            {
                if (request.providerWorkConfigList.Count() > 0)
                {
                    var workConfigData = request.providerWorkConfigList.FindAll(x => x.LocationId > 0);
                    if (workConfigData == null)
                    {
                        return new SuccessDataResult<string>("Please Select Location In Hours Of Service");
                    }
                } 
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var providerObj = await _providerRepository.GetAsync(x => x.ProviderId == request.ProviderId && x.IsDeleted != true);
                if(providerObj != null)
                {
                    providerObj.ProviderId = request.ProviderId;
                    providerObj.Title = request.Title;
                    providerObj.FirstName = request.FirstName;
                    providerObj.MI = request.MI;
                    providerObj.LastName = request.LastName;
                    providerObj.Address = request.Address;
                    providerObj.City = request.City;
                    providerObj.State = request.State;
                    providerObj.ZIP = request.ZIP;
                    providerObj.ProviderEmail = request.ProviderEmail;
                    providerObj.OfficeEmail = request.OfficeEmail;
                    providerObj.PreviousName = request.PreviousName;
                    providerObj.Degree = request.Degree;
                    providerObj.NPINumber = request.NPINumber;
                    providerObj.TaxonomyNo = request.TaxonomyNo;
                    providerObj.Specialty = request.Specialty;
                    providerObj.PLICarrierName = request.PLICarrierName;
                    providerObj.PLINumber = request.PLINumber;
                    providerObj.PLIExpiryDate = request.PLIExpiryDate;
                    providerObj.CAQHID = request.CAQHID;
                    providerObj.CAQHUsername = request.CAQHUsername;
                    providerObj.CAQHPassword = request.CAQHPassword;
                    providerObj.ModifiedBy = int.Parse(userId);
                    providerObj.ModifiedDate = DateTime.Now;
                    providerObj.SSN = request.SSN;
                    providerObj.DOB = request.DOB;
                    providerObj.CellNumber = request.CellNumber;

                    _providerRepository.Update(providerObj);
                    await _providerRepository.SaveChangesAsync();

                    if (request.providerBoardCertificationInfo != null)
                    {
                        if (request.providerBoardCertificationInfo.Count() > 0)
                        {
                            request.providerBoardCertificationInfo.ToList().ForEach(x => { x.ProviderId = providerObj.ProviderId; x.BoardCertificationId = 0; });

                            var list = await _providerBoardCertificationInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                            _providerBoardCertificationInfoRepository.BulkInsert(list, request.providerBoardCertificationInfo);
                            await _providerBoardCertificationInfoRepository.SaveChangesAsync();
                        }
                    }
                    if (request.providerStateLicenseInfo != null)
                    {
                        if (request.providerStateLicenseInfo.Count() > 0)
                        {
                            request.providerStateLicenseInfo.ToList().ForEach(x => { x.ProviderId = providerObj.ProviderId; x.StateLicenseId = 0; });

                            var list = await _providerStateLicenseInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                            _providerStateLicenseInfoRepository.BulkInsert(list, request.providerStateLicenseInfo);
                            await _providerStateLicenseInfoRepository.SaveChangesAsync();
                        }
                    }
                    if (request.providerDEAInfo != null)
                    {
                        if (request.providerDEAInfo.Count() > 0)
                        {
                            request.providerDEAInfo.ToList().ForEach(x => { x.ProviderId = providerObj.ProviderId; x.DEAInfoId = 0; });

                            var list = await _providerDEAInfoRepository.GetListAsync(x => x.ProviderId == providerObj.ProviderId);
                            _providerDEAInfoRepository.BulkInsert(list, request.providerDEAInfo);
                            await _providerDEAInfoRepository.SaveChangesAsync();
                        }
                    }
                    if (request.providerClinicalInfo != null)
                    {
                        var providerClinicalInfoObj = await _providerClinicalInfoRepository.GetAsync(x => x.ProviderId == request.ProviderId && x.IsDeleted != true);
                        if(providerClinicalInfoObj != null)
                        {
                            providerClinicalInfoObj.LocationId = request.providerClinicalInfo.LocationId;
                            providerClinicalInfoObj.JoiningDate = request.providerClinicalInfo.JoiningDate;
                            providerClinicalInfoObj.ReHiringDate = request.providerClinicalInfo.ReHiringDate;
                            providerClinicalInfoObj.TerminationDate = request.providerClinicalInfo.TerminationDate;
                            providerClinicalInfoObj.ProviderServiceId = request.providerClinicalInfo.ProviderServiceId;
                            providerClinicalInfoObj.ChildAbuseCertificate = request.providerClinicalInfo.ChildAbuseCertificate;
                            providerClinicalInfoObj.ChildAbuseCertExpiryDate = request.providerClinicalInfo.ChildAbuseCertExpiryDate;
                            providerClinicalInfoObj.MandatedReporterCertificate = request.providerClinicalInfo.MandatedReporterCertificate;
                            providerClinicalInfoObj.MandatedReportExpiryDate = request.providerClinicalInfo.MandatedReportExpiryDate;
                            providerClinicalInfoObj.RoomId = request.providerClinicalInfo.RoomId;
                            providerClinicalInfoObj.CredentialedInsurances = request.providerClinicalInfo.CredentialedInsurances;
                            providerClinicalInfoObj.FlatRate = request.providerClinicalInfo.FlatRate;
                            providerClinicalInfoObj.ProcedureBasedRate = request.providerClinicalInfo.ProcedureBasedRate;
                            providerClinicalInfoObj.HourlyRate = request.providerClinicalInfo.HourlyRate;
                            providerClinicalInfoObj.ContinuingEducation = request.providerClinicalInfo.ContinuingEducation;
                            providerClinicalInfoObj.CompletedHours = request.providerClinicalInfo.CompletedHours;
                            providerClinicalInfoObj.ProviderId = providerObj.ProviderId;
                            providerClinicalInfoObj.ModifiedBy = int.Parse(userId);
                            providerClinicalInfoObj.ModifiedDate = DateTime.Now;

                            _providerClinicalInfoRepository.Update(providerClinicalInfoObj);
                            await _providerClinicalInfoRepository.SaveChangesAsync();

                            if (request.providerClinicalInfo.providerSecurityCheckInfo != null)
                            {
                                if (request.providerClinicalInfo.providerSecurityCheckInfo.Count() > 0)
                                {
                                    request.providerClinicalInfo.providerSecurityCheckInfo.ToList().ForEach(x => { x.ProviderClinicalInfoId = providerClinicalInfoObj.ProviderClinicalInfoId; x.SecurityCheckId = 0; });

                                    var list = await _providerSecurityCheckInfoRepository.GetListAsync(x => x.ProviderClinicalInfoId == providerClinicalInfoObj.ProviderClinicalInfoId);
                                    _providerSecurityCheckInfoRepository.BulkInsert(list, request.providerClinicalInfo.providerSecurityCheckInfo);
                                    await _providerSecurityCheckInfoRepository.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            if (request.providerClinicalInfo != null)
                            {
                                ProviderClinicalInfo addProviderClinicalInfoObj = new ProviderClinicalInfo
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

                                _providerClinicalInfoRepository.Add(addProviderClinicalInfoObj);
                                await _providerClinicalInfoRepository.SaveChangesAsync();

                                if (request.providerClinicalInfo.providerSecurityCheckInfo != null)
                                {
                                    if (request.providerClinicalInfo.providerSecurityCheckInfo.Count() > 0)
                                    {
                                        request.providerClinicalInfo.providerSecurityCheckInfo.ToList().ForEach(x => { x.ProviderClinicalInfoId = addProviderClinicalInfoObj.ProviderClinicalInfoId; x.SecurityCheckId = 0; });

                                        var list = await _providerSecurityCheckInfoRepository.GetListAsync(x => x.ProviderClinicalInfoId == addProviderClinicalInfoObj.ProviderClinicalInfoId);
                                        _providerSecurityCheckInfoRepository.BulkInsert(list, request.providerClinicalInfo.providerSecurityCheckInfo);
                                        await _providerSecurityCheckInfoRepository.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                    if (request.providerWorkConfigList != null)
                    {
                        if (request.providerWorkConfigList.Count() > 0)
                        {
                            request.providerWorkConfigList.ToList().ForEach(x => x.ProviderId = providerObj.ProviderId);
                            var list = await _providerWorkConfigRepository.GetListAsync(x => x.ProviderId == request.ProviderId);

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
                    }
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
