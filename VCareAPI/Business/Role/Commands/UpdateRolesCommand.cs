using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IRolesRepository;
using Entities.Concrete;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Role.Commands
{
    public class UpdateRolesCommand : IRequest<IResult>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public class UpdateRolesCommandHandler : IRequestHandler<UpdateRolesCommand, IResult>
        {
            private readonly IRolesRepository _rolesRepository;

            public UpdateRolesCommandHandler(IRolesRepository rolesRepository)
            {
                _rolesRepository = rolesRepository;
            }

            [ValidationAspect(typeof(ValidatorUpdateRole), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
            {
                var roleObj = await _rolesRepository.GetAsync(x => x.RoleId == request.RoleId);
                if (roleObj != null)
                {
                    roleObj.RoleId = request.RoleId;
                    roleObj.RoleName = request.RoleName;

                    _rolesRepository.Update(roleObj);
                    await _rolesRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Updated);
                }
                else
                {
                    return new ErrorResult(Messages.NoRecordFound);
                }
            }
        }
    }
}
