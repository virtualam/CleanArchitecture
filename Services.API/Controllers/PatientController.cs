using Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.API.PaginationHelper;
using Services.RequestCommands.Patient.CreatePatient;
using Services.RequestCommands.Patient.DeletePatient;
using Services.RequestQueries.Patient.GetPatient.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpcontextaccessor;
        private readonly string _absoluteUrl;

        public PatientController(IMediator mediator, IHttpContextAccessor httpcontextaccessor)
        {
            _mediator = mediator;
            _httpcontextaccessor = httpcontextaccessor;

            var request = _httpcontextaccessor.HttpContext.Request;

            _absoluteUrl = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _mediator.Send(new DeletePatientCommand { PatientId = id });
            if (response.Errors.Any())
            {
                return BadRequest(new { Errors = response.Errors });
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPatientQuery getAllPatientQuery)
        {
            var response = await _mediator.Send(getAllPatientQuery);
            if (response.Errors.Any())
            {
                return BadRequest(new { Errors = response.Errors });
            }

            var paginationFilter = getAllPatientQuery == null ? new PaginationFilter() : PaginationFilter.Initialize(getAllPatientQuery.PageNumber, getAllPatientQuery.PageSize);

            var result = PagedResponse.CreatePaginatedResponse(_absoluteUrl, paginationFilter, response.HasData ? response.Result : null);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand createPatientCommand)
        {
            var response = await _mediator.Send(createPatientCommand);
            if (response.Errors.Any())
            {
                return BadRequest(new { Errors = response.Errors });
            }

            return Created("", null);
        }
    }
}
