using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
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
    public class UpdateFormTemplateCommand : IRequest<IResult>
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateHtml { get; set; }
        public string JsonHtml { get; set; }
        public int? ProviderId { get; set; }
        public int? TemplateCategoryId { get; set; }
        public class UpdateFormTemplateCommandHandler : IRequestHandler<UpdateFormTemplateCommand, IResult>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateFormTemplateCommandHandler(IFormTemplateRepository formTemplateRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _formTemplateRepository = formTemplateRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateFormTemplateCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var formTemplateObj = await _formTemplateRepository.GetAsync(x => x.TemplateId == request.TemplateId);
                if (formTemplateObj != null)
                {
                    formTemplateObj.TemplateId = request.TemplateId;
                    formTemplateObj.TemplateName = request.TemplateName;
                    formTemplateObj.TemplateHtml = request.TemplateHtml;
                    formTemplateObj.JsonHtml = request.JsonHtml;
                    formTemplateObj.ProviderId = request.ProviderId;
                    formTemplateObj.TemplateCategoryId = request.TemplateCategoryId;
                    formTemplateObj.ModifiedBy = int.Parse(userId);
                    formTemplateObj.ModifiedDate = DateTime.Now;

                    //string fullpath = formTemplateObj.TemplateFilePath;

                    //string folderName = "FormTemplate";
                    //string folderpath = Path.GetFullPath("~/UploadedFiles/" + folderName).Replace("~\\", "");
                    //if (!Directory.Exists(folderpath))
                    //{
                    //    Directory.CreateDirectory(folderpath);
                    //}

                    //string folderpathSave = Path.GetFullPath("~" + formTemplateObj.TemplateFilePath).Replace("~\\", "");
                    //FileInfo file = new FileInfo(folderpathSave);
                    //if (file.Exists)
                    //{
                    //    file.Delete();
                    //}

                    //using (FileStream fs = File.Create(folderpathSave, 1024))
                    //{
                    //    byte[] info = new UTF8Encoding(true).GetBytes(formTemplateObj.TemplateHtml);
                    //    fs.Write(info, 0, info.Length);
                    //}

                    _formTemplateRepository.Update(formTemplateObj);
                    await _formTemplateRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
