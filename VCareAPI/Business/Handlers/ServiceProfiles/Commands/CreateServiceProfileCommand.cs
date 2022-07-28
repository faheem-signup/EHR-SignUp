using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract.IServiceProfileRepository;
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

    public class CreateServiceProfileCommand : IRequest<IResult>
    {
        public string ServiceProfileName { get; set; }
        public int[] ICDIds { get; set; }
        public int[] ProcedureIds { get; set; }
        public int[] TemplateIds { get; set; }
        public class CreateServiceProfileCommandHandler : IRequestHandler<CreateServiceProfileCommand, IResult>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateServiceProfileCommandHandler(IServiceProfileRepository serviceProfileRepository, IMediator mediator, IMapper mapper)
            {
                _serviceProfileRepository = serviceProfileRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateServiceProfileCommand request, CancellationToken cancellationToken)
            {
                List<ServiceProfile> serviceProfileList = new List<ServiceProfile>();
                Guid rowid = Guid.NewGuid();
                if (request.ICDIds.Length > 0)
                {
                    var serviceProfileICDIds = request.ICDIds.Select(x => new ServiceProfile() { ICDId = x, ServiceProfileName = request.ServiceProfileName,Row_Id= rowid });
                    serviceProfileList.AddRange(serviceProfileICDIds);
                }

                if (request.ProcedureIds.Length > 0)
                {
                    var serviceProfileProcedureIds = request.ProcedureIds.Select(x => new ServiceProfile() { ProcedureId = x, ServiceProfileName = request.ServiceProfileName, Row_Id = rowid });
                    serviceProfileList.AddRange(serviceProfileProcedureIds);
                }

                if (request.TemplateIds.Length > 0)
                {
                    var serviceProfileTemplateIds = request.TemplateIds.Select(x => new ServiceProfile() { TemplateId = x, ServiceProfileName = request.ServiceProfileName, Row_Id = rowid });
                    serviceProfileList.AddRange(serviceProfileTemplateIds);
                }


                if (serviceProfileList.Count() > 0)
                {
                    await _serviceProfileRepository.BulkInsert(serviceProfileList);
                    await _serviceProfileRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

