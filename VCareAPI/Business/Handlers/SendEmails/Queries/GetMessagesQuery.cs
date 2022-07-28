using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISendEmailRepository;
using Entities.Dtos.SendEmailDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.SendEmails.Queries
{
    public class GetMessagesQuery : IRequest<IDataResult<IEnumerable<SendEmailDto>>>
    {
        public int EmailStatusId { get; set; }
        public int MessageStatusId { get; set; }
        public int UserId { get; set; }

        public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IDataResult<IEnumerable<SendEmailDto>>>
        {
            private readonly ISendEmailRepository _sendEmailRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public GetMessagesQueryHandler(ISendEmailRepository sendEmailRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _sendEmailRepository = sendEmailRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SendEmailDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
            {
                var list = await _sendEmailRepository.GetMessagesList(request.EmailStatusId, request.MessageStatusId, request.UserId);

                return new SuccessDataResult<IEnumerable<SendEmailDto>>(list);
            }
        }
    }
}
