using AutoMapper;
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
    public class GetContactsQuery : BasePaginationQuery<IDataResult<IEnumerable<ContactDto>>>
    {
        public string Name { get; set; }
        public int? ContactTypeId { get; set; }
        public int? ZipId { get; set; }

        public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IDataResult<IEnumerable<ContactDto>>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMapper _mapper;

            public GetContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
            {
                _contactRepository = contactRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ContactDto>>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
            {
                var list = await _contactRepository.GetContactSearchParams(request.Name, request.ContactTypeId, request.ZipId);
                var dataList = Paginate(list, request.PageNumber, request.PageSize);
                var convertedData = list.Select(x => _mapper.Map<ContactDto>(x)).ToList();

                return new PagedDataResult<IEnumerable<ContactDto>>(convertedData, list.Count(), request.PageNumber);
            }
        }
    }
}
