using Core.Utilities.Results;
using Entities.Concrete.InsurancePayerTypeEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Handlers.Insurances.Queries
{
    public class GetInsurancePayerTypesQuery : IRequest<IDataResult<IEnumerable<InsurancePayerType>>>
    {
    }
}
