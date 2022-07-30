using Business.Handlers.GetLocationWorkConfigsWorkConfig.Queries;
using Business.Handlers.LocationWorkConfig.Commands;
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
    public class LocationWorkConfigsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public LocationWorkConfigsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getLocationWorkConfigsByLocationId")]
        public async Task<IActionResult> GetList(int locationId)
        {
            try
            {
                var result = await Mediator.Send(new GetLocationWorkConfigsListQuery { LocationId = locationId });
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("LocationWorkConfigsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addUpdateLocationWorkConfigs")]
        public async Task<IActionResult> Add([FromBody] CreateLocationWorkConfigsCommand createLocationWorkConfigs)
        {
            try
            {
                var result = await Mediator.Send(createLocationWorkConfigs);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("LocationWorkConfigsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
