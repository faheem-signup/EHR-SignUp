using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Concrete.FormTemplatesEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetFormTemplateQuery : IRequest<IDataResult<FormTemplate>>
    {
        public int TemplateId { get; set; }

        public class GetFormTemplateQueryHandler : IRequestHandler<GetFormTemplateQuery, IDataResult<FormTemplate>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetFormTemplateQueryHandler(IFormTemplateRepository formTemplateRepository, IMediator mediator, IMapper mapper)
            {
                _formTemplateRepository = formTemplateRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<FormTemplate>> Handle(GetFormTemplateQuery request, CancellationToken cancellationToken)
            {
                var formTemplate = await _formTemplateRepository.GetAsync(x => x.TemplateId == request.TemplateId);

                string fullpath = formTemplate.TemplateFilePath;

                //string folderName = "FormTemplate";
                //string folderpath = Path.GetFullPath("~/UploadedFiles/" + folderName).Replace("~\\", "");
                //if (!Directory.Exists(folderpath))
                //{
                //    Directory.CreateDirectory(folderpath);
                //}

                //FileInfo file = new FileInfo(fullpath);
                //if (!file.Exists)
                //{
                //    using (FileStream fs = File.Create(fullpath, 1024))
                //    {
                //        byte[] info = new UTF8Encoding(true).GetBytes(formTemplate.TemplateHtml);
                //        fs.Write(info, 0, info.Length);
                //    }
                //}

                return new SuccessDataResult<FormTemplate>(formTemplate);
            }
        }
    }
}
