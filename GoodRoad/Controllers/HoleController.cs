using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleController : Controller
    {
        private readonly IHoleRepository _holeRepository;
        public HoleController(IHoleRepository holeRepository)
        {
            _holeRepository = holeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetComments()
        {
            var holes = _holeRepository.GetHoles();
            if(ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Hole))]
        public IActionResult GetHole(int id)
        {
            var hole = _holeRepository.GetHole(id);
            if(ModelState.IsValid)
            {
                return Ok(hole);
            }
            return BadRequest();
        }
    }
}
