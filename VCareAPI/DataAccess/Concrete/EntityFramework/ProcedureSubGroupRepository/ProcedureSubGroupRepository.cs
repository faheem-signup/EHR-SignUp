using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IProcedureSubGroupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ProcedureSubGroupEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.ProcedureSubGroupRepository
{
    public class ProcedureSubGroupRepository : EfEntityRepositoryBase<ProcedureSubGroup, ProjectDbContext>, IProcedureSubGroupRepository
    {
        public ProcedureSubGroupRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
