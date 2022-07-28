using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientNotesRepository;
using DataAccess.Abstract.IPatientRepository;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientNotesEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientNotesListQuery : IRequest<IDataResult<IEnumerable<PatientNotes>>>
    {
        public int PatientId { get; set; }
        public bool? IsDemographic { get; set; }
        public bool? IsAdditionalInfo { get; set; }

        public class GetPatientNotesListQueryHandler : IRequestHandler<GetPatientNotesListQuery, IDataResult<IEnumerable<PatientNotes>>>
        {
            private readonly IPatientNotesRepository _patientNotesRepository;

            public GetPatientNotesListQueryHandler(IPatientNotesRepository patientNotesRepository)
            {
                _patientNotesRepository = patientNotesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientNotes>>> Handle(GetPatientNotesListQuery request, CancellationToken cancellationToken)
            {
                if (request.IsDemographic != null && request.IsDemographic == true)
                {
                    var patientNotes = await _patientNotesRepository.GetListAsync(x => x.PatientId == request.PatientId && x.IsDemographic == true);
                    return new SuccessDataResult<IEnumerable<PatientNotes>>(patientNotes);
                }
                else if (request.IsAdditionalInfo != null && request.IsAdditionalInfo == true)
                {
                    var patientNotes = await _patientNotesRepository.GetListAsync(x => x.PatientId == request.PatientId && x.IsAdditionalInfo == true);
                    return new SuccessDataResult<IEnumerable<PatientNotes>>(patientNotes);
                }
                else
                {
                    var patientNotes = await _patientNotesRepository.GetListAsync(x => x.PatientId == request.PatientId);
                    return new SuccessDataResult<IEnumerable<PatientNotes>>(patientNotes);
                }
            }
        }
    }
}
