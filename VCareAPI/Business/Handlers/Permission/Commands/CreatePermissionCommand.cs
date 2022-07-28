using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPermissionRepository;
using Entities.Concrete;
using Entities.Concrete.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Permission.Commands
{

    public class CreatePermissionCommand : IRequest<IResult>
    {
        public string PermissionDescription { get; set; }
        public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, IResult>
        {
            private readonly IPermissionRepository _permissionRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IMediator mediator, IMapper mapper)
            {
                _permissionRepository = permissionRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
            {

                Permissions permissionsObj = new Permissions
                {
                    PermissionDescription = request.PermissionDescription,
                };

                _permissionRepository.Add(permissionsObj);
                await _permissionRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

