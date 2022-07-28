using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetAppointmentByPatientIdListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientAppointmentDto>>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public int PatientId { get; set; }

        public class GetAppointmentByPatientIdListQueryHandler : IRequestHandler<GetAppointmentByPatientIdListQuery, IDataResult<IEnumerable<PatientAppointmentDto>>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public GetAppointmentByPatientIdListQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientAppointmentDto>>> Handle(GetAppointmentByPatientIdListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _appointmentRepository.GetAppointmentByPatientId(request.PatientId);
                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                var convertedData = rawData.Select(x => _mapper.Map<PatientAppointmentDto>(x)).ToList();

                return new PagedDataResult<IEnumerable<PatientAppointmentDto>>(convertedData, rawData.Count(), request.PageNumber);

            }
        }
    }
}
