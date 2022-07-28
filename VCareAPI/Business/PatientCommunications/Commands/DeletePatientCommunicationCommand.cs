using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientCommunicationRepository;
using Entities.Concrete.PatientCommunicationEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Business.Handlers.PatientCommunications.Commands
{
    public class DeletePatientCommunicationCommand : IRequest<IResult>
    {
        public int CommunicationId { get; set; }
        public class DeletePatientCommunicationCommandHandler : IRequestHandler<DeletePatientCommunicationCommand, IResult>
        {
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;

            public DeletePatientCommunicationCommandHandler(IPatientCommunicationRepository patientCommunicationRepository)
            {
                _patientCommunicationRepository = patientCommunicationRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientCommunicationCommand request, CancellationToken cancellationToken)
            {
                var patientCommunicationToDelete = await _patientCommunicationRepository.GetAsync(x => x.CommunicationId == request.CommunicationId && x.IsDeleted != true);
                patientCommunicationToDelete.IsDeleted = true;
                _patientCommunicationRepository.Update(patientCommunicationToDelete);
                await _patientCommunicationRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
