using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IServiceProfileRepository;
using Entities.Concrete.ServiceProfileEntity;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ServiceProfiles.Commands
{
    public class DeleteServiceProfileCommand : IRequest<IResult>
    {
        public string Row_Id { get; set; }

        public class DeleteServiceProfileCommandHandler : IRequestHandler<DeleteServiceProfileCommand, IResult>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;

            public DeleteServiceProfileCommandHandler(IServiceProfileRepository serviceProfileRepository)
            {
                _serviceProfileRepository = serviceProfileRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteServiceProfileCommand request, CancellationToken cancellationToken)
            {

                var isthereanyexistProfile = await _serviceProfileRepository.GetListAsync(x => x.Row_Id == Guid.Parse(request.Row_Id));
                if (isthereanyexistProfile != null && isthereanyexistProfile.Count() > 0)
                {
                    _serviceProfileRepository.RemoveServiceProfile(isthereanyexistProfile.ToList());
                      await _serviceProfileRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
