using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IClinicalTemplateDataRepository;
using Entities.Concrete.ClinicalTemplateDataEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Commands
{
    public class UpsertClinicalTemplateDataCommand : IRequest<IResult>
    {
        public int TemplateId { get; set; }

        public int PatientId { get; set; }

        public int? ProviderId { get; set; }

        public int? AppointmentId { get; set; }

        public string FormData { get; set; }

        public class UpsertClinicalTemplateDataCommandHandler : IRequestHandler<UpsertClinicalTemplateDataCommand, IResult>
        {
            private readonly IClinicalTemplateDataRepository _clinicalTemplateDataRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpsertClinicalTemplateDataCommandHandler(IClinicalTemplateDataRepository clinicalTemplateDataRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _clinicalTemplateDataRepository = clinicalTemplateDataRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpsertClinicalTemplateData), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpsertClinicalTemplateDataCommand request, CancellationToken cancellationToken)
            {
                ClinicalTemplateData clinicalTemplateDataObjObj = new ClinicalTemplateData();
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (request.AppointmentId != null && request.AppointmentId > 0 && request.ProviderId != null && request.ProviderId > 0)
                {
                    clinicalTemplateDataObjObj = await _clinicalTemplateDataRepository.GetAsync(x => x.TemplateId == request.TemplateId && x.PatientId == request.PatientId && x.ProviderId == request.ProviderId && x.AppointmentId == request.AppointmentId);
                }
                else if (request.AppointmentId != null && request.AppointmentId > 0 && (request.ProviderId == null || request.ProviderId == 0))
                {
                    clinicalTemplateDataObjObj = await _clinicalTemplateDataRepository.GetAsync(x => x.TemplateId == request.TemplateId && x.PatientId == request.PatientId && x.AppointmentId == request.AppointmentId);
                }
                else if (request.ProviderId != null && request.ProviderId > 0)
                {
                    clinicalTemplateDataObjObj = await _clinicalTemplateDataRepository.GetAsync(x => x.TemplateId == request.TemplateId && x.PatientId == request.PatientId && x.ProviderId == request.ProviderId);
                }
                else
                {
                    clinicalTemplateDataObjObj = await _clinicalTemplateDataRepository.GetAsync(x => x.TemplateId == request.TemplateId && x.PatientId == request.PatientId);
                }

                if (clinicalTemplateDataObjObj == null)
                {
                    ClinicalTemplateData addClinicalTemplateDataObj = new ClinicalTemplateData
                    {
                        TemplateId = request.TemplateId,
                        PatientId = request.PatientId,
                        ProviderId = request.ProviderId,
                        AppointmentId = request.AppointmentId,
                        FormData = request.FormData,
                        CreatedDate = DateTime.Now,
                    };
                    _clinicalTemplateDataRepository.Add(addClinicalTemplateDataObj);
                    await _clinicalTemplateDataRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                else
                {
                    clinicalTemplateDataObjObj.TemplateId = request.TemplateId;
                    clinicalTemplateDataObjObj.PatientId = request.PatientId;
                    clinicalTemplateDataObjObj.FormData = request.FormData;
                    clinicalTemplateDataObjObj.AppointmentId = request.AppointmentId;
                    if (request.ProviderId != null && request.ProviderId > 0)
                    {
                        clinicalTemplateDataObjObj.ProviderId = request.ProviderId;
                    }

                    _clinicalTemplateDataRepository.Update(clinicalTemplateDataObjObj);
                    await _clinicalTemplateDataRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Updated);
                }
            }
        }
    }
}
