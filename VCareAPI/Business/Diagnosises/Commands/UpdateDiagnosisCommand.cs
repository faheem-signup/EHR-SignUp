using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Diagnosises.Commands
{
    public class UpdateDiagnosisCommand : IRequest<IResult>
    {
        public int DiagnosisId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int PracticeId { get; set; }
        public int ICDCategoryId { get; set; }
        public class UpdateDiagnosisCommandHandler : IRequestHandler<UpdateDiagnosisCommand, IResult>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateDiagnosisCommandHandler(IDiagnosisRepository diagnosiseRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _diagnosiseRepository = diagnosiseRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateDiagnosis), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDiagnosisCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var diagUpdateObj = await _diagnosiseRepository.GetAsync(x => x.DiagnosisId == request.DiagnosisId && x.IsDeleted != true);

                if(diagUpdateObj != null)
                {
                    diagUpdateObj.DiagnosisId = request.DiagnosisId;
                    diagUpdateObj.Code = request.Code;
                    diagUpdateObj.ShortDescription = request.ShortDescription;
                    diagUpdateObj.Description = request.Description;
                    diagUpdateObj.StatusId = request.StatusId;
                    diagUpdateObj.PracticeId = request.PracticeId;
                    diagUpdateObj.ModifiedBy = int.Parse(userId);
                    diagUpdateObj.ModifiedDate = DateTime.Now;
                    diagUpdateObj.ICDCategoryId = request.ICDCategoryId;

                    _diagnosiseRepository.Update(diagUpdateObj);
                    await _diagnosiseRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
