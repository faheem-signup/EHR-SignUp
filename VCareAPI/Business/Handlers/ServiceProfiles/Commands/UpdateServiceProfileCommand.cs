using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IServiceProfileRepository;
using Entities.Concrete;
using Entities.Concrete.Role;
using Entities.Concrete.ServiceProfileEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ServiceProfiles.Commands
{
    public class UpdateServiceProfileCommand : IRequest<IResult>
    {
        public string Row_Id { get; set; }
        public string ServiceProfileName { get; set; }
        public int[] ICDIds { get; set; }
        public int[] ProcedureIds { get; set; }
        public int[] TemplateIds { get; set; }

        public class UpdateServiceProfileCommandHandler : IRequestHandler<UpdateServiceProfileCommand, IResult>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;

            public UpdateServiceProfileCommandHandler(IServiceProfileRepository serviceProfileRepository)
            {
                _serviceProfileRepository = serviceProfileRepository;
            }

            // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateServiceProfileCommand request, CancellationToken cancellationToken)
            {
                List<ServiceProfile> serviceProfileList = new List<ServiceProfile>();
                var row_Id = Guid.Parse(request.Row_Id);

                if (request.Row_Id != string.Empty)
                {
                    // var df = Guid.Parse(Row_Id);
                    //_serviceProfileRepository.RemoveServiceProfile(Guid.Parse(request.Row_Id));
                    //await _serviceProfileRepository.SaveChangesAsync();

                    var isthereanyexistProfile = await _serviceProfileRepository.GetListAsync(x => x.Row_Id == Guid.Parse(request.Row_Id));
                    if (isthereanyexistProfile != null && isthereanyexistProfile.Count() > 0)
                    {
                        _serviceProfileRepository.RemoveServiceProfile(isthereanyexistProfile.ToList());
                        await _serviceProfileRepository.SaveChangesAsync();
                    }


                    if (request.ICDIds.Length > 0)
                    {
                        var serviceProfileICDIds = request.ICDIds.Select(x => new ServiceProfile() { ICDId = x, ServiceProfileName = request.ServiceProfileName, Row_Id = row_Id });
                        serviceProfileList.AddRange(serviceProfileICDIds);
                    }

                    if (request.ProcedureIds.Length > 0)
                    {
                        var serviceProfileProcedureIds = request.ProcedureIds.Select(x => new ServiceProfile() { ProcedureId = x, ServiceProfileName = request.ServiceProfileName, Row_Id = row_Id });
                        serviceProfileList.AddRange(serviceProfileProcedureIds);
                    }

                    if (request.TemplateIds.Length > 0)
                    {
                        var serviceProfileTemplateIds = request.TemplateIds.Select(x => new ServiceProfile() { TemplateId = x, ServiceProfileName = request.ServiceProfileName, Row_Id = row_Id });
                        serviceProfileList.AddRange(serviceProfileTemplateIds);
                    }

                    if (serviceProfileList.Count() > 0)
                    {
                        await _serviceProfileRepository.BulkInsert(serviceProfileList);
                        await _serviceProfileRepository.SaveChangesAsync();
                    }
                }


                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
