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
using DataAccess.Abstract.IProcedureGroupToPracticesRepository;
using Entities.Dtos.ProcedureGroupsToPractice;

namespace Business.Handlers.ProcedureGroupToPractice.Queries
{
    public class GetProcedureGroupWithProcedureSubGroupListQuery : BasePaginationQuery<IDataResult<IEnumerable<ProcedureGroupWithProcedureSubGroupDto>>>
    {
        public class GetProcedureGroupWithProcedureSubGroupListQueryHandler : IRequestHandler<GetProcedureGroupWithProcedureSubGroupListQuery, IDataResult<IEnumerable<ProcedureGroupWithProcedureSubGroupDto>>>
        {
            private readonly IProcedureGroupToPracticesRepository _procedureGroupToPracticesRepository;
            private readonly IMapper _mapper;
            public GetProcedureGroupWithProcedureSubGroupListQueryHandler(IProcedureGroupToPracticesRepository procedureGroupToPracticesRepository, IMapper mapper)
            {
                _procedureGroupToPracticesRepository = procedureGroupToPracticesRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProcedureGroupWithProcedureSubGroupDto>>> Handle(GetProcedureGroupWithProcedureSubGroupListQuery request, CancellationToken cancellationToken)
            {
                var list = await _procedureGroupToPracticesRepository.GetProcedureGroupWithProcedureSubGroup();

                List<ProcedureGroupWithProcedureSubGroupDto> procedureGroupWithProcedureSubGroupList = new List<ProcedureGroupWithProcedureSubGroupDto>();

                var groupList = list.GroupBy(x => x.ProcedureGroupId);

                groupList.ToList().ForEach(x =>
                {
                    ProcedureGroupWithProcedureSubGroupDto obj = new ProcedureGroupWithProcedureSubGroupDto();
                    obj.ProcedureGroupId = x.FirstOrDefault().ProcedureGroupId;
                    obj.ProcedureGroupName = x.FirstOrDefault().ProcedureGroupName;
                    obj.ProcedureGroupCode = x.FirstOrDefault().ProcedureGroupCode;
                    obj.ProcedureSubGroupDtoList = x.Select(x => _mapper.Map<ProcedureSubGroupDto>(x)).ToList();
                    procedureGroupWithProcedureSubGroupList.Add(obj);
                });

                var pagedData = Paginate(procedureGroupWithProcedureSubGroupList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ProcedureGroupWithProcedureSubGroupDto>>(pagedData, procedureGroupWithProcedureSubGroupList.Count(), request.PageNumber);
            }
        }
    }
}
