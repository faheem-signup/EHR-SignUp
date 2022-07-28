using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocsFileUploadRepository;
using DataAccess.Abstract.IPatientDocumentRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocsFileUploads.Commands
{
    public class DeletePatientDocsFileUploadCommand : IRequest<IResult>
    {
        public int PatientDocumentId { get; set; }
        public class DeletePatientDocsFileUploadCommandHandler : IRequestHandler<DeletePatientDocsFileUploadCommand, IResult>
        {
            private readonly IPatientDocsFileUploadRepository _patientDocsFileUploadRepository;
            private readonly IPatientDocumentRepository _patientDocmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public DeletePatientDocsFileUploadCommandHandler(IPatientDocsFileUploadRepository patientDocsFileUploadRepository, IPatientDocumentRepository patientDocmentRepository, IHttpContextAccessor contextAccessor)
            {
                _patientDocsFileUploadRepository = patientDocsFileUploadRepository;
                _patientDocmentRepository = patientDocmentRepository;
                _contextAccessor = contextAccessor;
            }

          //  [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientDocsFileUploadCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var IsThereAnyDataPatDocsExist = await _patientDocmentRepository.GetAsync(x => x.PatientDocumentId == request.PatientDocumentId);

                var existingData = await _patientDocsFileUploadRepository.GetAsync(x => x.UploadDocumentId == IsThereAnyDataPatDocsExist.UploadDocumentId);

                if (IsThereAnyDataPatDocsExist != null)
                {
                    if (existingData != null && File.Exists(existingData.DocumentPath))
                    {
                        File.Delete(existingData.DocumentPath);
                    }

                    IsThereAnyDataPatDocsExist.IsDeleted = true;
                    IsThereAnyDataPatDocsExist.ModifiedBy = int.Parse(userId);
                    IsThereAnyDataPatDocsExist.ModifiedDate = DateTime.Now;
                    _patientDocmentRepository.Update(IsThereAnyDataPatDocsExist);
                    await _patientDocmentRepository.SaveChangesAsync();
                }


                //   await _patientDocsFileUploadRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
