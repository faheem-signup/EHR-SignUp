using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IClinicalTemplateDataRepository;
using Entities.Dtos.FormTemplateDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Business.Handlers.FormTemplates.Queries
{
    public class GetClinicalTemplateDataQuery : IRequest<IDataResult<ClinicalTemplateDataDto>>
    {
        public int TemplateId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int? AppointmentId { get; set; }
        public class GetClinicalTemplateDataQueryHandler : IRequestHandler<GetClinicalTemplateDataQuery, IDataResult<ClinicalTemplateDataDto>>
        {
            private readonly IClinicalTemplateDataRepository _clinicalTemplateDataRepository;

            public GetClinicalTemplateDataQueryHandler(IClinicalTemplateDataRepository clinicalTemplateDataRepository)
            {
                _clinicalTemplateDataRepository = clinicalTemplateDataRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<ClinicalTemplateDataDto>> Handle(GetClinicalTemplateDataQuery request, CancellationToken cancellationToken)
            {
                ClinicalTemplateDataDto clinicalTemplateDataObj = await _clinicalTemplateDataRepository.GetClinicalTemplateData(request.TemplateId, request.PatientId, request.ProviderId, request.AppointmentId);

                return new SuccessDataResult<ClinicalTemplateDataDto>(clinicalTemplateDataObj);
            }
        }
    }
}
