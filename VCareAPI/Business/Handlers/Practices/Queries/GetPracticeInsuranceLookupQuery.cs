using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticesRepository;
using DataAccess.Abstract.IProviderClinicalInfoRepository;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.InsuranceDto;
using Entities.Dtos.PracticeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticeInsuranceLookupQuery : IRequest<IDataResult<IEnumerable<InsuranceLookupDto>>>
    {
        public int PracticeId { get; set; }
        public int ProviderId { get; set; }
        public class GetPracticeInsuranceLookupQueryHandler : IRequestHandler<GetPracticeInsuranceLookupQuery, IDataResult<IEnumerable<InsuranceLookupDto>>>
        {
            private readonly IPracticesRepository _practicesRepository;
            private readonly IProviderClinicalInfoRepository _providerClinicalInfoRepository;
            public GetPracticeInsuranceLookupQueryHandler(IPracticesRepository practicesRepository, IProviderClinicalInfoRepository providerClinicalInfoRepository)
            {
                _practicesRepository = practicesRepository;
                _providerClinicalInfoRepository = providerClinicalInfoRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<InsuranceLookupDto>>> Handle(GetPracticeInsuranceLookupQuery request, CancellationToken cancellationToken)
            {
                if (request.PracticeId != 0 && request.PracticeId != null)
                {
                    var practiceInsuranceList = await _practicesRepository.GetPracticeInsuranceList(request.PracticeId);
                    if (practiceInsuranceList.Count() > 0)
                    {
                        practiceInsuranceList = practiceInsuranceList.OrderByDescending(x => x.InsuranceId).ToList();
                    }

                    return new SuccessDataResult<IEnumerable<InsuranceLookupDto>>(practiceInsuranceList.ToList());
                }
                else if (request.ProviderId != 0 && request.ProviderId != null)
                {
                    var practiceInsuranceList = await _practicesRepository.GetCredentialedInsurancesList(request.ProviderId);
                    if (practiceInsuranceList.Count() > 0)
                    {
                        practiceInsuranceList = practiceInsuranceList.OrderByDescending(x => x.InsuranceId).ToList();
                    }

                    return new SuccessDataResult<IEnumerable<InsuranceLookupDto>>(practiceInsuranceList.ToList());
                }

                return new SuccessDataResult<IEnumerable<InsuranceLookupDto>>(new List<InsuranceLookupDto>());
            }
        }
    }
}
