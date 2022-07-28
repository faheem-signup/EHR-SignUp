using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using DataAccess.Abstract.IPatientDocumentRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocumentCategories.Commands
{
    public class DeletePatientDocumentCategoryCommand : IRequest<IResult>
    {
        public int PatientDocCateogryId { get; set; }
        public class DeletePatientDocumentCategoryCommandHandler : IRequestHandler<DeletePatientDocumentCategoryCommand, IResult>
        {
            private readonly IPatientDocumentCategoryRepository _patientDocumentCategoryRepository;
            private readonly IPatientDocumentRepository _patientDocumentRepository;


            public DeletePatientDocumentCategoryCommandHandler(IPatientDocumentCategoryRepository patientDocumentCategoryRepository, IPatientDocumentRepository patientDocumentRepository)
            {
                _patientDocumentCategoryRepository = patientDocumentCategoryRepository;
                _patientDocumentRepository = patientDocumentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientDocumentCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientDocumentList =await _patientDocumentRepository.GetListAsync(x => x.PatientDocCateogryId == request.PatientDocCateogryId);
                    if (patientDocumentList != null)
                    {

                        foreach (var item in patientDocumentList)
                        {
                            _patientDocumentRepository.Delete(item);
                            await _patientDocumentRepository.SaveChangesAsync();
                        }

                        //_patientDocumentRepository.RemovePateintDocument(patientDocumentList);
                        //_patientDocumentRepository.SaveChangesAsync();
                    }

                    var patientDocumentCategoryToDelete = await _patientDocumentCategoryRepository.GetAsync(x => x.PatientDocCateogryId == request.PatientDocCateogryId);

                    _patientDocumentCategoryRepository.Delete(patientDocumentCategoryToDelete);
                    await _patientDocumentCategoryRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Deleted);
                }
                catch (System.Exception)
                {
                    return new ErrorResult("Can't be deleted");
                }

            }
        }
    }
}
