using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEducationDocumentRepository;
using Entities.Concrete.PatientEducationEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientEducation.Commands
{
    public class CreatePatientEducationDocumentCommand : IRequest<IResult>
    {
        public string ICDCode { get; set; }
        public int PatientId { get; set; }

        public class CreatePatientEducationDocumentCommandHandler : IRequestHandler<CreatePatientEducationDocumentCommand, IResult>
        {
            private readonly IPatientEducationDocumentRepository _patientEducationDocumentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreatePatientEducationDocumentCommandHandler(IPatientEducationDocumentRepository patientEducationDocumentRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientEducationDocumentRepository = patientEducationDocumentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientEducationDocument), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientEducationDocumentCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (!string.IsNullOrEmpty(request.ICDCode))
                {
                    var client = new RestClient("https://connect.medlineplus.gov/application?mainSearchCriteria.v.c=" + request.ICDCode + "&mainSearchCriteria.v.cs=2.16.840.1.113883.6.90&mainSearchCriteria.v.dn=&informationRecipient.languageCode.c=en");
                    var clientrequest = new RestRequest("", Method.Get);
                    RestResponse response = await client.ExecuteAsync(clientrequest);
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        return new SuccessResult(Messages.NoIcdCodeFound);
                    }

                    PatientEducationDocument patientEducationDocumentObj = new PatientEducationDocument
                    {
                        ICDCode = request.ICDCode,
                        DocumentURL = response.Content,
                        DocumentName = "PatientEducationDocument-" + request.ICDCode,
                        PatientId = request.PatientId,
                    };

                    _patientEducationDocumentRepository.Add(patientEducationDocumentObj);
                    await _patientEducationDocumentRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }

                return new SuccessResult(Messages.FieldEmpty);
            }
        }
    }
}
