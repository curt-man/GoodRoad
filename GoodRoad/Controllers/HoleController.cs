using GoodRoad.Data.Repository.IRepository;
using GoodRoad.InputModels;
using GoodRoad.Interfaces;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleController : Controller
    {
        private readonly IHoleRepository _holeRepository;
        private readonly IPhotoService _photoService;
        public HoleController(IHoleRepository holeRepository, IPhotoService photoService)
        {
            _holeRepository = holeRepository;
            _photoService = photoService;
        }


        #region GET
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetHoles()
        {
            var holes = _holeRepository.GetHoles();
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }
        [HttpGet("state/{state}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetHolesByState(string state)
        {
            var holes = _holeRepository.GetHolesByState(state);
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }
        [HttpGet("city/{city}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetHolesByCity(string city)
        {
            var holes = _holeRepository.GetHolesByCity(city);
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }
        [HttpGet("street/{street}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetHolesByStreet(string street)
        {
            var holes = _holeRepository.GetHolesByStreet(street);
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }

        [HttpGet("user/{user}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hole>))]
        public IActionResult GetHolesByUser(string user)
        {
            var holes = _holeRepository.GetHolesByUser(user);
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Hole))]
        public IActionResult GetHole(int id)
        {
            var hole = _holeRepository.GetHole(id);
            if (ModelState.IsValid)
            {
                return Ok(hole);
            }
            return BadRequest();
        }

        [HttpGet("{id}/likes")]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult GetHoleLikes(int id)
        {
            var holeLikes = _holeRepository.GetHoleLikes(id);
            if (ModelState.IsValid)
            {
                return Ok(holeLikes);
            }
            return BadRequest();
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateHole([FromBody] HoleInputModel holeInputModel)
        {
            if (holeInputModel is not null)
            {
                string problem = string.Empty;
                if (_holeRepository.GetHoles().Any(x => x.Name == holeInputModel.Name))
                {
                    problem = "Hole with similar name already exist";
                }
                if (_holeRepository.GetHoles().Any(x => x.Coordinates.Latitude == holeInputModel.Coordinates.Latitude &&
                                              x.Coordinates.Longitude == holeInputModel.Coordinates.Longitude))
                {
                    problem = "Hole with similar coordinates already exist";
                }
                //if (holeInputModel.Image == hole2.Image)
                //{
                //    problem = "Holes have similar images";
                //}

                if (problem.Length == 0)
                {
                    var imageUploadResult = await _photoService.AddPhotoAsync(holeInputModel.Image);
                    if (imageUploadResult != null)
                    {

                        var hole = new Hole()
                        {
                            Name = holeInputModel.Name,
                            Description = holeInputModel.Description,
                            Size = holeInputModel.Size,
                            Address = holeInputModel.Address,
                            Contributor = holeInputModel.Contributor,
                            Coordinates = holeInputModel.Coordinates,
                            CreatedAt = DateTime.Now,
                            ImageUrl = imageUploadResult.Url.ToString(),
                            ImageId = imageUploadResult.PublicId.ToString(),
                        };

                        if (_holeRepository.CreateHole(hole))
                        {
                            return Ok("Successfully created");

                        }
                    }
                    ModelState.AddModelError("SaveProblem", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                ModelState.AddModelError("AlreadyExist", problem);
                return StatusCode(422, holeInputModel);

            }
            return BadRequest(ModelState);
        }

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> UpdateHole([FromBody] HoleInputModel holeInputModel)
        //{
        //    if (holeInputModel is not null)
        //    {
        //        string problem = string.Empty;
        //        if (_holeRepository.GetHoles().Any(x => (x.Name == holeInputModel.Name && x.Id != holeInputModel.Id)))
        //        {
        //            problem = "Hole with similar name already exist";
        //        }
        //        if (_holeRepository.GetHoles().Any(x => x.Coordinates.Latitude == holeInputModel.Coordinates.Latitude &&
        //                                      x.Coordinates.Longitude == holeInputModel.Coordinates.Longitude && x.Id != holeInputModel.Id))
        //        {
        //            problem = "Hole with similar coordinates already exist";
        //        }
        //        //if (holeInputModel.Image == hole2.Image)
        //        //{
        //        //    problem = "Holes have similar images";
        //        //}

        //        if (problem.Length == 0)
        //        {
        //            var hole = _holeRepository.GetHole(holeInputModel.Id);
        //            if (holeInputModel.Image is not null)
        //            {
        //                var imageUploadResult = await _photoService.AddPhotoAsync(holeInputModel.Image);
        //                if (imageUploadResult.Error is not null)
        //                {
        //                    ModelState.AddModelError("Image", "Photo upload failed");
        //                    return StatusCode(500, ModelState);
        //                }

        //                _ = _photoService.DeletePhotoAsync(hole.ImageId);

        //                hole.ImageUrl = imageUploadResult.Url.ToString();
        //                hole.ImageId = imageUploadResult.PublicId.ToString();
        //            }


        //            hole.Name = holeInputModel.Name;
        //            hole.Description = holeInputModel.Description;
        //            hole.Size = holeInputModel.Size;
        //            hole.Address = holeInputModel.Address;
        //            hole.Contributor = holeInputModel.Contributor;
        //            hole.Coordinates = holeInputModel.Coordinates;


        //            if (_holeRepository.UpdateHole(hole))
        //            {
        //                return Ok("Successfully updated");

        //            }
        //            ModelState.AddModelError("SaveProblem", "Something went wrong while saving");
        //            return StatusCode(500, ModelState);
        //        }

        //        ModelState.AddModelError("AlreadyExist", problem);
        //        return StatusCode(422, holeInputModel);

        //    }
        //    return BadRequest(ModelState);
        //}


        #endregion


    }
}
