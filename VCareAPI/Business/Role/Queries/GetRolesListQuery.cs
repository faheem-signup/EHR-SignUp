using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRolesRepository;
using Entities.Concrete.Role;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Role.Queries
{
    public class GetRolesListQuery : BasePaginationQuery<IDataResult<IEnumerable<Roles>>>
    {

        public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, IDataResult<IEnumerable<Roles>>>
        {
            private readonly IRolesRepository _rolesRepository;

            public GetRolesListQueryHandler(IRolesRepository rolesRepository)
            {
                _rolesRepository = rolesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Roles>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
            {
                var list = await _rolesRepository.GetListAsync();
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.RoleId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Roles>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
