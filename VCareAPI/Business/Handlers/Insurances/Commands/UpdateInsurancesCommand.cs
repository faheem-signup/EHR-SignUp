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
    public class UpdateInsurancesCommand : IRequest<IResult>
    {
        public int InsuranceId { get; set; }
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
        public class UpdateInsurancesCommandHandler : IRequestHandler<UpdateInsurancesCommand, IResult>
        {
            private readonly IInsuranceRepository _insuranceRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateInsurancesCommandHandler(IInsuranceRepository insuranceRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _insuranceRepository = insuranceRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateInsurance), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateInsurancesCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var InsuranceObj = await _insuranceRepository.GetAsync(x => x.InsuranceId == request.InsuranceId && x.IsDeleted != true);
                if(InsuranceObj != null)
                {
                    InsuranceObj.PayerId = request.PayerId;
                    InsuranceObj.InsurancePayerTypeId = request.InsurancePayerTypeId;
                    InsuranceObj.Name = request.Name;
                    InsuranceObj.AddressLine1 = request.AddressLine1;
                    InsuranceObj.AddressLine2 = request.AddressLine2;
                    InsuranceObj.City = request.City;
                    InsuranceObj.State = request.State;
                    InsuranceObj.ZIP = request.ZIP;
                    InsuranceObj.Phone = request.Phone;
                    InsuranceObj.Fax = request.Fax;
                    InsuranceObj.Email = request.Email;
                    InsuranceObj.Website = request.Website;
                    InsuranceObj.DenialResponseLimit = request.DenialResponseLimit;
                    InsuranceObj.TimlyFilingLimit = request.TimlyFilingLimit;
                    InsuranceObj.BillingProvider = request.BillingProvider;
                    InsuranceObj.PracticeId = request.PracticeId;
                    InsuranceObj.ModifiedBy = int.Parse(userId);
                    InsuranceObj.ModifiedDate = DateTime.Now;
                    _insuranceRepository.Update(InsuranceObj);
                    await _insuranceRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
