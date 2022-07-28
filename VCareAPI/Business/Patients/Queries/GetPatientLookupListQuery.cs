using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IPatientRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.LookUpDto;
using Entities.Dtos.PatientDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientLookupListQuery : IRequest<IDataResult<IEnumerable<LookupDto>>>
    {
        public class GetPatientLookupListQueryHandler : IRequestHandler<GetPatientLookupListQuery, IDataResult<IEnumerable<LookupDto>>>
        {
            private readonly IPatientRepository _patientRepository;

            public GetPatientLookupListQueryHandler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LookupDto>>> Handle(GetPatientLookupListQuery request, CancellationToken cancellationToken)
            {
                var patientLookupList = await _patientRepository.GetPatientLookupList();
                return new SuccessDataResult<IEnumerable<LookupDto>>(patientLookupList.ToList());

            }
        }
    }
}
