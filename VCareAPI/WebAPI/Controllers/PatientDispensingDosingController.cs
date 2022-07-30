using Business.Handlers.Patients.Queries;
using Business.Handlers.PatientsDispensing.Commands;
using Business.Handlers.PatientsDispensing.Queries;
using Business.Handlers.PatientsDispensingDosing.Commands;
using Business.Handlers.PatientsDispensingDosing.Queries;
using Business.Handlers.PatientVital.Commands;
using Business.Handlers.PatientVital.Queries;
using DataAccess.Abstract.IAuditLogRepository;
using Entities.Concrete.PatientDispensingDosingEntity;
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
    public class PatientDispensingDosingController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public PatientDispensingDosingController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// List PatientDispensingDosing.
        /// <summary>
        /// <response code="200"></response>
        [HttpGet("getall")]
        public async Task<IActionResult> GetPatientDispensingDosingList([FromQuery] GetPatientDispensingDosingListQuery query)
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
                _auditLogRepository.WriteLog("PatientDispensingDosingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// </summary>
        /// <return>Patient Vital List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDispensingDosing))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetPatientDispensingDosingByIdQuery query)
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
                _auditLogRepository.WriteLog("PatientDispensingDosingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add PatientDispensingDosing.
        /// </summary>
        /// <param name="createPatientDispensingDosing"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientDispensingDosing")]
        public async Task<IActionResult> Add([FromBody] CreatePatientDispensingDosingCommand createPatientDispensingDosing)
        {
            try
            {
                var result = await Mediator.Send(createPatientDispensingDosing);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDispensingDosingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatePatientDispensingDosing")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDispensingDosingCommand updatePatientDispensingDosing)
        {
            try
            {
                var result = await Mediator.Send(updatePatientDispensingDosing);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientDispensingDosingController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
