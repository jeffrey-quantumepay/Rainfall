using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rainfall.Web.API.Controllers
{
    [Route("")]
    public class RainfallController : ControllerBase
    {

        public RainfallController()
        {
        }

        [HttpGet("/rainfall/id/{stationId}/readings")]
        public async Task<IActionResult> Readings([FromRoute] string stationId, [FromQuery] string count)
        {
            return  Ok(new { stationId, count });

        }
    }
}
