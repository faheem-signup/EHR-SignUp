using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete;
using Entities.Concrete.ReferralProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providers.Commands
{
    public class CreateReferralProviderCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Speciality { get; set; }
        public string TaxID { get; set; }
        public string License { get; set; }
        public string SSN { get; set; }
        public string NPI { get; set; }
        public string ContactPerson { get; set; }
        public string Comments { get; set; }
        public string ReferralProviderType { get; set; }
        public class CreateReferralProviderCommandHandler : IRequestHandler<CreateReferralProviderCommand, IResult>
        {
            private readonly IReferralProviderRepository _referralProviderRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateReferralProviderCommandHandler(IReferralProviderRepository referralProviderRepository, IMediator mediator, IMapper mapper)
            {
                _referralProviderRepository = referralProviderRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(ValidatorReferralProvider), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateReferralProviderCommand request, CancellationToken cancellationToken)
            {

                ReferralProvider referralProviderObj = new ReferralProvider
                {
                    FirstName = request.FirstName,
                    MI = request.MI,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    Phone = request.Phone,
                    Fax = request.Fax,
                    Email = request.Email,
                    Speciality = request.Speciality,
                    TaxID = request.TaxID,
                    License = request.License,
                    SSN = request.SSN,
                    NPI = request.NPI,
                    ContactPerson = request.ContactPerson,
                    Comments = request.Comments,
                    ReferralProviderType = request.ReferralProviderType
                };

                _referralProviderRepository.Add(referralProviderObj);
                await _referralProviderRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

