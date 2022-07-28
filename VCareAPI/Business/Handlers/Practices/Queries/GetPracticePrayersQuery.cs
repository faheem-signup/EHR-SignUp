using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticePayersRepository;
using Entities.Concrete.PracticePayersEntity;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticePayersQuery : IRequest<IDataResult<IEnumerable<PracticePayer>>>
    {
        public int PracticeId { get; set; }
        public class GetPracticePayersQueryHandler : IRequestHandler<GetPracticePayersQuery, IDataResult<IEnumerable<PracticePayer>>>
        {
            private readonly IPracticePayersRepository _practicePayersRepository;

            public GetPracticePayersQueryHandler(IPracticePayersRepository practicePayersRepository)
            {
                _practicePayersRepository = practicePayersRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PracticePayer>>> Handle(GetPracticePayersQuery request, CancellationToken cancellationToken)
            {
                var data = await _practicePayersRepository.GetPracticePayersByPracticeId(request.PracticeId);
                return new SuccessDataResult<IEnumerable<PracticePayer>>(data);

            }
        }
    }
}
