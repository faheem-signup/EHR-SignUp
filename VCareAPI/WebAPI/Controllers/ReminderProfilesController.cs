using Business.Handlers.ReminderProfiles.Commands;
using Business.Handlers.ReminderProfiles.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete.ReminderProfileEntity;
using Entities.Dtos.ReminderProfileDto;
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
    public class ReminderProfilesController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public ReminderProfilesController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List Reminder Profile.
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetList([FromQuery] GetReminderProfileQuery query)
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
                _auditLogRepository.WriteLog("ReminderProfilesController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Reminder Profile</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReminderProfileByIdDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetReminderProfileByIdQuery query)
        {
            try { var result = await Mediator.Send(query);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderProfilesController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Reminder Profile.
        /// </summary>
        /// <param name="createReminderProfile"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createReminderProfile")]
        public async Task<IActionResult> Add([FromBody] CreateReminderProfileCommand createReminderProfile)
        {
            try { var result = await Mediator.Send(createReminderProfile);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderProfilesController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Reminder Profile.
        /// <param name="updateReminderProfile"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateReminderProfile")]
        public async Task<IActionResult> Update([FromBody] UpdateReminderProfileCommand updateReminderProfile)
        {
            try { var result = await Mediator.Send(updateReminderProfile);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderProfilesController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Reminder Profile.
        /// </summary>
        /// <param name="deleteReminderProfile"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteReminderProfile")]
        public async Task<IActionResult> Delete([FromQuery] DeleteReminderProfileCommand deleteReminderProfile)
        {
            try { var result = await Mediator.Send(deleteReminderProfile);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ReminderProfilesController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
