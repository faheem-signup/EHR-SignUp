using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsuranceAuthorizations.Commands
{
    public class CreatePatientInsuranceAuthorizationCommand : IRequest<IResult>
    {
        public int PatientInsuranceId { get; set; }
        public string AuthorizationNo { get; set; }
        public string Count { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? WarningDate { get; set; }
        public string Comment { get; set; }
        public string AuthorizationUnitsVsits { get; set; }
        //public List<PatientInsuranceAuthorizationCPT> PatientInsuranceAuthorizationCPTList { get; set; }
        public int[] DignoseIds { get; set; }
        public int[] ProcedureIds { get; set; }

        public class CreatePatientInsuranceAuthorizationCommandHandler : IRequestHandler<CreatePatientInsuranceAuthorizationCommand, IResult>
        {
            private readonly IPatientInsuranceAuthorizationRepository _patientInsuranceAuthorizationRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IPatientInsuranceAuthorizationCPTRepository _patientInsuranceAuthorizationCPTRepository;

            public CreatePatientInsuranceAuthorizationCommandHandler(IPatientInsuranceAuthorizationRepository patientInsuranceAuthorizationRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor, IPatientInsuranceAuthorizationCPTRepository patientInsuranceAuthorizationCPTRepository)
            {
                _patientInsuranceAuthorizationRepository = patientInsuranceAuthorizationRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
                _patientInsuranceAuthorizationCPTRepository = patientInsuranceAuthorizationCPTRepository;
            }

            [ValidationAspect(typeof(ValidatorPatientInsuranceAuthorization), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientInsuranceAuthorizationCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                userId = string.IsNullOrEmpty(userId) ? "0" : userId;
                PatientInsuranceAuthorization patientInsuranceAuthorizationObj = new PatientInsuranceAuthorization
                {
                    PatientInsuranceId = request.PatientInsuranceId,
                    AuthorizationNo = request.AuthorizationNo,
                    Count = request.Count,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    WarningDate = request.WarningDate,
                    Comment = request.Comment,
                    AuthorizationUnitsVsits = request.AuthorizationUnitsVsits,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _patientInsuranceAuthorizationRepository.Add(patientInsuranceAuthorizationObj);
                await _patientInsuranceAuthorizationRepository.SaveChangesAsync();

                List<PatientInsuranceAuthorizationCPT> patientInsuranceAuthorizationCPTList = new List<PatientInsuranceAuthorizationCPT>();
                if (request.DignoseIds.Count() > 0)
                {
                    var dignoseIdList = request.DignoseIds.Select(x => new PatientInsuranceAuthorizationCPT() { PatientInsuranceAuthorizationId = patientInsuranceAuthorizationObj.PatientInsuranceAuthorizationId, DiagnosisId = x });
                    patientInsuranceAuthorizationCPTList.AddRange(dignoseIdList);
                }

                if (request.DignoseIds.Length > 0)
                {
                    var procedureIdList = request.ProcedureIds.Select(x => new PatientInsuranceAuthorizationCPT() { PatientInsuranceAuthorizationId = patientInsuranceAuthorizationObj.PatientInsuranceAuthorizationId, ProcedureId = x });
                    patientInsuranceAuthorizationCPTList.AddRange(procedureIdList);
                }

                if (patientInsuranceAuthorizationCPTList.Count() > 0)
                {
                    var existingList = await _patientInsuranceAuthorizationCPTRepository.GetListAsync(x => x.PatientInsuranceAuthorizationId == patientInsuranceAuthorizationObj.PatientInsuranceAuthorizationId);
                    _patientInsuranceAuthorizationCPTRepository.BulkInsert(existingList, patientInsuranceAuthorizationCPTList);
                    await _patientInsuranceAuthorizationCPTRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
