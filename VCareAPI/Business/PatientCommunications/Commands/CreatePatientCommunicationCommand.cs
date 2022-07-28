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
    public class CreatePatientCommunicationCommand : IRequest<IResult>
    {
        public DateTime? CommunicationDate { get; set; }
        public DateTime? CommunicationTime { get; set; }
        public int? CommunicateBy { get; set; }
        public int? CallDetailTypeId { get; set; }
        public string CallDetailDescription { get; set; }
        public int? PatientId { get; set; }
        public class CreatePatientCommunicationCommandHandler : IRequestHandler<CreatePatientCommunicationCommand, IResult>
        {
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientCommunicationCommandHandler(IPatientCommunicationRepository patientCommunicationRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientCommunicationRepository = patientCommunicationRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientCommunication), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientCommunicationCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                userId = string.IsNullOrEmpty(userId) ? "0" : userId;
                PatientCommunication patientCommunicationObj = new PatientCommunication
                {
                    CommunicationDate = request.CommunicationDate,
                    CommunicationTime = request.CommunicationTime,
                    CommunicateBy = request.CommunicateBy,
                    CallDetailTypeId = request.CallDetailTypeId,
                    CallDetailDescription = request.CallDetailDescription,
                    PatientId = request.PatientId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _patientCommunicationRepository.Add(patientCommunicationObj);
                await _patientCommunicationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
