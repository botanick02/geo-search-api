using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoSearchController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<string>> SearchCityName(string cityName)
        {
            return new List<string> { "Kyiv!!!" };
        }
    }
}
