using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.DocumentParentCategoryLookupRepository
{
  public  class DocumentParentCategoryLookupRepository : EfEntityRepositoryBase<DocumentParentCategoryLookup, ProjectDbContext>, IDocumentParentCategoryLookupRepository
    {
        public DocumentParentCategoryLookupRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
