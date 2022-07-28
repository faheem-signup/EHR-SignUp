using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
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
    public class DeleteContactCommand : IRequest<IResult>
    {
        public int ContactId { get; set; }

        public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, IResult>
        {
            private readonly IContactRepository _contactRepository;

            public DeleteContactCommandHandler(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
            {
                var contactToDelete = await _contactRepository.GetAsync(x => x.ContactId == request.ContactId);
                contactToDelete.IsDeleted = true;
                _contactRepository.Update(contactToDelete);
                await _contactRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
