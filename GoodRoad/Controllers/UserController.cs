using GoodRoad.Data.Enums;
using GoodRoad.Data.Repository.IRepository;
using GoodRoad.InputModels;
using GoodRoad.Interfaces;
using GoodRoad.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPhotoService _photoService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IAppUserRepository appUserRepository, IPhotoService photoService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _appUserRepository = appUserRepository;
            _photoService = photoService;
            _signInManager = signInManager;
            _userManager = userManager;
        }



        #region GET
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUser>))]
        public IActionResult GetUsers()
        {
            var users = _appUserRepository.GetUsers();
            if (ModelState.IsValid)
            {
                return Ok(users);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AppUser))]
        public IActionResult GetUser(string id)
        {
            var user = _appUserRepository.GetUser(id);
            if (ModelState.IsValid)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpGet("{id}/likes")]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult GetHolesByUser(string id)
        {
            var holes = _appUserRepository.GetHolesByUser(id);
            if (ModelState.IsValid)
            {
                return Ok(holes);
            }
            return BadRequest();
        }
        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserInputModel userInputModel)
        {
            if (userInputModel is not null)
            {
                var user = await _userManager.FindByEmailAsync(userInputModel.Email);
                if (user is not null)
                {
                    ModelState.AddModelError("AlreadyExist", "User with similar email already exist");
                    return StatusCode(422, userInputModel);
                }
                else
                {
                    var imageUploadResult = await _photoService.AddPhotoAsync(userInputModel.Image);
                    if (imageUploadResult != null)
                    {

                        var newUser = new AppUser()
                        {
                            Name = userInputModel.Name,
                            Surname = userInputModel.Surname,
                            UserName = userInputModel.Email,
                            Email = userInputModel.Email,
                            CreatedAt = DateTime.Now,
                            ImageId = imageUploadResult.PublicId.ToString(),
                            ImageUrl = imageUploadResult.Url.ToString(),
                            BirthDay = userInputModel.BirthDay,
                        };
                        var newUserResponse = await _userManager.CreateAsync(newUser, userInputModel.Password);
                        if (newUserResponse.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                            await _signInManager.SignInAsync(newUser, false);
                        }
                        if(!_appUserRepository.Save())
                        {
                            ModelState.AddModelError("SaveProblem", "Something went wrong while saving");
                            return StatusCode(500, ModelState);

                        }
                    }
                }

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
