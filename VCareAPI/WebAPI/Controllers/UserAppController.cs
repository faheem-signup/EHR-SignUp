using Business.Handlers.UserApps.Commands;
using Business.Handlers.UserApps.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete;
using Entities.Concrete.User;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    public class UserAppController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public UserAppController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetList([FromQuery] GetUserAppListQuery query)
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
                _auditLogRepository.WriteLog("UserAppController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetUserAppQuery query)
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
                _auditLogRepository.WriteLog("UserAppController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createUser")]
        public async Task<IActionResult> Add([FromBody] CreateUserAppCommand createUser)
        {
            try
            {
                var result = await Mediator.Send(createUser);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserAppController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateUser")]
        public async Task<IActionResult> Update([FromBody] UpdateUserAppCommand updateUser)
        {
            try
            {
                var result = await Mediator.Send(updateUser);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserAppController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserAppCommand deleteUser)
        {
            try
            {
                var result = await Mediator.Send(deleteUser);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserAppController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        //[Produces("application/json", "text/plain")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        //[HttpPost("GetUserBySearchParams")]
        //public async Task<IActionResult> GetUserBySearchParams(UserAppSearchParams userAppSearchParams)
        //{
        //    try { var result = await Mediator.Send(new GetUserAppBySearchParamsQuery
        //    {
        //        Name = userAppSearchParams.Name,
        //        UserId = userAppSearchParams.UserId,
        //        RoleId = userAppSearchParams.RoleId,
        //        StatusId = userAppSearchParams.StatusId,
        //        LocationId = userAppSearchParams.LocationId,

        //    });
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }

        //    return BadRequest(result.Message);
        //}
    }
}
