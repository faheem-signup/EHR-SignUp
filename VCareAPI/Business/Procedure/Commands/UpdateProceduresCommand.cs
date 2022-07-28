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
using DataAccess.Abstract.IProceduresRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Commands
{
    public class UpdateProceduresCommand : IRequest<IResult>
    {
        public int ProcedureId { get; set; }
        public string Code { get; set; }
        public string NDCNumber { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? POSId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? Date { get; set; }
        public decimal? DefaultCharges { get; set; }
        public decimal? MedicareAllowance { get; set; }
        public int? PracticeId { get; set; }
        public int? ProcedureGroupId { get; set; }
        public int? ProcedureSubGroupId { get; set; }

        public class UpdateProceduresCommandHandler : IRequestHandler<UpdateProceduresCommand, IResult>
        {
            private readonly IProceduresRepository _proceduresRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateProceduresCommandHandler(IProceduresRepository proceduresRepository, IHttpContextAccessor contextAccessor)
            {
                _proceduresRepository = proceduresRepository;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateProcedure), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateProceduresCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var procedureObj = await _proceduresRepository.GetAsync(x => x.ProcedureId == request.ProcedureId);
                if(procedureObj != null)
                {
                    procedureObj.ProcedureId = request.ProcedureId;
                    procedureObj.Code = request.Code;
                    procedureObj.NDCNumber = request.NDCNumber;
                    procedureObj.ShortDescription = request.ShortDescription;
                    procedureObj.Description = request.Description;
                    procedureObj.POSId = request.POSId;
                    procedureObj.IsExpired = (bool)request.IsExpired;
                    procedureObj.Date = request.Date;
                    procedureObj.DefaultCharges = (decimal)request.DefaultCharges;
                    procedureObj.MedicareAllowance = (decimal)request.MedicareAllowance;
                    procedureObj.PracticeId = request.PracticeId;
                    procedureObj.ModifiedBy = int.Parse(userId);
                    procedureObj.ModifiedDate = DateTime.Now;
                    _proceduresRepository.Update(procedureObj);
                    await _proceduresRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
