using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Diagnosises.Commands
{
    public class DeleteDiagnosisCommand : IRequest<IResult>
    {
        public int DiagnosisId { get; set; }

        public class DeleteDiagnosisCommandHandler : IRequestHandler<DeleteDiagnosisCommand, IResult>
        {
            private readonly IDiagnosisRepository _diagnosisRepository;

            public DeleteDiagnosisCommandHandler(IDiagnosisRepository diagnosisRepository)
            {
                _diagnosisRepository = diagnosisRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var diagnosToDelete = await _diagnosisRepository.GetAsync(x => x.DiagnosisId == request.DiagnosisId);
                diagnosToDelete.IsDeleted = true;
                _diagnosisRepository.Update(diagnosToDelete);
                await _diagnosisRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
