using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using Entities.Concrete.Location;
using Entities.Concrete.LocationWorkConfigsEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Locations.Commands
{

    public class CreateLocationCommand : IRequest<IResult>
    {
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string NPI { get; set; }
        public int? POSId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }
        public int? PracticeId { get; set; }
        public List<LocationWorkConfigs> locationWorkConfigList { get; set; }
        public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, IResult>
        {
            private readonly ILocationRepository _locationRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly ILocationWorkConfigsRepository _locationWorkConfigsRepository;

            public CreateLocationCommandHandler(ILocationRepository locationRepository, IMediator mediator, IMapper mapper, ILocationWorkConfigsRepository locationWorkConfigsRepository)
            {
                _locationRepository = locationRepository;
                _mediator = mediator;
                _mapper = mapper;
                _locationWorkConfigsRepository=locationWorkConfigsRepository;

            }

            [ValidationAspect(typeof(ValidatorLocation), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
            {

                Entities.Concrete.Location.Locations locationObj = new Entities.Concrete.Location.Locations
                {
                    LocationName = request.LocationName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    ZIP = request.ZIP,
                    NPI = request.NPI,
                    POSId = request.POSId,
                    Phone = request.Phone,
                    Fax = request.Fax,
                    Email = request.Email,
                    StatusId = request.StatusId,
                    PracticeId = request.PracticeId
                };

                _locationRepository.Add(locationObj);
                await _locationRepository.SaveChangesAsync();


                if (request.locationWorkConfigList != null && request.locationWorkConfigList.Count() > 0)
                {
                    request.locationWorkConfigList.ToList().ForEach(x => x.LocationId = locationObj.LocationId);

                    var existingList = await _locationWorkConfigsRepository.GetListAsync(x => x.LocationId == locationObj.LocationId);
                    _locationWorkConfigsRepository.BulkInsert(existingList, request.locationWorkConfigList);
                    await _locationWorkConfigsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

