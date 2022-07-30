using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using Business.Handlers.Login.Queries;
using Business.Handlers.Users.Commands;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Dtos.UesrAppDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Make it Authorization operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IAuditLogRepository _auditLogRepository;

        /// <summary>
        /// Dependency injection is provided by constructor injection.
        /// </summary>
        /// <param name="configuration"></param>
        public AuthController(IConfiguration configuration, IAuditLogRepository auditLogRepository)
        {
            _configuration = configuration;
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// Make it User Login operations
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
        {
            try
            {
                var result = await Mediator.Send(loginModel);

                if (result.Success)
                {
                    return Ok(result);
                }

                return Unauthorized(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<UserAppPermissionDto>))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost("Userlogin")]
        public async Task<IActionResult> userLogin([FromBody] LoginQuery loginModel)
        {
            try
            {
                var result = await Mediator.Send(loginModel);

                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return Unauthorized(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<UserAppPermissionDto>))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost("getLogedinUserUpdatedData")]
        public async Task<IActionResult> GetLogedinUserUpdatedData([FromQuery] GetUserUpdatedDataQuery loginModel)
        {
            try
            {
                var result = await Mediator.Send(loginModel);

                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return Unauthorized(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Make it User Register operations
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand createUser)
        {
            try
            {
                var result = await Mediator.Send(createUser);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Make it Forgot Password operations
        /// </summary>
        /// <remarks>tckimlikno</remarks>
        /// <return></return>
        /// <response code="200"></response>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPut("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword)
        {
            try
            {
                var result = await Mediator.Send(forgotPassword);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Make it Change Password operation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("changeuserpassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);

                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Mobile Login
        /// </summary>
        /// <param name="verifyCid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("verify")]
        public async Task<IActionResult> Verification([FromBody] VerifyCidQuery verifyCid)
        {
            try
            {
                var result = await Mediator.Send(verifyCid);

                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Token decode test
        /// </summary>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPost("test")]
        public IActionResult LoginTest()
        {
            try
            {
                var auth = Request.Headers["Authorization"];
                var token = new JwtHelper(_configuration).DecodeToken(auth);

                return Ok(token);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("AuthController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}