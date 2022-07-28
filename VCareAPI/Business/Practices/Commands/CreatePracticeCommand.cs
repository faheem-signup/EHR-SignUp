using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticePayersRepository;
using DataAccess.Abstract.IPracticesRepository;
using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Commands
{
    public class CreatePracticeCommand : IRequest<IDataResult<object>>
    {
        public string LegalBusinessName { get; set; }
        public string DBA { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhysicalAddress { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public string OfficeEmail { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public string CLIANumber { get; set; }
        public int? CLIATypeId { get; set; }
        public string LiabilityInsuranceID { get; set; }
        public DateTime? LiabilityInsuranceExpiryDate { get; set; }
        public string LiabilityInsuranceCarrier { get; set; }
        public string StateLicense { get; set; }
        public DateTime? StateLicenseExpiryDate { get; set; }
        public string DeaNumber { get; set; }
        public DateTime? DeaNumberExpiryDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingPhone { get; set; }
        public int? TaxTypeId { get; set; }
        public int? BillingCity { get; set; }
        public int? BillingState { get; set; }
        public int? BillingZIP { get; set; }
        public string BillingFax { get; set; }
        public string BillingNPI { get; set; }
        public string TaxIDNumber { get; set; }
        public int? AcceptAssignment { get; set; }
        public string Taxonomy { get; set; }
        public int? StatusId { get; set; }
        public int? PracticeTypeId { get; set; }

        public List<PracticePayer> payer { get; set; }
        public class CreatePracticeCommandHandler : IRequestHandler<CreatePracticeCommand, IDataResult<object>>
        {
            private readonly IPracticesRepository _practicesRepository;
            private readonly IPracticePayersRepository _practicePayersRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePracticeCommandHandler(IPracticesRepository practicesRepository, IPracticePayersRepository practicePayersRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _practicesRepository = practicesRepository;
                _practicePayersRepository = practicePayersRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPractice), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<object>> Handle(CreatePracticeCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Practice practiceObj = new Practice
                {
                    LegalBusinessName = request.LegalBusinessName,
                    DBA = request.DBA,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    PhysicalAddress = request.PhysicalAddress,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    PhoneNumber = request.PhoneNumber,
                    FaxNumber = request.FaxNumber,
                    Website = request.Website,
                    OfficeEmail = request.OfficeEmail,
                    ContactPersonEmail = request.ContactPersonEmail,
                    ContactPerson = request.ContactPerson,
                    ContactPersonPhone = request.ContactPersonPhone,
                    CLIANumber = request.CLIANumber,
                    CLIATypeId = request.CLIATypeId,
                    LiabilityInsuranceID = request.LiabilityInsuranceID,
                    LiabilityInsuranceExpiryDate = request.LiabilityInsuranceExpiryDate,
                    LiabilityInsuranceCarrier = request.LiabilityInsuranceCarrier,
                    StateLicense = request.StateLicense,
                    StateLicenseExpiryDate = request.StateLicenseExpiryDate,
                    DeaNumber = request.DeaNumber,
                    DeaNumberExpiryDate = request.DeaNumberExpiryDate,
                    BillingAddress = request.BillingAddress,
                    BillingPhone = request.BillingPhone,
                    TaxTypeId = request.TaxTypeId,
                    BillingCity = request.BillingCity,
                    BillingState = request.BillingState,
                    BillingZIP = request.BillingZIP,
                    BillingFax = request.BillingFax,
                    BillingNPI = request.BillingNPI,
                    TaxIDNumber = request.TaxIDNumber,
                    AcceptAssignment = request.AcceptAssignment,
                    Taxonomy = request.Taxonomy,
                    PracticeTypeId = request.PracticeTypeId,
                    StatusId = request.StatusId == null ? 1 : request.StatusId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };
                _practicesRepository.Add(practiceObj);
                await _practicesRepository.SaveChangesAsync();

                if (request.payer != null && request.payer.Count() > 0)
                {
                    request.payer.ToList().ForEach(x => x.PracticeId = practiceObj.PracticeId);

                    var existingList = await _practicePayersRepository.GetListAsync(x => x.PracticeId == practiceObj.PracticeId);
                    _practicePayersRepository.BulkInsert(existingList, request.payer);
                    await _practicePayersRepository.SaveChangesAsync();
                }

                return new SuccessDataResult<object>(practiceObj.PracticeId);
            }
        }
    }
}
