using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IAddendumRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.AddendumEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.AddendumRepository
{
    public class AddendumRepository : EfEntityRepositoryBase<Addendum, ProjectDbContext>, IAddendumRepository
    {
        public AddendumRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
