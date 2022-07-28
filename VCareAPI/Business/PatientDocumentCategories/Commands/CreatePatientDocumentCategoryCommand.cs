using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using Entities.Concrete.PatientDocumentCategoryEntity;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocumentCategories.Commands
{

    public class CreatePatientDocumentCategoryCommand : IRequest<IResult>
    {
        public int ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public class CreatePatientDocumentCategoryCommandHandler : IRequestHandler<CreatePatientDocumentCategoryCommand, IResult>
        {
            private readonly IPatientDocumentCategoryRepository _patientDocumentCategoryRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreatePatientDocumentCategoryCommandHandler(IPatientDocumentCategoryRepository patientDocumentCategoryRepository, IMediator mediator, IMapper mapper)
            {
                _patientDocumentCategoryRepository = patientDocumentCategoryRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(ValidatorPatientDocumentCategory), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientDocumentCategoryCommand request, CancellationToken cancellationToken)
            {

                PatientDocumentCategory patientDocumentCategoryObj = new PatientDocumentCategory
                {
                    ParentCategoryId = request.ParentCategoryId,
                    CategoryName = request.CategoryName,
                };

                _patientDocumentCategoryRepository.Add(patientDocumentCategoryObj);
                await _patientDocumentCategoryRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

