using Business.Handlers.PatientDiagnosisCodes.Commands;
using Business.Handlers.PatientDiagnosisCodes.Queries;
using Business.Handlers.Patients.Queries;
using Business.Handlers.PatientVital.Commands;
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
    public class PatientDiagnosisCodeController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public PatientDiagnosisCodeController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List PatientDiagnosisCode.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetPatientDiagnosisCodeList([FromQuery] GetPatientDiagnosisCodeListQuery query)
        {
            try { var result = await Mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDiagnosisCodeController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add PatientDiagnosisCode.
        /// </summary>
        /// <param name="createPatientDiagnosisCode"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientDiagnosisCode")]
        public async Task<IActionResult> Add([FromBody] CreatePatientDiagnosisCodeCommand createPatientDiagnosisCode)
        {
            try { var result = await Mediator.Send(createPatientDiagnosisCode);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDiagnosisCodeController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
