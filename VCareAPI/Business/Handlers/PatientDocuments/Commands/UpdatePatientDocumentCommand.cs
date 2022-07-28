using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentRepository;
using Entities.Concrete;
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
    public class UpdatePatientDocumentCommand : IRequest<IResult>
    {
        public int PatientDocumentId { get; set; }
        public int PatientDocCateogryId { get; set; }
        public int UploadDocumentId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int PatientId { get; set; }
        public class UpdatePatientDocumentCommandHandler : IRequestHandler<UpdatePatientDocumentCommand, IResult>
        {
            private readonly IPatientDocumentRepository _patientDocumentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            public UpdatePatientDocumentCommandHandler(IPatientDocumentRepository patientDocumentRepository, IHttpContextAccessor contextAccessor)
            {
                _patientDocumentRepository = patientDocumentRepository;
                _contextAccessor = contextAccessor;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientDocumentCommand request, CancellationToken cancellationToken)
            {
                var existingPatientDocument = await _patientDocumentRepository.GetAsync(x => x.PatientDocumentId == request.PatientDocumentId);

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (existingPatientDocument != null)
                {
                    existingPatientDocument.PatientDocCateogryId = request.PatientDocCateogryId;
                    existingPatientDocument.UploadDocumentId = request.UploadDocumentId;
                    existingPatientDocument.DateOfVisit = request.DateOfVisit;
                    existingPatientDocument.PatientId = request.PatientId;
                    existingPatientDocument.ModifiedBy = int.Parse(userId);
                    existingPatientDocument.ModifiedDate = DateTime.Now;
                }
                _patientDocumentRepository.Update(existingPatientDocument);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
