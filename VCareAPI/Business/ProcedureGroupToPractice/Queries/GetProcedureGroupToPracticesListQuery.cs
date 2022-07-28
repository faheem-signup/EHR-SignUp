using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IICDToPracticesRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Dtos.PracticeDto;
using Entities.Concrete.ProcedureGroupToPracticesEntity;
using DataAccess.Abstract.IProcedureGroupToPracticesRepository;

namespace Business.Handlers.ProcedureGroupToPractice.Queries
{
    public class GetProcedureGroupToPracticesListQuery : IRequest<IDataResult<IEnumerable<ProcedureGroupToPractices>>>
    {
        public int PracticeId { get; set; }
        public class GetProcedureGroupToPracticesListQueryHandler : IRequestHandler<GetProcedureGroupToPracticesListQuery, IDataResult<IEnumerable<ProcedureGroupToPractices>>>
        {
            private readonly IProcedureGroupToPracticesRepository _procedureGroupToPracticesRepository;
            private readonly IMapper _mapper;
            public GetProcedureGroupToPracticesListQueryHandler(IProcedureGroupToPracticesRepository procedureGroupToPracticesRepository, IMapper mapper)
            {
                _procedureGroupToPracticesRepository = procedureGroupToPracticesRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProcedureGroupToPractices>>> Handle(GetProcedureGroupToPracticesListQuery request, CancellationToken cancellationToken)
            {
                var list = await _procedureGroupToPracticesRepository.GetListAsync(x => x.PracticeId == request.PracticeId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ProcedureToGroupId).ToList();
                }

                return new SuccessDataResult<IEnumerable<ProcedureGroupToPractices>>(list.ToList());
            }
        }
    }
}
