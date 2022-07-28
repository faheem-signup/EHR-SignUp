using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticePayersRepository;
using DataAccess.Abstract.IPracticesRepository;
using Entities.Concrete.PracticePayersEntity;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.PracticeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticeByIdQuery : IRequest<IDataResult<PracticePayerDto>>
    {
        public int PracticeId { get; set; }
        public class GetPracticeByIdQueryHandler : IRequestHandler<GetPracticeByIdQuery, IDataResult<PracticePayerDto>>
        {
            private readonly IPracticesRepository _practicesRepository;
            private readonly IPracticePayersRepository _practicePayersRepository;
            public GetPracticeByIdQueryHandler(IPracticesRepository practicesRepository, IPracticePayersRepository practicePayersRepository)
            {
                _practicesRepository = practicesRepository;
                _practicePayersRepository = practicePayersRepository;
            }

            public async Task<IDataResult<PracticePayerDto>> Handle(GetPracticeByIdQuery request, CancellationToken cancellationToken)
            {
                var practiceObj = await _practicesRepository.GetAsync(x => x.PracticeId == request.PracticeId);
                var payers = await _practicesRepository.GetPracticePayersById(request.PracticeId);

                PracticePayerDto practiceDto = new PracticePayerDto();
                practiceDto.practice = practiceObj;
                practiceDto.practicePayers = payers;

                return new SuccessDataResult<PracticePayerDto>(practiceDto);
            }
        }
    }
}
