using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientCommunicationRepository;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Dtos.PatientCommunicationDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Business.Handlers.PatientCommunications.Queries
{
    public class GetPatientCommunicationQuery : BasePaginationQuery<IDataResult<IEnumerable<GetPatientCommunicationListDto>>>
    {
        public int PatientId { get; set; }

        public class GetPatientCommunicationQueryHandler : IRequestHandler<GetPatientCommunicationQuery, IDataResult<IEnumerable<GetPatientCommunicationListDto>>>
        {
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;
            private readonly IMapper _mapper;

            public GetPatientCommunicationQueryHandler(IPatientCommunicationRepository patientCommunicationRepository, IMapper mapper)
            {
                _patientCommunicationRepository = patientCommunicationRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<GetPatientCommunicationListDto>>> Handle(GetPatientCommunicationQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientCommunicationRepository.GetPatientCommunication(request.PatientId);
                var dataList = Paginate(list, request.PageNumber, request.PageSize);

                return new PagedDataResult<IEnumerable<GetPatientCommunicationListDto>>(dataList, list.Count(), request.PageNumber);
            }
        }
    }
}
