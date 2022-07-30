using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract.IAuditLogRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public UserGroupsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List UserGroup
        /// </summary>
        /// <remarks>bla bla bla UserGroups</remarks>
        /// <return>Kullanıcı Grup List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            try { var result = await Mediator.Send(new GetUserGroupsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyuserid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try { var result = await Mediator.Send(new GetUserGroupLookupQuery { UserId = userId });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getusergroupbyuserid")]
        public async Task<IActionResult> GetGroupClaimsByUserId(int id)
        {
            try { var result = await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getusersingroupbygroupid")]
        public async Task<IActionResult> GetUsersInGroupByGroupid(int id)
        {
            try { var result = await Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery { GroupId = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add UserGroup.
        /// </summary>
        /// <param name="createUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserGroupCommand createUserGroup)
        {
            try { var result = await Mediator.Send(createUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update UserGroup.
        /// </summary>
        /// <param name="updateUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserGroupCommand updateUserGroup)
        {
            try { var result = await Mediator.Send(updateUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update UserGroup by Id.
        /// </summary>
        /// <param name="updateUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatebygroupid")]
        public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdCommand updateUserGroup)
        {
            try { var result = await Mediator.Send(updateUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete UserGroup.
        /// </summary>
        /// <param name="deleteUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserGroupCommand deleteUserGroup)
        {
            try { var result = await Mediator.Send(deleteUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("UserGroupsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}