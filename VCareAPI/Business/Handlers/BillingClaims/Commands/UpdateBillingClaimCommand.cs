using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IBillingClaimRepository;
using DataAccess.Abstract.IBillingClaimsAdditionalInfoRepository;
using DataAccess.Abstract.IBillingClaimsDiagnosisCodeRepository;
using DataAccess.Abstract.IBillingClaimsPayerInfoRepository;
using DataAccess.Abstract.IBillingClaimsPaymentDetailRepository;
using DataAccess.Abstract.IClaimsBillingInfoRepository;
using Entities.Concrete.BillingClaim;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BillingClaims.Commands
{
    public class UpdateBillingClaimCommand : IRequest<IResult>
    {
        public int BillingClaimsId { get; set; }
        public string PatientName { get; set; }
        public int? GenderId { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string InsuranceId { get; set; }

        public int? BilledStatusId { get; set; }
        public int? CurrentStatusId { get; set; }
        public string PrimaryInsurance { get; set; }
        public string SecondaryInsurance { get; set; }
        public int? LocationId { get; set; }
        public string AttendingProvider { get; set; }
        public string SupervisingProvider { get; set; }
        public string ReferringProvider { get; set; }
        public string BillingProvider { get; set; }
        public string PlaceOfService { get; set; }

        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }

        public int? ServiceProfileId { get; set; }
        public string OrignalClaimNo { get; set; }
        public bool? IsCorrectedClaims { get; set; }
        public string HCFABox19 { get; set; }
        public string OnsetLMP { get; set; }
        public string LastXRay { get; set; }

        public string EOBERAId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string CheckNo { get; set; }
        public DateTime? CheckDate { get; set; }
        public decimal CheckAmount { get; set; }
        public int? PayerId { get; set; }

        public List<BillingClaimsPaymentDetail> billingClaimsPaymentDetailList { get; set; }

        public class UpdateBillingClaimCommandHandler : IRequestHandler<UpdateBillingClaimCommand, IResult>
        {
            private readonly IBillingClaimRepository _billingClaimRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IClaimsBillingInfoRepository _claimsBillingInfoRepository;
            private readonly IBillingClaimsDiagnosisCodeRepository _billingClaimsDiagnosisCodeRepository;
            private readonly IBillingClaimsAdditionalInfoRepository _billingClaimsAdditionalInfoRepository;
            private readonly IBillingClaimsPayerInfoRepository _billingClaimsPayerInfoRepository;
            private readonly IBillingClaimsPaymentDetailRepository _billingClaimsPaymentDetailRepository;

            public UpdateBillingClaimCommandHandler(IBillingClaimRepository billingClaimRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor,
                IClaimsBillingInfoRepository claimsBillingInfoRepository, IBillingClaimsDiagnosisCodeRepository billingClaimsDiagnosisCodeRepository, IBillingClaimsAdditionalInfoRepository billingClaimsAdditionalInfoRepository,
                IBillingClaimsPayerInfoRepository billingClaimsPayerInfoRepository, IBillingClaimsPaymentDetailRepository billingClaimsPaymentDetailRepository)
            {
                _billingClaimRepository = billingClaimRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _claimsBillingInfoRepository = claimsBillingInfoRepository;
                _billingClaimsDiagnosisCodeRepository = billingClaimsDiagnosisCodeRepository;
                _billingClaimsAdditionalInfoRepository = billingClaimsAdditionalInfoRepository;
                _billingClaimsPayerInfoRepository = billingClaimsPayerInfoRepository;
                _billingClaimsPaymentDetailRepository = billingClaimsPaymentDetailRepository;
            }

            [ValidationAspect(typeof(ValidatorUpdateBillingClaim), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateBillingClaimCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var billingClaimObj = await _billingClaimRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId && x.IsDeleted != true);

                if (billingClaimObj != null)
                {
                    billingClaimObj.PatientName = request.PatientName;
                    billingClaimObj.GenderId = request.GenderId;
                    billingClaimObj.DOB = request.DOB;
                    billingClaimObj.Address = request.Address;
                    billingClaimObj.InsuranceId = request.InsuranceId;
                    billingClaimObj.ModifiedBy = int.Parse(userId);
                    billingClaimObj.ModifiedDate = DateTime.Now;
                    _billingClaimRepository.Update(billingClaimObj);
                    await _billingClaimRepository.SaveChangesAsync();

                    var claimsBillingInfoObj = await _claimsBillingInfoRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId);

                    if (claimsBillingInfoObj != null)
                    {
                        claimsBillingInfoObj.BilledStatusId = request.BilledStatusId;
                        claimsBillingInfoObj.CurrentStatusId = request.CurrentStatusId;
                        claimsBillingInfoObj.PrimaryInsurance = request.PrimaryInsurance;
                        claimsBillingInfoObj.SecondaryInsurance = request.SecondaryInsurance;
                        claimsBillingInfoObj.LocationId = request.LocationId;
                        claimsBillingInfoObj.AttendingProvider = request.AttendingProvider;
                        claimsBillingInfoObj.SupervisingProvider = request.SupervisingProvider;
                        claimsBillingInfoObj.ReferringProvider = request.ReferringProvider;
                        claimsBillingInfoObj.BillingProvider = request.BillingProvider;
                        claimsBillingInfoObj.PlaceOfService = request.PlaceOfService;
                        _claimsBillingInfoRepository.Update(claimsBillingInfoObj);
                        await _claimsBillingInfoRepository.SaveChangesAsync();
                    }

                    var claimsDiagnosisObj = await _billingClaimsDiagnosisCodeRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId);
                        
                    if (claimsDiagnosisObj != null)
                    {    
                        claimsDiagnosisObj.ICDCode1 = request.ICDCode1;
                        claimsDiagnosisObj.ICDCode2 = request.ICDCode1;
                        claimsDiagnosisObj.ICDCode3 = request.ICDCode1;
                        claimsDiagnosisObj.ICDCode4 = request.ICDCode1;
                        
                        _billingClaimsDiagnosisCodeRepository.Update(claimsDiagnosisObj);
                        await _billingClaimsDiagnosisCodeRepository.SaveChangesAsync();
                    }

                    var billingClaimsAdditionalInfoObj = await _billingClaimsAdditionalInfoRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId);
                      
                    if (billingClaimsAdditionalInfoObj != null)
                    {   
                        billingClaimsAdditionalInfoObj.ServiceProfileId = request.ServiceProfileId;
                        billingClaimsAdditionalInfoObj.OrignalClaimNo = request.OrignalClaimNo;
                        billingClaimsAdditionalInfoObj.IsCorrectedClaims = request.IsCorrectedClaims;
                        billingClaimsAdditionalInfoObj.HCFABox19 = request.HCFABox19;
                        billingClaimsAdditionalInfoObj.OnsetLMP = request.OnsetLMP;
                        billingClaimsAdditionalInfoObj.LastXRay = request.LastXRay;
                        _billingClaimsAdditionalInfoRepository.Update(billingClaimsAdditionalInfoObj);
                        await _billingClaimsAdditionalInfoRepository.SaveChangesAsync();
                    }


                    var billingClaimsPayerInfoObj = await _billingClaimsPayerInfoRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId);
                        
                    if (billingClaimsPayerInfoObj != null)
                    { 
                        billingClaimsPayerInfoObj.EOBERAId = request.EOBERAId;
                        billingClaimsPayerInfoObj.EntryDate = request.EntryDate;
                        billingClaimsPayerInfoObj.CheckNo = request.CheckNo;
                        billingClaimsPayerInfoObj.CheckDate = request.CheckDate;
                        billingClaimsPayerInfoObj.CheckAmount = request.CheckAmount;
                        _billingClaimsPayerInfoRepository.Update(billingClaimsPayerInfoObj);
                        await _billingClaimsPayerInfoRepository.SaveChangesAsync();
                    }

                    if (request.billingClaimsPaymentDetailList != null && request.billingClaimsPaymentDetailList.Count() > 0)
                    {
                        request.billingClaimsPaymentDetailList.ToList().ForEach(x => x.BillingClaimsId = request.BillingClaimsId);
                        var existingList = await _billingClaimsPaymentDetailRepository.GetListAsync(x => x.BillingClaimsId == request.BillingClaimsId);

                        _billingClaimsPaymentDetailRepository.BulkInsertAndRemove(existingList,request.billingClaimsPaymentDetailList);
                        await _billingClaimsPaymentDetailRepository.SaveChangesAsync();
                    }
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
