using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IServiceProfileRepository;
using Entities.Concrete.Role;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Dtos.ServiceProfileDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ServiceProfiles.Queries
{
    public class GetServiceProfileListQuery : BasePaginationQuery<IDataResult<IEnumerable<ServiceProfilesDto>>>
    {
        public class GetServiceProfileListQueryHandler : IRequestHandler<GetServiceProfileListQuery, IDataResult<IEnumerable<ServiceProfilesDto>>>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;
            public GetServiceProfileListQueryHandler(IServiceProfileRepository serviceProfileRepository)
            {
                _serviceProfileRepository = serviceProfileRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ServiceProfilesDto>>> Handle(GetServiceProfileListQuery request, CancellationToken cancellationToken)
            {
                List<ServiceProfilesDto> ServiceProfilesList = new List<ServiceProfilesDto>();
                var list = await _serviceProfileRepository.GetAllServiceProfile();
                var finalList = list.ToList().GroupBy(x => x.Row_Id).ToList();
                if (finalList.Count() > 0)
                {
                    finalList.ForEach(x =>
                    {
                        ServiceProfilesDto obj = new ServiceProfilesDto();
                        obj = x.FirstOrDefault();
                        obj.DiagnosisCode = string.Join(", ", x.Select(x => x.DiagnosisCode).Where(s => !string.IsNullOrEmpty(s)));
                        obj.Template = string.Join(", ", x.Select(x => x.Template).Where(s => !string.IsNullOrEmpty(s)));
                        obj.ProcedureCode = string.Join(", ", x.Select(x => x.ProcedureCode).Where(s => !string.IsNullOrEmpty(s)));
                        ServiceProfilesList.Add(obj);
                    });
                }

                var pagedData = Paginate(ServiceProfilesList, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ServiceProfilesDto>>(pagedData, ServiceProfilesList.Count(), request.PageNumber);
            }
        }
    }
}
