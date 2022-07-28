using AutoMapper;
using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientGuardiansRepository;
using DataAccess.Abstract.IPatientNotesRepository;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Services.UploadDocument;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientGuardiansEntity;
using Entities.Concrete.PatientNotesEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Commands
{
    public class CreatePatientNotesCommand : IRequest<IResult>
    {
        public int? PatientId { get; set; }
        public string NotesDescription { get; set; }
        public bool IsDemographic { get; set; }
        public bool IsAdditionalInfo { get; set; }
        public class CreatePatientNotesCommandHandler : IRequestHandler<CreatePatientNotesCommand, IResult>
        {
            private readonly IPatientNotesRepository _patientNotesRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientNotesCommandHandler(IPatientNotesRepository patientNotesRepository,
                IPatientGuardiansRepository patientGuardiansRepository,
                IMediator mediator,
                IMapper mapper,
                IUploadFile uploadFile,
                IHttpContextAccessor contextAccessor)
            {
                _patientNotesRepository = patientNotesRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientNotesCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                PatientNotes patientNotesObj = new PatientNotes
                {
                    PatientId = request.PatientId,
                    NotesDescription = request.NotesDescription,
                    IsAdditionalInfo = request.IsAdditionalInfo,
                    IsDemographic = request.IsDemographic,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now
                };

                _patientNotesRepository.Add(patientNotesObj);
                await _patientNotesRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);

            }

        }
    }
}
