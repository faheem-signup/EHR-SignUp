using Core.DataAccess.EntityFramework;
using Dapper;
using DataAccess.Abstract.IPatientCommunicationRepository;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Dtos.PatientCommunicationDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.PatientCommunicationRepository
{
    public class PatientCommunicationRepository : EfEntityRepositoryBase<PatientCommunication, ProjectDbContext>, IPatientCommunicationRepository
    {
        public PatientCommunicationRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<GetPatientCommunicationListDto>> GetPatientCommunication(int PatientId)
        {
            List<PatientCommunicationListDto> query = new List<PatientCommunicationListDto>();
            List<GetPatientCommunicationListDto> querylist = new List<GetPatientCommunicationListDto>();


            var sql = $@"select pc.CommunicationId,pc.CommunicationDate, pc.CommunicationTime, pc.CallDetailDescription, pc.PatientId,
                    ct.CallDetail, u.FirstName + ' '+ u.LastName as CommunicateByName
                    from PatientCommunication pc
                    left outer join CommunicationCallDetailType ct on pc.CallDetailTypeId = ct.CallDetailTypeId
                    left outer join UserApp u on pc.CommunicateBy = u.UserId
                    where ISNULL(pc.IsDeleted,0)=0 and pc.PatientId = " + PatientId;

            var connection = Context.Database.GetDbConnection();
            query = connection.Query<PatientCommunicationListDto>(sql).ToList();

            if (query.Count() > 0)
            {
                querylist = query.ConvertAll(a =>
                {
                    return new GetPatientCommunicationListDto()
                    {
                        CommunicationId = a.CommunicationId,
                        CommunicationDate = a.CommunicationDate,
                        CommunicationTime = a.CommunicationTime,
                        CallDetailDescription = a.CallDetailDescription,
                        PatientId = a.PatientId,
                        CallDetail = a.CallDetail,
                        CommunicateByName = a.CommunicateByName,
                    };
                });
            }

            if (querylist.Count() > 0)
            {
                querylist = querylist.OrderByDescending(x => x.CommunicationId).ToList();
            }

            return querylist;
        }
    }
}
