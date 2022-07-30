using Business.Handlers.DocumentParentCategoryLookups.Commands;
using Business.Handlers.DocumentParentCategoryLookups.Queries;
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
    public class DocumentParentCategoryLookupController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public DocumentParentCategoryLookupController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getAllDocumentParentCategoryLookup")]
        public async Task<IActionResult> GetList([FromQuery] GetDocumentParentCategoryLookupListQuery query)
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
                _auditLogRepository.WriteLog("DocumentParentCategoryLookupController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getDocumentParentCategoryLookupById")]
        public async Task<IActionResult> GetById(int ParentCategoryId)
        {
            try
            {
                var result = await Mediator.Send(new GetDocumentParentCategoryLookupQuery { ParentCategoryId = ParentCategoryId });
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("DocumentParentCategoryLookupController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createDocumentParentCategoryLookup")]
        public async Task<IActionResult> Add([FromBody] CreateDocumentParentCategoryLookupCommand createDocumentParentCategoryLookup)
        {
            try
            {
                var result = await Mediator.Send(createDocumentParentCategoryLookup);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("DocumentParentCategoryLookupController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateDocumentParentCategoryLookup")]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentParentCategoryLookupCommand updateDocumentParentCategoryLookup)
        {
            try
            {
                var result = await Mediator.Send(updateDocumentParentCategoryLookup);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("DocumentParentCategoryLookupController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteDocumentParentCategoryLookup")]
        public async Task<IActionResult> Delete([FromQuery] DeleteDocumentParentCategoryLookupCommand deleteDocumentParentCategoryLookup)
        {
            try
            {
                var result = await Mediator.Send(deleteDocumentParentCategoryLookup);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("DocumentParentCategoryLookupController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
