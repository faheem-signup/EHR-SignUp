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
    public class UpdatePracticeCommand : IRequest<IResult>
    {
        public int PracticeId { get; set; }
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
        public class UpdatePracticeCommandHandler : IRequestHandler<UpdatePracticeCommand, IResult>
        {
            private readonly IPracticesRepository _practicesRepository;
            private readonly IPracticePayersRepository _practicePayersRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdatePracticeCommandHandler(IPracticesRepository practicesRepository, IPracticePayersRepository practicePayersRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _practicesRepository = practicesRepository;
                _practicePayersRepository = practicePayersRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePractice), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePracticeCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var practiceObj = await _practicesRepository.GetAsync(x => x.PracticeId == request.PracticeId && x.IsDeleted != true);
                if (practiceObj != null)
                {
                    practiceObj.PracticeId = request.PracticeId;
                    practiceObj.LegalBusinessName = request.LegalBusinessName;
                    practiceObj.DBA = request.DBA;
                    practiceObj.FirstName = request.FirstName;
                    practiceObj.MiddleName = request.MiddleName;
                    practiceObj.LastName = request.LastName;
                    practiceObj.PhysicalAddress = request.PhysicalAddress;
                    practiceObj.City = request.City;
                    practiceObj.State = request.State;
                    practiceObj.ZIP = request.ZIP;
                    practiceObj.PhoneNumber = request.PhoneNumber;
                    practiceObj.FaxNumber = request.FaxNumber;
                    practiceObj.Website = request.Website;
                    practiceObj.OfficeEmail = request.OfficeEmail;
                    practiceObj.ContactPersonEmail = request.ContactPersonEmail;
                    practiceObj.ContactPerson = request.ContactPerson;
                    practiceObj.ContactPersonPhone = request.ContactPersonPhone;
                    practiceObj.CLIANumber = request.CLIANumber;
                    practiceObj.CLIATypeId = request.CLIATypeId;
                    practiceObj.LiabilityInsuranceID = request.LiabilityInsuranceID;
                    practiceObj.LiabilityInsuranceExpiryDate = request.LiabilityInsuranceExpiryDate;
                    practiceObj.LiabilityInsuranceCarrier = request.LiabilityInsuranceCarrier;
                    practiceObj.StateLicense = request.StateLicense;
                    practiceObj.StateLicenseExpiryDate = request.StateLicenseExpiryDate;
                    practiceObj.DeaNumber = request.DeaNumber;
                    practiceObj.DeaNumberExpiryDate = request.DeaNumberExpiryDate;
                    practiceObj.BillingAddress = request.BillingAddress;
                    practiceObj.BillingPhone = request.BillingPhone;
                    practiceObj.TaxTypeId = request.TaxTypeId;
                    practiceObj.BillingCity = request.BillingCity;
                    practiceObj.BillingState = request.BillingState;
                    practiceObj.BillingZIP = request.BillingZIP;
                    practiceObj.BillingFax = request.BillingFax;
                    practiceObj.BillingNPI = request.BillingNPI;
                    practiceObj.TaxIDNumber = request.TaxIDNumber;
                    practiceObj.AcceptAssignment = request.AcceptAssignment;
                    practiceObj.Taxonomy = request.Taxonomy;
                    practiceObj.StatusId = request.StatusId;
                    practiceObj.ModifiedBy = int.Parse(userId);
                    practiceObj.ModifiedDate = DateTime.Now;
                    practiceObj.PracticeTypeId = request.PracticeTypeId;

                    _practicesRepository.Update(practiceObj);
                    await _practicesRepository.SaveChangesAsync();

                    if (request.payer != null && request.payer.Count() > 0)
                    {
                        request.payer.ToList().ForEach(x => x.PracticeId = practiceObj.PracticeId);
                        var existingList = await _practicePayersRepository.GetListAsync(x => x.PracticeId == practiceObj.PracticeId);
                        _practicePayersRepository.BulkInsert(existingList, request.payer);
                        await _practicePayersRepository.SaveChangesAsync();
                    }

                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
