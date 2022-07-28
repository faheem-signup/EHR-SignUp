using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using Entities.Concrete;
using Entities.Concrete.PatientInsuranceAuthorizationCPTEntity;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsuranceAuthorizations.Commands
{
    public class UpdatePatientInsuranceAuthorizationCommand : IRequest<IResult>
    {
        public int PatientInsuranceAuthorizationId { get; set; }
        public int PatientInsuranceId { get; set; }
        public string AuthorizationNo { get; set; }
        public string Count { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? WarningDate { get; set; }
        public string Comment { get; set; }
        public string AuthorizationUnitsVsits { get; set; }
        public int[] DignoseIds { get; set; }
        public int[] ProcedureIds { get; set; }
        // public List<PatientInsuranceAuthorizationCPT> PatientInsuranceAuthorizationCPTList { get; set; }

        public class UpdatePatientInsuranceAuthorizationCommandHandler : IRequestHandler<UpdatePatientInsuranceAuthorizationCommand, IResult>
        {
            private readonly IPatientInsuranceAuthorizationRepository _patientInsuranceAuthorizationRepository;
            private readonly IPatientInsuranceAuthorizationCPTRepository _patientInsuranceAuthorizationCPTRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            public UpdatePatientInsuranceAuthorizationCommandHandler(IPatientInsuranceAuthorizationRepository patientInsuranceAuthorizationRepository, IPatientInsuranceAuthorizationCPTRepository patientInsuranceAuthorizationCPTRepository, IHttpContextAccessor contextAccessor)
            {
                _patientInsuranceAuthorizationRepository = patientInsuranceAuthorizationRepository;
                _patientInsuranceAuthorizationCPTRepository = patientInsuranceAuthorizationCPTRepository;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientInsuranceAuthorization), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientInsuranceAuthorizationCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                userId = string.IsNullOrEmpty(userId) ? "0" : userId;
                var patientInsuranceAuthorizationObj = await _patientInsuranceAuthorizationRepository.GetAsync(x => x.PatientInsuranceAuthorizationId == request.PatientInsuranceAuthorizationId && x.IsDeleted != true);

                if(patientInsuranceAuthorizationObj != null)
                {
                    patientInsuranceAuthorizationObj.PatientInsuranceAuthorizationId = request.PatientInsuranceAuthorizationId;
                    patientInsuranceAuthorizationObj.PatientInsuranceId = request.PatientInsuranceId;
                    patientInsuranceAuthorizationObj.AuthorizationNo = request.AuthorizationNo;
                    patientInsuranceAuthorizationObj.Count = request.Count;
                    patientInsuranceAuthorizationObj.StartDate = request.StartDate;
                    patientInsuranceAuthorizationObj.EndDate = request.EndDate;
                    patientInsuranceAuthorizationObj.WarningDate = request.WarningDate;
                    patientInsuranceAuthorizationObj.Comment = request.Comment;
                    patientInsuranceAuthorizationObj.AuthorizationUnitsVsits = request.AuthorizationUnitsVsits;
                    patientInsuranceAuthorizationObj.ModifiedBy = int.Parse(userId);
                    patientInsuranceAuthorizationObj.ModifiedDate = DateTime.Now;

                    _patientInsuranceAuthorizationRepository.Update(patientInsuranceAuthorizationObj);
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
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
