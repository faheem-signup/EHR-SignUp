using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticesRepository;
using Entities.Concrete.InsuranceEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticePayersSearchById : IRequest<IDataResult<IEnumerable<Insurance>>>
    {
        public string PayerName { get; set; }
        public int? PayerID { get; set; }
        public class GetPracticePayersSearchByIdHandler : IRequestHandler<GetPracticePayersSearchById, IDataResult<IEnumerable<Insurance>>>
        {
            private readonly IPracticesRepository _practicesRepository;
            public GetPracticePayersSearchByIdHandler(IPracticesRepository practicesRepository)
            {
                _practicesRepository = practicesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Insurance>>> Handle(GetPracticePayersSearchById request, CancellationToken cancellationToken)
            {
                var list = await _practicesRepository.GetPracticePayerSearchParams(request.PayerName, request.PayerID);
                return new SuccessDataResult<IEnumerable<Insurance>>(list);
            }
        }
    }
}
