using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Concrete.FormTemplatesEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Commands
{
    public class CreateFormTemplateCommand : IRequest<IResult>
    {
        public string TemplateName { get; set; }
        public string TemplateHtml { get; set; }
        public string JsonHtml { get; set; }
        public int? ProviderId { get; set; }
        public int? TemplateCategoryId { get; set; }
        public class CreateFormTemplateCommandHandler : IRequestHandler<CreateFormTemplateCommand, IResult>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateFormTemplateCommandHandler(IFormTemplateRepository formTemplateRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _formTemplateRepository = formTemplateRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateFormTemplateCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                FormTemplate formTemplateObj = new FormTemplate
                {
                    TemplateName = request.TemplateName,
                    TemplateHtml = request.TemplateHtml,
                    JsonHtml = request.JsonHtml,
                    ProviderId = request.ProviderId,
                    TemplateCategoryId = request.TemplateCategoryId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now
                };

                //string fileName = "guid";
                //fileName = fileName + "-" + DateTime.Now.Ticks + ".html";

                ////string folderName = "FormTemplate";
                ////string folderpath = Path.GetFullPath("~/UploadedFiles/" + folderName).Replace("~\\", "");
                ////if (!Directory.Exists(folderpath))
                ////{
                ////    Directory.CreateDirectory(folderpath);
                ////}
                //string fullpath = @"C:\\Project\\VCareProject\\VCareDev\\VCareAPI\\WebAPI\\logs\\"+ fileName;// Path.GetFullPath("~/UploadedFiles/FormTemplate/" + fileName).Replace("~\\", "");

                //using (FileStream fs = File.Create(fullpath, 1024))
                //{
                //    byte[] info = new UTF8Encoding(true).GetBytes(formTemplateObj.TemplateHtml);
                //    fs.Write(info, 0, info.Length);
                //}



                //formTemplateObj.TemplateFilePath = fileName;

                _formTemplateRepository.Add(formTemplateObj);
                await _formTemplateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
