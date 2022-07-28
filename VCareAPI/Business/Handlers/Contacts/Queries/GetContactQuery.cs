using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IContactRepository;
using Entities.Concrete.ContactEntity;
using Entities.Dtos.ContactDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Contacts.Queries
{
    public class GetContactQuery : IRequest<IDataResult<Contact>>
    {
        public int ContactId { get; set; }

        public class GetContactQueryHandler : IRequestHandler<GetContactQuery, IDataResult<Contact>>
        {
            private readonly IContactRepository _contactRepository;
            public GetContactQueryHandler(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Contact>> Handle(GetContactQuery request, CancellationToken cancellationToken)
            {
                var contact = await _contactRepository.GetAsync(x => x.ContactId == request.ContactId && x.IsDeleted != true);

                return new SuccessDataResult<Contact>(contact);
            }
        }
    }
}
