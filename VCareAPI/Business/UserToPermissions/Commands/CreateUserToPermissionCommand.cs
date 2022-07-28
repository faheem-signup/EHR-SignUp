using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserToPermissionRepository;
using Entities.Concrete.UserToPermissions;
using Entities.Dtos.UesrAppDto;
using Entities.Dtos.UserPermissionsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserToPermissions.Commands
{

    public class CreateUserToPermissionCommand : IRequest<IResult>
    {
        //public int? RoleToPermissionId { get; set; }
        public int UserId { get; set; }
        //public bool? CanView { get; set; }
        //public bool? CanEdit { get; set; }
        //public bool? CanAdd { get; set; }
        //public bool? CanSearch { get; set; }
        //public bool? CanDelete { get; set; }
        public List<UserPermissionsDto> userPermissionList { get; set; }

        public class CreateUserToPermissionCommandHandler : IRequestHandler<CreateUserToPermissionCommand, IResult>
        {
            private readonly IUserToPermissionRepository _userToPermissionRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateUserToPermissionCommandHandler(IUserToPermissionRepository userToPermissionRepository, IMediator mediator, IMapper mapper)
            {
                _userToPermissionRepository = userToPermissionRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateUserToPermissionCommand request, CancellationToken cancellationToken)
            {
                //UserToPermission userToPermissionObj = new UserToPermission
                //{
                //    RoleToPermissionId = request.RoleToPermissionId,
                //    UserId = request.UserId,
                //    CanView = request.CanView,
                //    CanEdit = request.CanEdit,
                //    CanAdd = request.CanAdd,
                //    CanSearch = request.CanSearch,
                //    CanDelete = request.CanDelete
                //};

                //_userToPermissionRepository.Add(userToPermissionObj);
                //await _userToPermissionRepository.SaveChangesAsync();

                if (request.userPermissionList.Count() > 0)
                {

                    var existingUserToPermission = _userToPermissionRepository.GetList(x => x.UserId == request.UserId).ToList();

                    foreach (var dataObj in request.userPermissionList)
                    {
                UserToPermission userToPermissionObj = new UserToPermission();

                        var FindExistingItem = existingUserToPermission.Where(a => a.RoleToPermissionsId == dataObj.RoleToPermissionId).FirstOrDefault();

                        if (FindExistingItem != null)
                        {
                            FindExistingItem.RoleToPermissionsId = dataObj.RoleToPermissionId;
                            FindExistingItem.CanAdd = dataObj.CanAdd;
                            FindExistingItem.CanDelete = dataObj.CanDelete;
                            FindExistingItem.CanEdit = dataObj.CanEdit;
                            FindExistingItem.CanView = dataObj.CanView;
                            FindExistingItem.CanSearch = dataObj.CanSearch;
                            FindExistingItem.UserId = request.UserId;
                            _userToPermissionRepository.Update(FindExistingItem);
                            _userToPermissionRepository.SaveChanges();
                            existingUserToPermission.Remove(FindExistingItem); // Removed the updated records from existingRoleToPermission list
                        }
                        else
                        {
                            userToPermissionObj.UserId = request.UserId;
                            userToPermissionObj.RoleToPermissionsId = dataObj.RoleToPermissionId;
                            userToPermissionObj.CanAdd = dataObj.CanAdd;
                            userToPermissionObj.CanDelete = dataObj.CanDelete;
                            userToPermissionObj.CanEdit = dataObj.CanEdit;
                            userToPermissionObj.CanView = dataObj.CanView;
                            userToPermissionObj.CanSearch = dataObj.CanSearch;
                            _userToPermissionRepository.Add(userToPermissionObj);
                            _userToPermissionRepository.SaveChanges();
                        }
                    }

                    if (existingUserToPermission.Count() > 0) /// If record is not available than remove record
                    {
                        existingUserToPermission.ToList().ForEach(a =>
                        {
                            _userToPermissionRepository.Delete(a);
                        });
                        _userToPermissionRepository.SaveChangesAsync();
                    }

                }


                return new SuccessResult(Messages.Added);
            }
        }
    }
}

