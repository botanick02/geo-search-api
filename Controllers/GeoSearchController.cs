using GeoSearchApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoSearchController : ControllerBase
    {
        private LocationsRepository locationRepostiory;
        public GeoSearchController(LocationsRepository locRepo)
        {
            locationRepostiory = locRepo;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<string>> SearchCityName(string cityName)
        {
            return locationRepostiory.FindByCity(cityName).Select(city => city.City).ToList();
        }
    }
}
