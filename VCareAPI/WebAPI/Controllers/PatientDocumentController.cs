using Business.Handlers.PatientDocuments.Commands;
using Business.Handlers.PatientDocuments.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Business.Handlers.PatientDocuments.Commands.SendEmailPatientDocumentCommand;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDocumentController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public PatientDocumentController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getAllPatientDocument")]
        public async Task<IActionResult> GetList([FromQuery] GetPatientDocumentListQuery query)
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
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getPatientDocumentById")]
        public async Task<IActionResult> GetById(int PatientDocumentId)
        {
            try
            {
                var result = await Mediator.Send(new GetPatientDocumentQuery { PatientDocumentId = PatientDocumentId });
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientDocument")]
        public async Task<IActionResult> Add([FromBody] CreatePatientDocumentCommand createPatientDocument)
        {
            try
            {
                var result = await Mediator.Send(createPatientDocument);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatePatientDocument")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDocumentCommand updatePatientDocument)
        {
            try
            {
                var result = await Mediator.Send(updatePatientDocument);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deletePatientDocument")]
        public async Task<IActionResult> Delete([FromQuery] DeletePatientDocumentCommand deletePatientDocument)
        {
            try
            {
                var result = await Mediator.Send(deletePatientDocument);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sendEmailPatientDocument")]
        public async Task<IActionResult> Add([FromBody] SendEmailPatientDocumentCommand sendEmailPatientDocumentCommand)
        {
            try
            {
                var result = await Mediator.Send(sendEmailPatientDocumentCommand);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocumentController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
