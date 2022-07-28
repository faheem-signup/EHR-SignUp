using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IInsuranceRepository;
using Entities.Concrete.InsuranceEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Commands
{
    public class CreateInsurancesCommand : IRequest<IResult>
    {
        public int? PayerId { get; set; }
        public int? InsurancePayerTypeId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int? DenialResponseLimit { get; set; }
        public int? TimlyFilingLimit { get; set; }
        public int? BillingProvider { get; set; }
        public int? PracticeId { get; set; }
        public class CreateInsurancesCommandHandler : IRequestHandler<CreateInsurancesCommand, IResult>
        {
            private readonly IInsuranceRepository _insuranceRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateInsurancesCommandHandler(IInsuranceRepository insuranceRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _insuranceRepository = insuranceRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorInsurance), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateInsurancesCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Insurance InsuranceObj = new Insurance
                {
                    PayerId = request.PayerId,
                    InsurancePayerTypeId = request.InsurancePayerTypeId,
                    Name = request.Name,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    Phone = request.Phone,
                    Fax = request.Fax,
                    Email = request.Email,
                    Website = request.Website,
                    DenialResponseLimit = request.DenialResponseLimit,
                    TimlyFilingLimit = request.TimlyFilingLimit,
                    BillingProvider = request.BillingProvider,
                    PracticeId = request.PracticeId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _insuranceRepository.Add(InsuranceObj);
                await _insuranceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
