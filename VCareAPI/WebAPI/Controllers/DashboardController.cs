using Business.Handlers.Dashboard.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Dtos.DashboardDto;
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
    public class DashboardController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public DashboardController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// Admin Appointments Pie Chart Dashboard.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("getAdminAppointmentsPieChartDashboard")]
        public async Task<IActionResult> GetAdminAppointmentsPieChart([FromQuery] GetAdminAppointmentsPieChartQuery query)
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
                _auditLogRepository.WriteLog("DashboardController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Admin Appointments Graph Chart Dashboard.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("getGraphChart")]
        public async Task<IActionResult> GetAdminAppointmentsGraphChart()
        {
            try
            {
                var result = await Mediator.Send(new GetAdminAppointmentsGraphChartQuery());
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("DashboardController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Provider Appointments Graph Chart Dashboard.
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("getProviderAppointmentsGraphChartDashboard")]
        public async Task<IActionResult> GetProviderAppointmentsGraphChart([FromQuery] GetProviderAppointmentsGraphChartQuery query)
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
                _auditLogRepository.WriteLog("DashboardController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
