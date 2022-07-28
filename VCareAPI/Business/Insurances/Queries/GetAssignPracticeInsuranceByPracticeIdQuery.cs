using AutoMapper;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAssignInsuranceRepository;
using DataAccess.Abstract.IInsuranceRepository;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using Entities.Concrete.InsuranceEntity;
using Entities.Dtos.AssignPracticeInsurancesDto;
using Entities.Dtos.InsuranceDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Queries
{
    public class GetAssignPracticeInsuranceByPracticeIdQuery : BasePaginationQuery<IDataResult<IEnumerable<AssignInsuranceDto>>>
    {
        public int PracticeId { get; set; }
        public class GetAssignPracticeInsuranceByPracticeIdQueryHandler : IRequestHandler<GetAssignPracticeInsuranceByPracticeIdQuery, IDataResult<IEnumerable<AssignInsuranceDto>>>
        {
            private readonly IAssignInsuranceRepository _assignInsuranceRepository;
            private readonly IMapper _mapper;

            public GetAssignPracticeInsuranceByPracticeIdQueryHandler(IInsuranceRepository insuranceRepository, IAssignInsuranceRepository assignInsuranceRepository)
            {
                _assignInsuranceRepository = assignInsuranceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AssignInsuranceDto>>> Handle(GetAssignPracticeInsuranceByPracticeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _assignInsuranceRepository.GetAssignPracticeListAsync(request.PracticeId);
                return new PagedDataResult<IEnumerable<AssignInsuranceDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
