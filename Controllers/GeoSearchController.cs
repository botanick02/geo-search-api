using GeoSearchApi.Models;
using GeoSearchApi.Repositories;
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

        [HttpGet("searchLocationsByName/{city}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<LocationEntityMini>> SearchCityName(string city, int? resultsNumber = null)
        {
            return locationRepostiory.FindByCity(city, resultsNumber);
        }

        [HttpGet("getLocationById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocationEntity> GetLocation(int id)
        {
            return locationRepostiory.FindById(id);
        }

        [HttpGet("getLocationByCords/{latitude}/{longitude}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocationEntity> GetLocationByCoordinates(double latitude, double longitude)
        {
            return locationRepostiory.FindByCoordinates(latitude, longitude);
        }
    }
}
