using AutoMapper;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProcedureSubGroupRepository;
using Entities.Concrete.ProcedureSubGroupEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ProcedureSubGroups.Queries
{
    public class GetProcedureSubGroupByProcedureGroupId : BasePaginationQuery<IDataResult<IEnumerable<ProcedureSubGroup>>>
    {
        public int ProcedureGroupId { get; set; }
        public class GetProcedureSubGroupByProcedureGroupIdHandler : IRequestHandler<GetProcedureSubGroupByProcedureGroupId, IDataResult<IEnumerable<ProcedureSubGroup>>>
        {
            private readonly IProcedureSubGroupRepository _procedureSubGroupRepositoryry;
            private readonly IMapper _mapper;
            public GetProcedureSubGroupByProcedureGroupIdHandler(IProcedureSubGroupRepository procedureSubGroupRepositoryry, IMapper mapper)
            {
                _procedureSubGroupRepositoryry = procedureSubGroupRepositoryry;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProcedureSubGroup>>> Handle(GetProcedureSubGroupByProcedureGroupId request, CancellationToken cancellationToken)
            {
                var list = await _procedureSubGroupRepositoryry.GetListAsync(x=> x.ProcedureGroupId == request.ProcedureGroupId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ProcedureSubGroupId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ProcedureSubGroup>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
