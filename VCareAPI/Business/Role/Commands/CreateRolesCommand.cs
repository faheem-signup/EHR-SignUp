using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRolesRepository;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Role.Commands
{

    public class CreateRolesCommand : IRequest<IResult>
    {
        public string RoleName { get; set; }

        public class CreateRolesCommandHandler : IRequestHandler<CreateRolesCommand, IResult>
        {
            private readonly IRolesRepository _rolesRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateRolesCommandHandler(IRolesRepository rolesRepository, IMediator mediator, IMapper mapper)
            {
                _rolesRepository = rolesRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(ValidatorRole), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateRolesCommand request, CancellationToken cancellationToken)
            {
                var roleName = await _rolesRepository.GetAsync(x => x.RoleName == request.RoleName);
                if (roleName != null)
                {
                    return new ErrorResult(Messages.UniqueName);
                }

                Roles roleObj = new Roles
                {
                    RoleName = request.RoleName,
                };

                _rolesRepository.Add(roleObj);
                await _rolesRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}