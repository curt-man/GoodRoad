using GoodRoad.Data.Repository.IRepository;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetComments()
        {
            var comments = _commentRepository.GetComments();
            if(ModelState.IsValid)
            {
                return Ok(comments);
            }
            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Comment))]
        public IActionResult GetComment(int id)
        {
            var comment = _commentRepository.GetComment(id);
            if(ModelState.IsValid)
            {
                return Ok(comment);
            }
            return BadRequest();
        }
    }
}
