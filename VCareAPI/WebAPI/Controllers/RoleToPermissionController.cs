using Business.Handlers.RoleToPermissions.Commands;
using Business.Handlers.RoleToPermissions.Queries;
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
    public class RoleToPermissionController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public RoleToPermissionController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getallRoleToPermission")]
        public async Task<IActionResult> GetList([FromQuery] GetRoleToPermissionListQuery query)
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
                _auditLogRepository.WriteLog("RoleToPermissionController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUserToPermissionList")]
        public async Task<IActionResult> GetUserToPermissionList([FromQuery] GetUserToPermissionListQuery query)
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
                _auditLogRepository.WriteLog("RoleToPermissionController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("AddMultipleRoleToPermissions")]
        public async Task<IActionResult> Add([FromBody] CreateRoleToPermissionCommand createRoles)
        {
            try
            {
                var result = await Mediator.Send(createRoles);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("RoleToPermissionController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
