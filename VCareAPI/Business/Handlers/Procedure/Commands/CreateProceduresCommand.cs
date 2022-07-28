using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProceduresRepository;
using Entities.Concrete.ProceduresEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Commands
{
    public class CreateProceduresCommand : IRequest<IResult>
    {
        public string Code { get; set; }
        public string NDCNumber { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? POSId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? Date { get; set; }
        public decimal? DefaultCharges { get; set; }
        public decimal? MedicareAllowance { get; set; }
        public int PracticeId { get; set; }
        public int? ProcedureGroupId { get; set; }
        public int? ProcedureSubGroupId { get; set; }
        public class CreateProcedureCommandHandler : IRequestHandler<CreateProceduresCommand, IResult>
        {
            private readonly IProceduresRepository _proceduresRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreateProcedureCommandHandler(IProceduresRepository proceduresRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _proceduresRepository = proceduresRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorProcedure), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateProceduresCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                Entities.Concrete.ProceduresEntity.Procedure procedureObj = new Entities.Concrete.ProceduresEntity.Procedure
                {
                    Code = request.Code,
                    NDCNumber = request.NDCNumber,
                    ShortDescription = request.ShortDescription,
                    Description = request.Description,
                    POSId = request.POSId,
                    IsExpired = (bool)request.IsExpired,
                    Date = request.Date,
                    DefaultCharges = request.DefaultCharges,
                    MedicareAllowance =request.MedicareAllowance,
                    PracticeId = request.PracticeId,
                    ProcedureGroupId = request.ProcedureGroupId,
                    ProcedureSubGroupId = request.ProcedureSubGroupId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _proceduresRepository.Add(procedureObj);
                await _proceduresRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}