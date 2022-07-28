using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using Entities.Concrete.LocationWorkConfigsEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LocationWorkConfig.Commands
{

    public class CreateLocationWorkConfigsCommand : IRequest<IResult>
    {
        public int? LocationId { get; set; }
        public List<LocationWorkConfigs> locationWorkConfigList { get; set; }

        public class CreateLocationWorkConfigsCommandHandler : IRequestHandler<CreateLocationWorkConfigsCommand, IResult>
        {
            private readonly ILocationWorkConfigsRepository _locationWorkConfigsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateLocationWorkConfigsCommandHandler(ILocationWorkConfigsRepository locationWorkConfigsRepository, IMediator mediator, IMapper mapper)
            {
                _locationWorkConfigsRepository = locationWorkConfigsRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
           // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLocationWorkConfigsCommand request, CancellationToken cancellationToken)
            {
                if (request.locationWorkConfigList.Count() > 0)
                {

                    request.locationWorkConfigList.ToList().ForEach(x => x.LocationId = request.LocationId);  // Adding LocationId in locationWorkConfig List

                    var existingList = await _locationWorkConfigsRepository.GetListAsync(x => x.LocationId == request.LocationId);
                    _locationWorkConfigsRepository.BulkInsert(existingList, request.locationWorkConfigList);
                    await _locationWorkConfigsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

