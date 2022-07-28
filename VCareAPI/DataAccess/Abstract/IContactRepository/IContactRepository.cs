using Core.DataAccess;
using Entities.Concrete.ContactEntity;
using Entities.Dtos.ContactDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.IContactRepository
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
        Task<IEnumerable<ContactDto>> GetContactSearchParams(int? PracticeId, string Name, int? ContactTypeId, int? ZipId);

    }
}
