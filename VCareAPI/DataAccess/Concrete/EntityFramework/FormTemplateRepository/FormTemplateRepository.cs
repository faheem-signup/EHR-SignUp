using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IFormTemplateRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Concrete.TemplateCategoryEntity;
using Entities.Dtos.FormTemplateDto;
using Entities.Dtos.TemplateCategoryDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.FormTemplateRepository
{
    public class FormTemplateRepository : EfEntityRepositoryBase<FormTemplate, ProjectDbContext>, IFormTemplateRepository
    {
        public FormTemplateRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<TemplateCategoryDto>> GetAllCategoriesTemplates()
        {
            List<TemplateCategoryDto> query = new List<TemplateCategoryDto>();
            List<CategoryDto> CategoriesTemplatesList = new List<CategoryDto>();

            var sql = $@"select c.TemplateCategoryId as CategoryId, c.CategoryName, t.*
                    from TemplateCategory c
                    left outer join FormTemplates t on c.TemplateCategoryId = t.TemplateCategoryId";

            using (var connection = Context.Database.GetDbConnection())
            {
                CategoriesTemplatesList = connection.Query<CategoryDto>(sql).ToList();
            }

            if (CategoriesTemplatesList.Count() > 0)
            {
                var categories = CategoriesTemplatesList.Select(x => x.CategoryId).Distinct().ToList();
                foreach (var categoryId in categories)
                {
                    var tempList = CategoriesTemplatesList.Where(x => x.CategoryId == categoryId).ToList();
                    TemplateCategoryDto templateCategoryDto = new TemplateCategoryDto();
                    templateCategoryDto.TemplateCategoryId = categoryId;
                    string cName = tempList.Where(x => x.CategoryId == categoryId).Select(y => y.CategoryName).FirstOrDefault();
                    templateCategoryDto.CategoryName = cName;
                    templateCategoryDto.TemplateList = tempList.ConvertAll(a =>
                    {
                        return new TemplateDto()
                        {
                            TemplateId = a.TemplateId,
                            TemplateName = a.TemplateName,
                            TemplateFilePath = a.TemplateFilePath,
                            TemplateHtml = a.TemplateHtml,
                            JsonHtml = a.JsonHtml,
                            CreatedBy = a.CreatedBy,
                            CreatedDate = a.CreatedDate,
                            ModifiedBy = a.ModifiedBy,
                            ModifiedDate = a.ModifiedDate,
                            ProviderId = a.ProviderId,
                            TemplateCategoryId = a.TemplateCategoryId,
                            CategoryName = cName,
                        };
                    });

                    query.Add(templateCategoryDto);
                }
            }

            return query;
        }

        public async Task<IEnumerable<FormTemplate>> GetFormTemplateSearchParams(string TemplateName, int TemplateCategoryId, string Speciality)
        {
            var list = Context.FormTemplates.ToList();

            if (!string.IsNullOrEmpty(TemplateName))
            {
                list = list.Where(x => x.TemplateName == TemplateName).ToList();
            }

            if (list.Count() > 0)
            {
                list = list.OrderByDescending(x => x.TemplateId).ToList();
            }

            return list;
        }

        public async Task<IEnumerable<TemplateCategory>> GetTemplateCategory()
        {
            var _list = Context.TemplateCategory.ToList();
            return _list;
        }

        public async Task<List<FormTemplateDto>> GetFormTemplateList()
        {

            //return Context.Appointment.Include(x => x.Provider).Include(x => x.Locations).Include(x => x._appointmentStatuses).Include(x => x._appointmentTypes).Include(x => x._serviceProfile)
            //    .Include(x => x.Room).Include(x => x.AppointmentReasons).Where(x=>x.ProviderId == ProviderId);
            List<FormTemplateDto> query = new List<FormTemplateDto>();
            var sql = $@"SELECT ft.[TemplateId]
                         ,ft.[TemplateName]
                         ,ft.[TemplateCategoryId]
                         ,ft.[TemplateFilePath]
                         ,ft.[JsonHtml]
                         ,ft.[TemplateHtml]
                         ,ft.[ProviderId]
                         ,ft.[CreatedBy]
                         ,ft.[CreatedDate]
                         ,ft.[ModifiedBy]
                         ,ft.[ModifiedDate]
                    	  ,(ISNULL(pr.FirstName,'')+ ISNULL(' '+pr.LastName,'')) as ProviderName
                    	  ,(select CategoryName from dbo.TemplateCategory where TemplateCategoryId =ft.TemplateCategoryId ) as TemplateCategory
                     FROM [dbo].[FormTemplates] ft
                     INNER JOIN dbo.Provider pr
                     ON pr.ProviderId = ft.ProviderId";

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<FormTemplateDto>(sql).ToList();
            }

            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.TemplateId).ToList();
            }

            return query;
        }

        public async Task<IEnumerable<NotesDto>> GetNotes(int PatientId, int ProviderId)
        {
            List<NotesDto> query = new List<NotesDto>();
            var sql = $@"select distinct a.AppointmentId, a.AppointmentDate, a.PatientId, a.ProviderId, 
                pr.FirstName + ' ' + pr.LastName as ProviderName, t.TemplateId, t.TemplateName, td.ClinicalTemplateId
                from Appointment a
			    inner join Provider pr on a.ProviderId = pr.ProviderId and ISNULL(a.IsDeleted,0)=0 and ISNULL(pr.IsDeleted,0)=0
				inner join Patients p on a.PatientId = p.PatientId and ISNULL(p.IsDeleted,0)=0
				inner join ClinicalTemplateData td on p.PatientId = td.PatientId and pr.ProviderId = td.ProviderId 
                inner join FormTemplates t on td.TemplateId = t.TemplateId
				and t.TemplateId = td.TemplateId and td.AppointmentId = a.AppointmentId
                where td.PatientId = " + PatientId;

            if (ProviderId != null && ProviderId > 0)
            {
                sql = sql + " and td.ProviderId = " + ProviderId;
            }

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<NotesDto>(sql).ToList();
            if (query.Count() > 0)
            {
                query = query.OrderByDescending(x => x.ClinicalTemplateId).ToList();
            }

            return query;
        }
    }
}
