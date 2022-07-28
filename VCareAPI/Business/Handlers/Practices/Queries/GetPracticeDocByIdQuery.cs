using Core.Utilities.Results;
using DataAccess.Abstract.IPracticeDocsRepository;
using Entities.Concrete.PracticeDocsEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Practices.Queries
{
    public class GetPracticeDocByIdQuery : IRequest<IDataResult<PracticeDoc>>
    {
        public int DocmentId { get; set; }
        public string DocmentName { get; set; }

        public class GetPracticeDocByIdQueryHandler : IRequestHandler<GetPracticeDocByIdQuery, IDataResult<PracticeDoc>>
        {
            private readonly IPracticeDocsRepository _practiceDocsRepository;
            public GetPracticeDocByIdQueryHandler(IPracticeDocsRepository practiceDocsRepository)
            {
                _practiceDocsRepository = practiceDocsRepository;
            }

            public async Task<IDataResult<PracticeDoc>> Handle(GetPracticeDocByIdQuery request, CancellationToken cancellationToken)
            {
                var practiceDoc = await _practiceDocsRepository.GetAsync(x => x.DocmentId == request.DocmentId);

                try
                {
                    var directoryPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "UploadedFiles\\PracticeDocuments";
                    var filePath = Path.Combine(directoryPath, request.DocmentName);
                    //if (!System.IO.File.Exists(filePath))
                        //return NotFound();

                    var memory = new MemoryStream();
                    await using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    //return File(memory, "application/octet-stream", ActualFileName);
                }
                catch (Exception ex)
                {
                    //return BadRequest();
                }

                return new SuccessDataResult<PracticeDoc>(practiceDoc);
            }
        }
    }
}
