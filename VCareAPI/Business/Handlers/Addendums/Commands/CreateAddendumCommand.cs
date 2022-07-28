using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAddendumRepository;
using Entities.Concrete.AddendumEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Addendums.Commands
{
    public class CreateAddendumCommand : IRequest<IResult>
    {
        public int? ProviderId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? CreatedByDate { get; set; }
        public DateTime? ModifiedByDate { get; set; }
        public DateTime? CreatedByTime { get; set; }
        public DateTime? ModifiedByTime { get; set; }
        public string Comments { get; set; }
        public string ProviderSignature { get; set; }
        public class CreateAddendumCommandHandler : IRequestHandler<CreateAddendumCommand, IResult>
        {
            private readonly IAddendumRepository _addendumRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateAddendumCommandHandler(IAddendumRepository addendumRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _addendumRepository = addendumRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorAddendum), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateAddendumCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Addendum addendumObj = new Addendum
                {
                    ProviderId = request.ProviderId,
                    PatientId = request.PatientId,
                    CreatedByDate = request.CreatedByDate,
                    ModifiedByDate = request.ModifiedByDate,
                    CreatedByTime = request.CreatedByTime,
                    ModifiedByTime = request.ModifiedByTime,
                    Comments = request.Comments,
                    ProviderSignature = request.ProviderSignature,
                };

                _addendumRepository.Add(addendumObj);
                await _addendumRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
