using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.ISendEmailRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.SendEmailEntity;
using Entities.Dtos.SendEmailDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.SendEmailRepository
{
    public class SendEmailRepository : EfEntityRepositoryBase<SendEmail, ProjectDbContext>, ISendEmailRepository
    {
        public SendEmailRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SendEmailDto>> GetMessagesList(int EmailStatusId, int MessageStatusId, int UserId)
        {
            List<SendEmailDto> query = new List<SendEmailDto>();
            var sql = $@"select e.EmailId, e.EmailFrom, e.EmailTo, e.EmailSubject, e.EmailBody, e.EmailCC, 
                e.EmailBCC, e.UserId, u.FirstName + ' ' + u.LastName as UserName
                from SendEmail e
                inner join EmailStatuses es on e.EmailStatusId = es.EmailStatusId
                inner join MessageStatuses ms on e.MessageStatusId = ms.MessageStatusId
                inner join UserApp u on e.UserId = u.UserId
                where e.EmailStatusId = " + EmailStatusId + " and e.MessageStatusId = " + MessageStatusId + " and e.UserId = " + UserId;

            using (var connection = Context.Database.GetDbConnection())
            {
                query = connection.Query<SendEmailDto>(sql).ToList();
            }

            return query;
        }
    }
}
