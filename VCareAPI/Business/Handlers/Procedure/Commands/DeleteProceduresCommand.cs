using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IProceduresRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Commands
{
    public class DeleteProceduresCommand : IRequest<IResult>
    {
        public int ProcedureId { get; set; }
        public class DeleteProcedureCommandHandler : IRequestHandler<DeleteProceduresCommand, IResult>
        {
            private readonly IProceduresRepository _proceduresRepository;

            public DeleteProcedureCommandHandler(IProceduresRepository proceduresRepository)
            {
                _proceduresRepository = proceduresRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteProceduresCommand request, CancellationToken cancellationToken)
            {
                var procedureToDelete = await _proceduresRepository.GetAsync(x => x.ProcedureId == request.ProcedureId);

                _proceduresRepository.Delete(procedureToDelete);
                await _proceduresRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
