
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Abstract.IDiagnosisRepository;
using AutoMapper;
using Entities.Concrete;
using System;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Helpers.Validators;

namespace Business.Handlers.Diagnosises.Commands
{
   

    public class CreateDiagnosisCommand : IRequest<IResult>
    {
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int PracticeId { get; set; }
        public int ICDCategoryId { get; set; }
        public class CreateDiagnosisCommandHandler : IRequestHandler<CreateDiagnosisCommand, IResult>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateDiagnosisCommandHandler(IDiagnosisRepository diagnosiseRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _diagnosiseRepository = diagnosiseRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorDiagnosis), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                DiagnosisCode diagObj = new DiagnosisCode
                {
                    Code = request.Code,
                    ShortDescription = request.ShortDescription,
                    Description = request.Description,
                    StatusId = request.StatusId,
                    PracticeId = request.PracticeId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                    ICDCategoryId = request.ICDCategoryId,
                };

                _diagnosiseRepository.Add(diagObj);
                await _diagnosiseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}

