using Core.DataAccess;
using Entities.Concrete.SendEmailEntity;
using Entities.Dtos.SendEmailDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.ISendEmailRepository
{
    public interface ISendEmailRepository : IEntityRepository<SendEmail>
    {
        Task<List<SendEmailDto>> GetMessagesList(int EmailStatusId, int MessageStatusId, int UserId);
    }
}
