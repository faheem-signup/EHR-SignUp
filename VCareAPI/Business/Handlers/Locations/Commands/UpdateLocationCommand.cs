using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.ILocationWorkConfigsRepository;
using DataAccess.Abstract.IProceduresRepository;
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
    public class UpdateLocationCommand : IRequest<IResult>
    {
        public int LocationId { get; set; }
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

        public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, IResult>
        {
            private readonly ILocationRepository _locationRepository;
            private readonly ILocationWorkConfigsRepository _locationWorkConfigsRepository;

            public UpdateLocationCommandHandler(ILocationRepository locationRepository, ILocationWorkConfigsRepository locationWorkConfigsRepository)
            {
                _locationRepository = locationRepository;
                _locationWorkConfigsRepository = locationWorkConfigsRepository;
            }

            [ValidationAspect(typeof(ValidatorUpdateLocation), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
            {
                var locationObj = await _locationRepository.GetAsync(x => x.LocationId == request.LocationId);
                if (locationObj != null)
                {
                    locationObj.LocationId = request.LocationId;
                    locationObj.LocationName = request.LocationName;
                    locationObj.Address = request.Address;
                    locationObj.City = request.City;
                    locationObj.State = request.State;
                    locationObj.ZIP = request.ZIP;
                    locationObj.NPI = request.NPI;
                    locationObj.POSId = request.POSId;
                    locationObj.Phone = request.Phone;
                    locationObj.Fax = request.Fax;
                    locationObj.Email = request.Email;
                    locationObj.StatusId = request.StatusId;
                    locationObj.PracticeId = request.PracticeId;
                    _locationRepository.Update(locationObj);
                    await _locationRepository.SaveChangesAsync();
                }


                if (request.locationWorkConfigList != null && request.locationWorkConfigList.Count() > 0)
                {
                    request.locationWorkConfigList.ToList().ForEach(x => x.LocationId = locationObj.LocationId);  // Adding Location in LocationWorkHourConfig List
                    var existingList = await _locationWorkConfigsRepository.GetListAsync(x => x.LocationId == locationObj.LocationId);
                    _locationWorkConfigsRepository.BulkInsert(existingList, request.locationWorkConfigList);
                    await _locationWorkConfigsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
