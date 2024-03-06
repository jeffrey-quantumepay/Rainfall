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
        private readonly ILogger<RainfallController> _logger;
        private readonly IMediator _mediator;

        public RainfallController(ILogger<RainfallController> logger, 
            IMediator mediator)
        {
            _logger = logger;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReadingByStationIdCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResponseError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponseError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalResponseError))]
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
            catch (RainfallException re)
            {
                switch (re)
                {
                    case RainfallValidationException rve: return base.BadRequest(new ValidationResponseError(rve.Message, rve.stationId, rve.Errors));
                    case RainfallNotFoundException rnf: return base.NotFound(new NotFoundResponseError(rnf.Message, rnf.stationId,rnf.Errors));
                    default:
                        return base.NotFound(new InternalResponseError(re.Message, re.stationId));
                        break;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Reading Exception: stationId {0}", stationId);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new InternalResponseError(stationId));
        }
    }
}
