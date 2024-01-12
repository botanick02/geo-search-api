using GeoSearchApi.Models;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<LocationEntityMini>> SearchCityName(string cityName, int? resultsNumber = null)
        {
            return locationRepostiory.FindByCity(cityName, resultsNumber);
        }

        [HttpGet("getLocation/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocationEntity> GetLocation(int id)
        {
            return locationRepostiory.FindById(id);
        }
    }
}
