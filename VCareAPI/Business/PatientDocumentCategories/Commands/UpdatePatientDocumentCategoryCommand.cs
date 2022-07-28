using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using Entities.Concrete;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocumentCategories.Commands
{
    public class UpdatePatientDocumentCategoryCommand : IRequest<IResult>
    {
        public int PatientDocCateogryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public class UpdatePatientDocumentCategoryCommandHandler : IRequestHandler<UpdatePatientDocumentCategoryCommand, IResult>
        {
            private readonly IPatientDocumentCategoryRepository _patientDocumentCategoryRepository;

            public UpdatePatientDocumentCategoryCommandHandler(IPatientDocumentCategoryRepository patientDocumentCategoryRepository)
            {
                _patientDocumentCategoryRepository = patientDocumentCategoryRepository;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientDocumentCategory), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientDocumentCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingData = await _patientDocumentCategoryRepository.GetAsync(x => x.PatientDocCateogryId == request.PatientDocCateogryId);
                if (existingData != null)
                {
                    existingData.CategoryName = request.CategoryName;
                    existingData.ParentCategoryId = request.ParentCategoryId;

                    _patientDocumentCategoryRepository.Update(existingData);
                    await _patientDocumentCategoryRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
