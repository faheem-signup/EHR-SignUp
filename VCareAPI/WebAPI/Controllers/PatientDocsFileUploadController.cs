using Business.Handlers.PatientDocsFileUploads.Commands;
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
    public class PatientDocsFileUploadController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public PatientDocsFileUploadController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("uploadPatientDocsFile")]
        public async Task<IActionResult> Add([FromBody] CreatePatientDocsFileUploadCommand uploadPatientDocsFile)
        {
            try
            {
                var result = await Mediator.Send(uploadPatientDocsFile);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocsFileUploadController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteUploadFile")]
        public async Task<IActionResult> Delete([FromQuery] DeletePatientDocsFileUploadCommand deletePatientDocsFileUpload)
        {
            try
            {
                var result = await Mediator.Send(deletePatientDocsFileUpload);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDocsFileUploadController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }


}
