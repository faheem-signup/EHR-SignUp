using Business.Handlers.PatientDocumentCategories.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientDocumentCategory : AbstractValidator<CreatePatientDocumentCategoryCommand>
    {
        public ValidatorPatientDocumentCategory()
        {
            RuleFor(p => p.ParentCategoryId).NotEqual(0).WithMessage("Please select Parent Category");
            RuleFor(p => p.ParentCategoryId).NotNull().WithMessage("Please select Parent Category");
            RuleFor(p => p.CategoryName).NotEmpty().WithMessage("Please Enter Category Name!");
            RuleFor(p => p.CategoryName).MaximumLength(25).WithMessage("Please Enter Category Name Less Than 25 Characters!");
        }
    }

    public class ValidatorUpdatePatientDocumentCategory : AbstractValidator<UpdatePatientDocumentCategoryCommand>
    {
        public ValidatorUpdatePatientDocumentCategory()
        {
            RuleFor(p => p.PatientDocCateogryId).NotEqual(0).WithMessage("Please select PatientDocCateogryId");
            RuleFor(p => p.PatientDocCateogryId).NotNull().WithMessage("Please select PatientDocCateogryId");
            RuleFor(p => p.ParentCategoryId).NotEqual(0).WithMessage("Please select Parent Category");
            RuleFor(p => p.ParentCategoryId).NotNull().WithMessage("Please select Parent Category");
            RuleFor(p => p.CategoryName).NotEmpty().WithMessage("Please Enter Category Name!");
            RuleFor(p => p.CategoryName).MaximumLength(25).WithMessage("Please Enter Category Name Less Than 25 Characters!");
        }
    }
}
