using Business.Handlers.ADLFunctions.Commands;
using Business.Handlers.ADLFunctions.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADLFunctionsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public ADLFunctionsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List ADL Function.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getAllADLFunction")]
        public async Task<IActionResult> GetAllADLFunctionList([FromQuery] GetAllADLFunctionQuery query)
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
                _auditLogRepository.WriteLog("ADLFunctionsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add ADL Function.
        /// </summary>
        /// <param name="createADLFunction"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createADLFunction")]
        public async Task<IActionResult> Add([FromBody] CreateADLFunctionCommand createADLFunction)
        {
            try
            {
                var result = await Mediator.Send(createADLFunction);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ADLFunctionsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add/Update ADL Function.
        /// </summary>
        /// <param name="upsertADLFunction"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("upsertADLFunction")]
        public async Task<IActionResult> Update([FromBody] UpsertADLFunctionCommand upsertADLFunction)
        {
            try
            {
                var result = await Mediator.Send(upsertADLFunction);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("ADLFunctions : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
