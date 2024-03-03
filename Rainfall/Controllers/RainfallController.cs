using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rainfall.Application.Queries;
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
            catch (ValidationException vex)
            {
                return BadRequest(vex.Data);
            }
            catch(Exception vex)
            {
                return BadRequest(vex.Data);
            }

        }
    }
}
