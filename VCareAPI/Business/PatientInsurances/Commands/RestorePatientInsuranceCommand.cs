using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Base64FileExtension;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IPatientInsuranceTypeRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.ReferralProviderEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Commands
{
    public class RestorePatientInsuranceCommand : IRequest<IResult>
    {
        public int PatientInsuranceId { get; set; }
        public class RestorePatientInsuranceCommandHandler : IRequestHandler<RestorePatientInsuranceCommand, IResult>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IPatientInsuranceTypeRepository _patientInsuranceTypeRepository;


            public RestorePatientInsuranceCommandHandler(IPatientInsuranceRepository patientInsuranceRepository, IHttpContextAccessor contextAccessor, IPatientInsuranceTypeRepository patientInsuranceTypeRepository)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _contextAccessor = contextAccessor;
                _patientInsuranceTypeRepository = patientInsuranceTypeRepository;

            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(RestorePatientInsuranceCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var patientInsuranceObj = await _patientInsuranceRepository.GetAsync(x => x.PatientInsuranceId == request.PatientInsuranceId);
                if (patientInsuranceObj != null)
                {
                    patientInsuranceObj.PatientInsuranceId = request.PatientInsuranceId;
                    patientInsuranceObj.IsDeleted = false;
                    patientInsuranceObj.ModifiedBy = int.Parse(userId);
                    patientInsuranceObj.ModifiedDate = DateTime.Now;

                    _patientInsuranceRepository.Update(patientInsuranceObj);
                    await _patientInsuranceRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }

        }
    }
}
