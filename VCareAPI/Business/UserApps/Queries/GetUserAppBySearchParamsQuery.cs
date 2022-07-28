using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IUserAppRepository;
using Entities.Concrete.User;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserApps.Queries
{
    public class GetUserAppBySearchParamsQuery : IRequest<IDataResult<IEnumerable<UserApp>>>
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int LocationId { get; set; }


        //public class GetUserAppBySearchParamsQueryHandler : IRequestHandler<GetUserAppBySearchParamsQuery, IDataResult<IEnumerable<UserApp>>>
        //{
        //    private readonly IUserAppRepository _userAppRepository;
        //    public GetUserAppBySearchParamsQueryHandler(IUserAppRepository userAppRepository)
        //    {
        //        _userAppRepository = userAppRepository;
        //    }

        //    [SecuredOperation(Priority = 1)]
        //    [LogAspect(typeof(FileLogger))]
        //    // [CacheAspect(10)]
        //    public async Task<IDataResult<IEnumerable<UserApp>>> Handle(GetUserAppBySearchParamsQuery request, CancellationToken cancellationToken)
        //    {
        //        UserAppSearchParams userAppSearchParams = new UserAppSearchParams
        //        {
        //            Name = request.Name,
        //            UserId = request.UserId,
        //            StatusId = request.StatusId,
        //            RoleId = request.RoleId,
        //            LocationId = request.LocationId
        //        };
        //        var list = await _userAppRepository.GetUserAppBySearchParams(userAppSearchParams);
        //        return new SuccessDataResult<IEnumerable<UserApp>>(list.ToList());
        //    }
        //}
    }
}
