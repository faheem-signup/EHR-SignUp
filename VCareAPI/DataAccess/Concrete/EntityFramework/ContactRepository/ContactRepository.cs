using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IContactRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.ContactEntity;
using Entities.Dtos.ContactDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.ContactRepository
{
    public class ContactRepository : EfEntityRepositoryBase<Contact, ProjectDbContext>, IContactRepository
    {
        public ContactRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Contact>> GetContactSearchParams(string Name, int? ContactTypeId, int? ZipId)
        {
            var data = Context.Contact.Include(x => x.Cities)
                .Include(x => x.states)
                .Include(x => x.zipcode)
                .Include(x => x.status)
                .Include(x => x.ContactType)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.ContactId)
                .ToList();

            if (data.Count() > 0)
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    data = data.Where(s => s.Name == Name).ToList();
                }

                if (ContactTypeId > 0)
                {
                    data = data.Where(s => s.ContactTypeId == ContactTypeId).ToList();
                }

                if (ZipId > 0)
                {
                    data = data.Where(s => s.ZIP == ZipId).ToList();
                }
            }

            return data;
        }
    }
}
