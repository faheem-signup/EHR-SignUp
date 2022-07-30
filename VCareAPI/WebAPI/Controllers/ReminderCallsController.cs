using Business.Handlers.ReminderCalls.Commands;
using Business.Handlers.ReminderCalls.Queries;
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
    public class ReminderCallsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public ReminderCallsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List of Reminder Calls.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetReminderCallsList([FromQuery] GetReminderCallsQuery query)
        {
            try { var result = await Mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderCallsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Reminder Call.
        /// </summary>
        /// <param name="createReminderCall"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createReminderCall")]
        public async Task<IActionResult> Add([FromBody] CreateReminderCallCommand createReminderCall)
        {
            try { var result = await Mediator.Send(createReminderCall);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderCallsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
