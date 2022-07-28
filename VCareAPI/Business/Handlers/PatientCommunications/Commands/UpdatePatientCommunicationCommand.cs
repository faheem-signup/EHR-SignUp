using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientCommunicationRepository;
using Entities.Concrete.PatientCommunicationEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientCommunications.Commands
{
    public class UpdatePatientCommunicationCommand : IRequest<IResult>
    {
        public int? CommunicationId { get; set; }
        public DateTime? CommunicationDate { get; set; }
        public DateTime? CommunicationTime { get; set; }
        public int? CommunicateBy { get; set; }
        public int? CallDetailTypeId { get; set; }
        public string CallDetailDescription { get; set; }
        public int? PatientId { get; set; }
        public class UpdatePatientCommunicationCommandHandler : IRequestHandler<UpdatePatientCommunicationCommand, IResult>
        {
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdatePatientCommunicationCommandHandler(IPatientCommunicationRepository patientCommunicationRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientCommunicationRepository = patientCommunicationRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientCommunication), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientCommunicationCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                userId = string.IsNullOrEmpty(userId) ? "1" : userId;
                var patientCommunicationObj = await _patientCommunicationRepository.GetAsync(x => x.CommunicationId == request.CommunicationId && x.IsDeleted != true);
                if (patientCommunicationObj != null)
                {
                    patientCommunicationObj.CommunicationDate = request.CommunicationDate;
                    patientCommunicationObj.CommunicationTime = request.CommunicationTime;
                    patientCommunicationObj.CallDetailTypeId = request.CallDetailTypeId;
                    patientCommunicationObj.CallDetailTypeId = request.CallDetailTypeId;
                    patientCommunicationObj.CallDetailDescription = request.CallDetailDescription;
                    patientCommunicationObj.PatientId = request.PatientId;
                    patientCommunicationObj.ModifiedBy = int.Parse(userId);
                    patientCommunicationObj.ModifiedDate = DateTime.Now;

                    _patientCommunicationRepository.Update(patientCommunicationObj);
                    await _patientCommunicationRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
