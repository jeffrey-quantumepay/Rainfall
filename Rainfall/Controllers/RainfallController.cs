using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Application.Models;
using Rainfall.Application.Queries;
using Rainfall.SharedLibrary.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Rainfall.Web.API.Controllers
{
    [Route("")]
    public class RainfallController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RainfallController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return</param>
        /// <returns></returns>
        [HttpGet("/rainfall/id/{stationId}/readings")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResponseError))]
        public async Task<IActionResult> Readings([FromRoute] string stationId, [FromQuery] int count = 10)
        {
            try
            {
                var response = await _mediator.Send(new GetReadingByStationIdCommand()
                {
                    StationId = stationId,
                    Count = count
                });

                if (response != null)
                    return Ok(response);

                return Ok();
            }
            catch (RainfallException rex)
            {
                switch (rex)
                {
                    case RainfallValidationException rve: return BadRequest(new ValidationResponseError("Validation Exception Error", rex.Errors));
                    default: break;
                }
            }
            catch(Exception vex)
            {
                return BadRequest(vex.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
