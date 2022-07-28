using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ADLFunctionRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ADLEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.ADLFunctionRepository
{
    public class ADLFunctionRepository : EfEntityRepositoryBase<ADLFunction, ProjectDbContext>, IADLFunctionRepository
    {
        public ADLFunctionRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
