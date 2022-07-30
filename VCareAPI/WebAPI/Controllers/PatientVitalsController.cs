using Business.Handlers.Patients.Queries;
using Business.Handlers.PatientVital.Commands;
using Business.Handlers.PatientVital.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete.PatientVitalsEntity;
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
    public class PatientVitalsController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public PatientVitalsController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List PatientVitals.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetPatientVitalsList([FromQuery] GetPatientVitalsListQuery query)
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
                _auditLogRepository.WriteLog("PatientVitalsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Patient Vital List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientVitals))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetPatientVitalByIdQuery query)
        {
            try { var result = await Mediator.Send(query);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientVitalsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add PatientVitals.
        /// </summary>
        /// <param name="createPatientVitals"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientVitals")]
        public async Task<IActionResult> Add([FromBody] CreatePatientVitalsCommand createPatientVitals)
        {
            try { var result = await Mediator.Send(createPatientVitals);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientVitalsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatePatientInsurance")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientVitalsCommand updatePatientVitals)
        {
            try { var result = await Mediator.Send(updatePatientVitals);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientVitalsController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
