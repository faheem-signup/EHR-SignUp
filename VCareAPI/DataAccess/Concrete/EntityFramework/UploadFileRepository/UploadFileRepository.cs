using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.IUploadFile;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDocsFileUploadEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UploadFile
{
    public class UploadFileRepository : EfEntityRepositoryBase<PatientDocsFileUpload, ProjectDbContext>, IUploadFileRepository
    {
        public UploadFileRepository(ProjectDbContext context) : base(context)
        {
        }


       


    }
}
