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
    public class UpdateContactCommand : IRequest<IResult>
    {
        public int ContactId { get; set; }
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
        public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, IResult>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateContactCommandHandler(IContactRepository contactRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _contactRepository = contactRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateContact), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var contactObj = await _contactRepository.GetAsync(x => x.ContactId == request.ContactId && x.IsDeleted != true);

                if (contactObj != null)
                {
                    contactObj.ContactId = request.ContactId;
                    contactObj.Name = request.Name;
                    contactObj.Email = request.Email;
                    contactObj.Phone = request.Phone;
                    contactObj.Fax = request.Fax;
                    contactObj.Address = request.Address;
                    contactObj.City = request.City;
                    contactObj.State = request.State;
                    contactObj.ZIP = request.ZIP;
                    contactObj.StatusId = request.StatusId;
                    contactObj.ModifiedBy = int.Parse(userId);
                    contactObj.ModifiedDate = DateTime.Now;
                    contactObj.ContactTypeId = request.ContactTypeId;
                    _contactRepository.Update(contactObj);
                    await _contactRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
