using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocuments.Commands
{
    public class DeletePatientDocumentCommand : IRequest<IResult>
    {
        public int PatientDocumentId { get; set; }
        public class DeletePatientDocumentCommandHandler : IRequestHandler<DeletePatientDocumentCommand, IResult>
        {
            private readonly IPatientDocumentRepository _patientDocumentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            public DeletePatientDocumentCommandHandler(IPatientDocumentRepository patientDocumentRepository, IHttpContextAccessor contextAccessor)
            {
                _patientDocumentRepository = patientDocumentRepository;
                _contextAccessor = contextAccessor;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientDocumentCommand request, CancellationToken cancellationToken)
            {
                var existingPatientDocument = await _patientDocumentRepository.GetAsync(x => x.PatientDocumentId == request.PatientDocumentId);

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (existingPatientDocument != null)
                {
                    existingPatientDocument.IsDeleted = true;
                    existingPatientDocument.ModifiedBy = int.Parse(userId);
                    existingPatientDocument.ModifiedDate = DateTime.Now;
                }
                _patientDocumentRepository.Update(existingPatientDocument);
                await _patientDocumentRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
