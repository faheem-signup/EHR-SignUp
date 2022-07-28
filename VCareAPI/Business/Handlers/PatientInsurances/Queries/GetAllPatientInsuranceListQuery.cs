using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Dtos.PatientInsurancesDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Queries
{
    public class GetAllPatientInsuranceListQuery : IRequest<IDataResult<IEnumerable<PatientInsurance>>>
    {
        public int PatientId { get; set; }

        public class GetAllPatientInsuranceListQueryHandler : IRequestHandler<GetAllPatientInsuranceListQuery, IDataResult<IEnumerable<PatientInsurance>>>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IMapper _mapper;

            public GetAllPatientInsuranceListQueryHandler(IPatientInsuranceRepository patientInsuranceRepository, IMapper mapper)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientInsurance>>> Handle(GetAllPatientInsuranceListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientInsuranceRepository.GetListAsync(x => x.PatientId == request.PatientId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.PatientInsuranceId).ToList();
                }

                return new SuccessDataResult<IEnumerable<PatientInsurance>>(list.ToList());
            }
        }
    }
}
