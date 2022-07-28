using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using Entities.Concrete.PatientProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientProvideReferringById : IRequest<IDataResult<PatientProvideReferring>>
    {
        public int PatientId { get; set; }
        public bool? ReferringProvider { get; set; }
        public bool? PCPName { get; set; }
        public bool? Pharmacy { get; set; }
        public bool? ReferringAgency { get; set; }
        public bool? AlcoholAgency { get; set; }
        public bool? ProbationOfficer { get; set; }

        public class GetPatientProvideReferringByIdHandler : IRequestHandler<GetPatientProvideReferringById, IDataResult<PatientProvideReferring>>
        {
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetPatientProvideReferringByIdHandler(
                IPatientProvideReferralRepository patientProvideReferralRepository,
                IMediator mediator,
                IMapper mapper)
            {
                _patientProvideReferralRepository = patientProvideReferralRepository;
            }

            public async Task<IDataResult<PatientProvideReferring>> Handle(GetPatientProvideReferringById request, CancellationToken cancellationToken)
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

                return new SuccessDataResult<PatientProvideReferring>(patientProvideReferringObj);
            }
        }
    }
}
