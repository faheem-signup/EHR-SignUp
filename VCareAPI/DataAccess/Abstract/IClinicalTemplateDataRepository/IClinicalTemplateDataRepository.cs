using Core.DataAccess;
using Entities.Concrete.ClinicalTemplateDataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.IClinicalTemplateDataRepository
{
    public interface IClinicalTemplateDataRepository : IEntityRepository<ClinicalTemplateData>
    {
    }
}
