using Core.DataAccess;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Concrete.TemplateCategoryEntity;
using Entities.Dtos.TemplateCategoryDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IFormTemplateRepository
{
    public interface IFormTemplateRepository : IEntityRepository<FormTemplate>
    {
        Task<IEnumerable<TemplateCategory>> GetTemplateCategory();
        Task<IEnumerable<TemplateCategoryDto>> GetAllCategoriesTemplates();
        Task<IEnumerable<FormTemplate>> GetFormTemplateSearchParams(string TemplateName, int TemplateCategoryId, string Speciality);
    }
}
