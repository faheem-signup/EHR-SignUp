using Business.Handlers.PatientInsurances.Commands;
using Business.Handlers.PatientInsurances.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Handlers.PatientInsuranceAuthorizations.Commands;
using static Business.Handlers.PatientInsuranceAuthorizations.Commands.UpdatePatientInsuranceAuthorizationCommand;
using Business.Handlers.PatientInsuranceAuthorizations.Queries;
using DataAccess.Abstract.IAuditLogRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInsuranceController : BaseApiController
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public PatientInsuranceController(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetList([FromQuery] GetPatientInsuranceListQuery query)
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
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int patientInsuranceId)
        {
            try
            {
                var result = await Mediator.Send(new GetPatientInsuranceQuery { PatientInsuranceId = patientInsuranceId });
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createPatientInsurance")]
        public async Task<IActionResult> Add([FromBody] CreatePatientInsuranceCommand createPatientInsurance)
        {
            try
            {
                var result = await Mediator.Send(createPatientInsurance);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatePatientInsurance")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientInsuranceCommand updatePatientInsurance)
        {
            try
            {
                var result = await Mediator.Send(updatePatientInsurance);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deletePatientInsurance")]
        public async Task<IActionResult> Delete([FromQuery] DeletePatientInsuranceCommand deletePatientInsurance)
        {
            try
            {
                var result = await Mediator.Send(deletePatientInsurance);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllDeletedPatientInsurance")]
        public async Task<IActionResult> GetDeletedPatientInsuranceList([FromQuery] GetDeletedPatientInsuranceListQuery query)
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
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("switchPatientInsurance")]
        public async Task<IActionResult> SwitchPatientInsurance([FromBody] SwitchPatientInsuranceCommand updatePatientInsurance)
        {
            try
            {
                var result = await Mediator.Send(updatePatientInsurance);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("restorePatientInsurance")]
        public async Task<IActionResult> RestorePatientInsurance([FromBody] RestorePatientInsuranceCommand restorePatientInsurance)
        {
            try
            {
                var result = await Mediator.Send(restorePatientInsurance);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllPatientInsuranceListQuery")]
        public async Task<IActionResult> GetAllPatientInsuranceList([FromQuery] GetAllPatientInsuranceListQuery query)
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
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllPatientInsuranceAuthorization")]
        public async Task<IActionResult> GetPatientInsuranceAuthorization([FromQuery] GetPatientInsuranceAuthorizationListQuery queryPatientInsuranceAuthorization)
        {
            try
            {
                var result = await Mediator.Send(queryPatientInsuranceAuthorization);
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getPatientInsuranceAuthorizationById")]
        public async Task<IActionResult> GetPatientInsuranceAuthorizationById([FromQuery] GetPatientInsuranceAuthorizationQuery query)
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
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createPatientInsuranceAuthorization")]
        public async Task<IActionResult> Add([FromBody] CreatePatientInsuranceAuthorizationCommand createPatientInsuranceAuthorization)
        {
            try
            {
                var result = await Mediator.Send(createPatientInsuranceAuthorization);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updatePatientInsuranceAuthorization")]
        public async Task<IActionResult> UpdatePatientInsuranceAuthorization([FromBody] UpdatePatientInsuranceAuthorizationCommand updatePatientInsuranceAuthorization)
        {
            try
            {
                var result = await Mediator.Send(updatePatientInsuranceAuthorization);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletePatientInsuranceAuthorization")]
        public async Task<IActionResult> DeletePatientInsuranceAuthorization([FromQuery] DeletePatientInsuranceAuthorizationCommand deletePatientInsuranceAuthorization)
        {
            try
            {
                var result = await Mediator.Send(deletePatientInsuranceAuthorization);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                _auditLogRepository.WriteLog("PatientInsuranceController : " + actionName + " : " + DateTime.Now + " : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
