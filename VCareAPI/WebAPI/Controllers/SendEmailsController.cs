using Business.Handlers.SendEmails.Commands;
using Business.Handlers.SendEmails.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public SendEmailsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List of Messages.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetMessagesList([FromQuery] GetMessagesQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SendEmailsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Email.
        /// </summary>
        /// <param name="createSendEmail"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("saveEmail")]
        public async Task<IActionResult> Add([FromBody] CreateSendEmailCommand createSendEmail)
        {
            try
            {
                var result = await Mediator.Send(createSendEmail);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SendEmailsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
