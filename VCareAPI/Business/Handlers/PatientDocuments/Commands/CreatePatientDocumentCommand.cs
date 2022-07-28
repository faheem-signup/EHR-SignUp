using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentRepository;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Concrete.Role;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocuments.Commands
{

    public class CreatePatientDocumentCommand : IRequest<IResult>
    {
        public int PatientDocCateogryId { get; set; }
        public int UploadDocumentId { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public int PatientId { get; set; }

        public class CreatePatientDocumentCommandHandler : IRequestHandler<CreatePatientDocumentCommand, IResult>
        {
            private readonly IPatientDocumentRepository _patientDocumentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreatePatientDocumentCommandHandler(IPatientDocumentRepository patientDocumentRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDocumentRepository = patientDocumentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePatientDocumentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                PatientDocument patientDocumentObj = new PatientDocument
                {
                    PatientDocCateogryId = request.PatientDocCateogryId,
                    UploadDocumentId = request.UploadDocumentId,
                    DateOfVisit = request.DateOfVisit,
                    PatientId = request.PatientId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _patientDocumentRepository.Add(patientDocumentObj);
                await _patientDocumentRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

