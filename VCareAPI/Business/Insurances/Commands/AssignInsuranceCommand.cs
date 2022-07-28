using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAssignInsuranceRepository;
using DataAccess.Abstract.IInsuranceRepository;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Commands
{
    public class AssignInsuranceCommand : IRequest<IResult>
    {
        public string InsuranceId { get; set; }
        public int? PracticeId { get; set; }

        public class AssignInsuranceCommandHandler : IRequestHandler<AssignInsuranceCommand, IResult>
        {
            private readonly IAssignInsuranceRepository _assignInsuranceRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public AssignInsuranceCommandHandler(IAssignInsuranceRepository assignInsuranceRepository, IMediator mediator, IMapper mapper)
            {
                _assignInsuranceRepository = assignInsuranceRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(AssignInsuranceCommand request, CancellationToken cancellationToken)
            {
                List<AssignPracticeInsurance> list = new List<AssignPracticeInsurance>();
                if (!string.IsNullOrEmpty(request.InsuranceId))
                {
                    var InsuranceIds = request.InsuranceId.Split(",");

                    foreach (var item in InsuranceIds)
                    {
                        AssignPracticeInsurance AssignInsuranceObj = new AssignPracticeInsurance
                        {
                            PracticeId = request.PracticeId,
                            InsuranceId = int.Parse(item)
                        };

                        list.Add(AssignInsuranceObj);
                    }
                }

                if (request.PracticeId != null && request.PracticeId > 0)
                {
                    var existingList = await _assignInsuranceRepository.GetListAsync(x => x.PracticeId == request.PracticeId);
                    _assignInsuranceRepository.BulkInsert(existingList, list);
                    await _assignInsuranceRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
