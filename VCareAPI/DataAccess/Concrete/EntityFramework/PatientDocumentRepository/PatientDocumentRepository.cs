using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientDocumentRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Dtos.PatientDocumentsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientDocumentRepository
{
    public class PatientDocumentRepository : EfEntityRepositoryBase<PatientDocument, ProjectDbContext>, IPatientDocumentRepository
    {
        public PatientDocumentRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<PatientDocumentDto>> GetPatientDocumentCategory(int PatientID, int PatientDocCateogryId)
        {
            List<PatientDocumentDto> query = new List<PatientDocumentDto>();

            var sql = $@"select pd.[PatientDocumentId]
                        ,pd.[PatientDocCateogryId]
                        ,pd.[UploadDocumentId]
                        ,pd.[DateOfVisit]
                        ,pd.[PatientId] 
                        ,pd.CreatedDate
	                    ,pdf.[DocumentPath]
	                    ,pdc.[CategoryName]
                        ,pdf.[DocumentName]
                        ,pdf.[DocumentData]
                        ,pdf.FileType
	                    from dbo.PatientDocument pd
                        LEFT JOIN dbo.PatientDocumentCategory pdc
                        ON pdc.PatientDocCateogryId = pd.PatientDocCateogryId
                        LEFT JOIN dbo.PatientDocsFileUpload pdf
                        ON pdf.UploadDocumentId = pd.UploadDocumentId
                        where pd.PatientId =" + PatientID + " and pd.PatientDocCateogryId = " + PatientDocCateogryId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDocumentDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.PatientDocumentId).ToList();
            }

            return query;
        }

        public async Task<PatientDocumentDto> GetPatientDocumentById(int PatientDocumentId)
        {
            PatientDocumentDto query = new PatientDocumentDto();
            var sql = string.Empty;
            if (PatientDocumentId != null && PatientDocumentId > 0)
            {
                sql = $@"select pd.[PatientDocumentId]
                        ,pd.[PatientDocCateogryId]
                        ,pd.[UploadDocumentId]
                        ,pd.[DateOfVisit]
                        ,pd.[PatientId] 
	                    ,pdf.[DocumentPath]
	                    ,pdc.[CategoryName]
                        ,pdf.[DocumentName]
	                    from dbo.PatientDocument pd
                        LEFT JOIN dbo.PatientDocumentCategory pdc
                        ON pdc.PatientDocCateogryId = pd.PatientDocCateogryId
                        LEFT JOIN dbo.PatientDocsFileUpload pdf
                        ON pdf.UploadDocumentId = pd.UploadDocumentId
                        where pd.PatientDocumentId =" + PatientDocumentId;
            }

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<PatientDocumentDto>(sql).FirstOrDefault();
            }

            return query;
        }

        public async Task RemovePateintDocument(IEnumerable<PatientDocument> patientDocumentList)
        {
            try
            {
                Context.PatientDocument.RemoveRange(patientDocumentList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
