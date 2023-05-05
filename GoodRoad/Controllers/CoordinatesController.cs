using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatesController : Controller
    {
        private readonly ICoordinatesRepository _coordinatesRepository;
        public CoordinatesController(ICoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coordinates>))]
        public IActionResult GetCoordinates()
        {
            var coordinates = _coordinatesRepository.GetCoordinates();
            if(ModelState.IsValid)
            {
                return Ok(coordinates);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Coordinates))]
        public IActionResult GetCoordinate(int id)
        {
            var coordinates = _coordinatesRepository.GetCoordinate(id);
            if(ModelState.IsValid)
            {
                return Ok(coordinates);
            }
            return BadRequest();
        }
    }
}
