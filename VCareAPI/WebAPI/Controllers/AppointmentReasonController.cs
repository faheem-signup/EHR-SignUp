using Business.Handlers.AppointmentReason.Commands;
using Business.Handlers.AppointmentReason.Queries;
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
    public class AppointmentReasonController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AppointmentReasonController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getAllAppointmentReason")]
        public async Task<IActionResult> GetList([FromQuery] GetAppointmentReasonQuery query)
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
                _auditLogRepository.WriteLog("AppointmentReasonController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add AppointmentReason.
        /// </summary>
        /// <param name="createAppointmentReason"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createAppointmentReason")]
        public async Task<IActionResult> Add([FromBody] CreateAppointmentReasonCommand createAppointmentReason)
        {
            try
            {
                var result = await Mediator.Send(createAppointmentReason);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentReasonController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update AppointmentReason.
        /// <param name="updateAppointmentReason"></param>
        /// <returns></returns>
        /// </summary>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateAppointmentReason")]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentReasonCommand updateAppointmentReason)
        {
            try
            {
                var result = await Mediator.Send(updateAppointmentReason);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentReasonController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Delete AppointmentReason.
        /// </summary>
        /// <param name="AppointmentReason"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteAppointmentReason")]
        public async Task<IActionResult> Delete([FromQuery] DeleteAppointmentReasonCommand deleteAppointmentReason)
        {
            try
            {
                var result = await Mediator.Send(deleteAppointmentReason);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentReasonController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
