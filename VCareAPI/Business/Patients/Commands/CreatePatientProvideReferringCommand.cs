using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEmploymentsRepository;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using Entities.Concrete.PatientEmploymentEntity;
using Entities.Concrete.PatientProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Commands
{
    public class CreatePatientProvideReferringCommand : IRequest<IResult>
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string NPI { get; set; }
        public bool? ReferringProvider { get; set; }
        public bool? PCPName { get; set; }
        public bool? Pharmacy { get; set; }
        public bool? ReferringAgency { get; set; }
        public bool? AlcoholAgency { get; set; }
        public bool? ProbationOfficer { get; set; }
        public class CreatePatientProvideReferringCommandHandler : IRequestHandler<CreatePatientProvideReferringCommand, IResult>
        {
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreatePatientProvideReferringCommandHandler(IPatientProvideReferralRepository patientProvideReferralRepository, IMediator mediator, IMapper mapper)
            {
                _patientProvideReferralRepository = patientProvideReferralRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(ValidatorPatientProvideReferring), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientProvideReferringCommand request, CancellationToken cancellationToken)
            {
                PatientProvideReferring patientProvideReferringObj = new PatientProvideReferring();
                if (request.ReferringProvider == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.ReferringProvider == true);
                }
                else if (request.PCPName == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.PCPName == true);
                }
                else if (request.Pharmacy == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.Pharmacy == true);
                }
                else if (request.ReferringAgency == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.ReferringAgency == true);
                }
                else if (request.AlcoholAgency == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.AlcoholAgency == true);
                }
                else if (request.ProbationOfficer == true)
                {
                    patientProvideReferringObj = await _patientProvideReferralRepository.GetAsync(x => x.PatientId == request.PatientId && x.ProbationOfficer == true);
                }

                if (patientProvideReferringObj == null)
                {
                    PatientProvideReferring patientInfoDetailObj = new PatientProvideReferring
                    {
                        Name = request.Name,
                        Address = request.Address,
                        CellPhone = request.CellPhone,
                        Fax = request.Fax,
                        ContactPerson = request.ContactPerson,
                        NPI = request.NPI,
                        ReferringProvider = request.ReferringProvider,
                        PCPName = request.PCPName,
                        Pharmacy = request.Pharmacy,
                        ReferringAgency = request.ReferringAgency,
                        AlcoholAgency = request.AlcoholAgency,
                        ProbationOfficer = request.ProbationOfficer,
                        PatientId = request.PatientId,
                    };

                    _patientProvideReferralRepository.Add(patientInfoDetailObj);
                    await _patientProvideReferralRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                else
                {
                    patientProvideReferringObj.Name = request.Name;
                    patientProvideReferringObj.Address = request.Address;
                    patientProvideReferringObj.CellPhone = request.CellPhone;
                    patientProvideReferringObj.Fax = request.Fax;
                    patientProvideReferringObj.ContactPerson = request.ContactPerson;
                    patientProvideReferringObj.NPI = request.NPI;
                    patientProvideReferringObj.ReferringProvider = request.ReferringProvider;
                    patientProvideReferringObj.PCPName = request.PCPName;
                    patientProvideReferringObj.Pharmacy = request.Pharmacy;
                    patientProvideReferringObj.ReferringAgency = request.ReferringAgency;
                    patientProvideReferringObj.AlcoholAgency = request.AlcoholAgency;
                    patientProvideReferringObj.ProbationOfficer = request.ProbationOfficer;
                    patientProvideReferringObj.PatientId = request.PatientId;

                    _patientProvideReferralRepository.Update(patientProvideReferringObj);
                    await _patientProvideReferralRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Updated);
                }
            }
        }
    }
}
