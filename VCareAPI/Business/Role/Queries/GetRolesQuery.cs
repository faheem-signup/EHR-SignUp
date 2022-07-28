using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IRolesRepository;
using Entities.Concrete.Role;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Role.Queries
{
    public class GetRolesQuery : IRequest<IDataResult<Roles>>
    {
        public int RoleId { get; set; }

        public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IDataResult<Roles>>
        {
            private readonly IRolesRepository _rolesRepository;

            public GetRolesQueryHandler(IRolesRepository rolesRepository)
            {
                _rolesRepository = rolesRepository;
            }

           // [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Roles>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
            {
                var role = await _rolesRepository.GetAsync(p => p.RoleId == request.RoleId);
                return new SuccessDataResult<Roles>(role);
            }
        }
    }
}
