using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
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
    public class CreateBillingClaimCommand : IRequest<IResult>
    {
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

        public class CreateBillingClaimCommandHandler : IRequestHandler<CreateBillingClaimCommand, IResult>
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

            public CreateBillingClaimCommandHandler(IBillingClaimRepository billingClaimRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor,
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

            [ValidationAspect(typeof(ValidatorBillingClaim), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            [TransactionScopeAspectAsync]
            public async Task<IResult> Handle(CreateBillingClaimCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                BillingClaim BillingClaimObj = new BillingClaim
                {
                    PatientName = request.PatientName,
                    GenderId = request.GenderId,
                    DOB = request.DOB,
                    Address = request.Address,
                    InsuranceId = request.InsuranceId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _billingClaimRepository.Add(BillingClaimObj);
                await _billingClaimRepository.SaveChangesAsync();


                ClaimsBillingInfo ClaimsBillingInfoObj = new ClaimsBillingInfo
                {
                    BilledStatusId = request.BilledStatusId,
                    CurrentStatusId = request.CurrentStatusId,
                    PrimaryInsurance = request.PrimaryInsurance,
                    SecondaryInsurance = request.SecondaryInsurance,
                    LocationId = request.LocationId,
                    AttendingProvider = request.AttendingProvider,
                    SupervisingProvider = request.SupervisingProvider,
                    ReferringProvider = request.ReferringProvider,
                    BillingProvider = request.BillingProvider,
                    PlaceOfService = request.PlaceOfService,
                    BillingClaimsId = BillingClaimObj.BillingClaimsId
                };

                _claimsBillingInfoRepository.Add(ClaimsBillingInfoObj);
                await _billingClaimRepository.SaveChangesAsync();

                BillingClaimsDiagnosisCode BillingClaimsDiagnosisCodeObj = new BillingClaimsDiagnosisCode
                {
                    ICDCode1 = request.ICDCode1,
                    ICDCode2 = request.ICDCode2,
                    ICDCode3 = request.ICDCode3,
                    ICDCode4 = request.ICDCode4,
                    BillingClaimsId = BillingClaimObj.BillingClaimsId
                };

                _billingClaimsDiagnosisCodeRepository.Add(BillingClaimsDiagnosisCodeObj);
                await _billingClaimsDiagnosisCodeRepository.SaveChangesAsync();

                if(request.ServiceProfileId != null && request.ServiceProfileId != 0)
                {
                    BillingClaimsAdditionalInfo BillingClaimsAdditionalInfoObj = new BillingClaimsAdditionalInfo
                    {
                        ServiceProfileId = request.ServiceProfileId,
                        OrignalClaimNo = request.OrignalClaimNo,
                        IsCorrectedClaims = request.IsCorrectedClaims,
                        HCFABox19 = request.HCFABox19,
                        OnsetLMP = request.OnsetLMP,
                        LastXRay = request.LastXRay,
                        BillingClaimsId = BillingClaimObj.BillingClaimsId

                    };

                    _billingClaimsAdditionalInfoRepository.Add(BillingClaimsAdditionalInfoObj);
                    await _billingClaimsAdditionalInfoRepository.SaveChangesAsync();
                }
                
                if(request.EOBERAId != string.Empty)
                {
                    BillingClaimsPayerInfo BillingClaimsPayerInfoObj = new BillingClaimsPayerInfo
                    {
                        EOBERAId = request.EOBERAId,
                        EntryDate = request.EntryDate,
                        CheckNo = request.CheckNo,
                        CheckDate = request.CheckDate,
                        CheckAmount = request.CheckAmount,
                        PayerId = request.PayerId,
                        BillingClaimsId = BillingClaimObj.BillingClaimsId
                    };
                    _billingClaimsPayerInfoRepository.Add(BillingClaimsPayerInfoObj);
                    await _billingClaimsPayerInfoRepository.SaveChangesAsync();
                }

                if (request.billingClaimsPaymentDetailList != null && request.billingClaimsPaymentDetailList.Count() > 0)
                {
                    request.billingClaimsPaymentDetailList.ToList().ForEach(x => x.BillingClaimsId = BillingClaimObj.BillingClaimsId);

                    _billingClaimsPaymentDetailRepository.BulkInsert(request.billingClaimsPaymentDetailList);
                    await _billingClaimsPaymentDetailRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
