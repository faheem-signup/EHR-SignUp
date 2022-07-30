using Business.Handlers.Schedulers.Commands;
using Business.Handlers.Schedulers.Queries;
using Core.Utilities.Results;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Concrete.SchedulerEntity;
using Entities.Dtos.SchedulerDto;
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
    public class SchedulersController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public SchedulersController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List Scheduler.
        /// </summary>
        /// <return>Scheduler List.</return>
        /// <response code="200"></response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetSchedulerList()
        {
            try
            {
                var result = await Mediator.Send(new GetSchedulersQuery());
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Scheduler</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentScheduler))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int schedulerId)
        {
            try
            {
                var result = await Mediator.Send(new GetSchedulerByIdQuery { SchedulerId = schedulerId });
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Scheduler.
        /// <param name="createScheduler"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<object>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createScheduler")]
        public async Task<IActionResult> Add([FromBody] CreateSchedulerCommand createScheduler)
        {
            try
            {
                var result = await Mediator.Send(createScheduler);
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Scheduler.
        /// <param name="updateScheduler"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateScheduler")]
        public async Task<IActionResult> Update([FromBody] UpdateSchedulerCommand updateScheduler)
        {
            try
            {
                var result = await Mediator.Send(updateScheduler);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Provider Status Summary.
        /// </summary>
        /// <return>Provider Status Summary</return>
        /// <response code="200"></response>
        [HttpGet("getProviderStatusSummary")]
        public async Task<IActionResult> GetProviderStatusSummary([FromQuery] GetProviderStatusSummaryQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>GetProviderStatusSummary</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderStatusSummaryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getProviderStatusSummaryById")]
        public async Task<IActionResult> GetProviderStatusSummaryById([FromQuery] GetProviderStatusSummaryByIdQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Appointment Status Summary.
        /// </summary>
        /// <return>Appointment Status Summary</return>
        /// <response code="200"></response>
        [HttpGet("getAppointmentStatussSummaryDetail")]
        public async Task<IActionResult> GetAppointmentStatusSummaryDetail([FromQuery] GetAppointmentStatusDetailQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Provider Appointments Dashboard.</return>
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProviderStatusSummaryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getProviderAppointmentsDashboard")]
        public async Task<IActionResult> GetProviderAppointmentsDashboard([FromQuery] GetProviderAppointmentsDashboardQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// List Checkin Appointment.
        /// </summary>
        /// <return>Checkin Appointment List.</return>
        /// <response code="200"></response>
        [HttpGet("getProviderCheckinAppointment")]
        public async Task<IActionResult> GetCheckinAppointment([FromQuery] GetAppointmentCheckinStatusQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// List Scheduler Status.
        /// </summary>
        /// <return>Scheduler Status List.</return>
        /// <response code="200"></response>
        [HttpGet("getAllSchedulerStatus")]
        public async Task<IActionResult> GetSchedulerStatusList()
        {
            try
            {
                var result = await Mediator.Send(new GetSchedulerStatusQuery());
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// List Scheduler Status.
        /// </summary>
        /// <return>Scheduler Status List.</return>
        /// <response code="200"></response>
        [HttpGet("getAllProviderTypes")]
        public async Task<IActionResult> GetAllProviderTypesList([FromQuery] GetAllProviderTypesQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Slots</return>
        /// <response code="200"></response>
        [HttpGet("GenerateSchedule")]
        public async Task<IActionResult> GetProviderWorkConfigById([FromQuery] GetProviderWorkCnfigQueryByDay query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Scheduler List By Status.</return>
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("getSchedulerListByAppointmentStatus")]
        public async Task<IActionResult> GetSchedulerListByAppointmentStatus([FromQuery] GetSchedulerListByAppointmentStatusQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetScheduleLocationLookup")]
        public async Task<IActionResult> GetScheduleLocationList([FromQuery] GetScheduleLocationListQuery query)
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
                _auditLogRepository.WriteLog("SchedulersController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
