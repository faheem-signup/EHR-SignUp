using Business.Handlers.AppointmentCheckIn.Commands;
using Business.Handlers.AppointmentCheckIn.Queries;
using Business.Handlers.Appointments.Commands;
using Business.Handlers.Appointments.Queries;
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
    public class AppointmentsCheckInController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AppointmentsCheckInController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        [HttpGet("getAppointmentsCheckInList")]
        public async Task<IActionResult> GetList([FromQuery] GetAppointmentsCheckInListQuery query)
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
                _auditLogRepository.WriteLog("AppointmentsCheckInController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getAppointmentsCheckInById")]
        public async Task<IActionResult> GetById([FromQuery] GetAppointmentsCheckInByIdQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentsCheckInController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                _auditLogRepository.SaveChangesAsync();
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addAppointmentsCheckIn")]
        public async Task<IActionResult> Add([FromBody] CreateAppointmentsCheckInCommand createAppointment)
        {
            try
            {
                var result = await Mediator.Send(createAppointment);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentsCheckInController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
            //await _auditLogRepository.SaveChangesAsync();
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateAppointmentsCheckIn")]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentsCheckInCommand updateAppointment)
        {
            try
            {
                var result = await Mediator.Send(updateAppointment);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AppointmentsCheckInController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
