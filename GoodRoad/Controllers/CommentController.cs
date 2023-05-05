using GoodRoad.Data.Repository;
using GoodRoad.Data.Repository.IRepository;
using GoodRoad.InputModels;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodRoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        #region Get
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetComments()
        {
            var comments = _commentRepository.GetComments();
            if (ModelState.IsValid)
            {
                return Ok(comments);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        public IActionResult GetComment(int id)
        {
            var comment = _commentRepository.GetComment(id);
            if (ModelState.IsValid)
            {
                return Ok(comment);
            }
            return BadRequest();
        }

        [HttpGet("hole/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetCommentsByHole(int id)
        {
            var comments = _commentRepository.GetCommentsByHole(id);
            if (ModelState.IsValid)
            {
                return Ok(comments);
            }
            return BadRequest();
        }

        #endregion


        #region Post

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            if (comment is not null)
            {
                comment.CreatedAt = DateTime.Now;
                //comment.Owner = 

                if (_commentRepository.CreateComment(comment))
                {
                    return Ok("Successfully created");

                }
                ModelState.AddModelError("SaveProblem", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return BadRequest(ModelState);
        }

        #endregion
    }
}
