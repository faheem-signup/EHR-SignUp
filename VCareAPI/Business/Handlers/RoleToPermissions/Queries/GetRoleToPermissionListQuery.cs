using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.IRoleToPermissionRepository;
using Entities.Concrete.Location;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Concrete.User;
using Entities.Dtos.RoleToPermissionsDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RoleToPermissions.Queries
{
    public class GetRoleToPermissionListQuery : BasePaginationQuery<IDataResult<IEnumerable<AllRoleToPermissionDto>>>
    {
        public int RoleId { get; set; }

        public class GetRoleToPermissionListQueryHandler : IRequestHandler<GetRoleToPermissionListQuery, IDataResult<IEnumerable<AllRoleToPermissionDto>>>
        {
            private readonly IRoleToPermissionRepository _roleToPermissionRepository;
            private readonly IMapper _mapper;
            public GetRoleToPermissionListQueryHandler(IRoleToPermissionRepository roleToPermissionRepository, IMapper mapper)
            {
                _roleToPermissionRepository = roleToPermissionRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AllRoleToPermissionDto>>> Handle(GetRoleToPermissionListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _roleToPermissionRepository.GetRoleToPermissionListByRoleId(request.RoleId);
                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                var convertedData = rawData.Select(x => _mapper.Map<AllRoleToPermissionDto>(x)).ToList();
                return new PagedDataResult<IEnumerable<AllRoleToPermissionDto>>(convertedData, rawData.Count(), request.PageNumber);
            }
        }
    }
}
