using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoleToPermissionRepository;
using DataAccess.Abstract.IUserToPermissionRepository;
using Entities.Concrete.Role;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Dtos.RoleToPermissionsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RoleToPermissions.Commands
{

    public class CreateRoleToPermissionCommand : IRequest<IResult>
    {
        public int RoleId { get; set; }

        public List<RoleToPermissionDto> roleToPermissionList { get; set; }

        public class CreateRoleToPermissionCommandHandler : IRequestHandler<CreateRoleToPermissionCommand, IResult>
        {
            private readonly IRoleToPermissionRepository _roleToPermissionRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IUserToPermissionRepository _userToPermissionRepository;

            public CreateRoleToPermissionCommandHandler(IRoleToPermissionRepository roleToPermissionRepository, IMediator mediator, IMapper mapper, IUserToPermissionRepository userToPermissionRepository)
            {
                _roleToPermissionRepository = roleToPermissionRepository;
                _mediator = mediator;
                _mapper = mapper;
                _userToPermissionRepository = userToPermissionRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateRoleToPermissionCommand request, CancellationToken cancellationToken)
            {
                if (request.roleToPermissionList != null && request.roleToPermissionList.Count() > 0)
                {
                    List<RoleToPermission> roleToPermissionList = request.roleToPermissionList.ConvertAll(a =>
                    {
                        return new RoleToPermission()
                        {
                            RoleId = request.RoleId,
                            PageId = a.PageId,
                            SubPageId = a.SubPageId,
                        };
                    });

                    var existingList = await _roleToPermissionRepository.GetListAsync(x => x.RoleId == request.RoleId);
                    if (existingList.Count() > 0)
                    {
                        foreach (var item in existingList)
                        {
                            var roleObj = await _userToPermissionRepository.GetListAsync(x => x.RoleToPermissionsId == item.Id);
                            if (roleObj.Count() > 0)
                            {
                                _roleToPermissionRepository.DeleteUserToPermission(roleObj);
                                await _roleToPermissionRepository.SaveChangesAsync();
                            }
                        }

                    }
                    _roleToPermissionRepository.BulkInsert(existingList, roleToPermissionList);
                    await _roleToPermissionRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}