using Business.Handlers.Patients.Queries;
using Business.Handlers.PatientsDispensing.Commands;
using Business.Handlers.PatientsDispensing.Queries;
using Business.Handlers.PatientVital.Commands;
using Business.Handlers.PatientVital.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete.PatientDispensingEntity;
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
    public class PatientDispensingController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public PatientDispensingController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List PatientDispensing.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetPatientDispensingList([FromQuery] GetPatientDispensingListQuery query)
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
                _auditLogRepository.WriteLog("PatientDispensingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// <return>Patient Vital List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDispensing))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetPatientDispensingByIdQuery query)
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
                _auditLogRepository.WriteLog("PatientDispensingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add PatientDispensing.
        /// </summary>
        /// <param name="createPatientDispensing"></param>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientDispensing")]
        public async Task<IActionResult> Add([FromBody] CreatePatientDispensingCommand createPatientDispensing)
        {
            try { var result = await Mediator.Send(createPatientDispensing);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDispensingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatePatientDispensing")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDispensingCommand updatePatientDispensing)
        {
            try { var result = await Mediator.Send(updatePatientDispensing);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDispensingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
