using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IContactRepository;
using Entities.Concrete.ContactEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Contacts.Commands
{
    public class CreateContactCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? StatusId { get; set; }
        public int? ContactTypeId { get; set; }
        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, IResult>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateContactCommandHandler(IContactRepository contactRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _contactRepository = contactRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorContact), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Contact contactObj = new Contact
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Fax = request.Fax,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    StatusId = request.StatusId == 1 ? 1 : 2,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                    ContactTypeId= request.ContactTypeId,
                };

                _contactRepository.Add(contactObj);
                await _contactRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
