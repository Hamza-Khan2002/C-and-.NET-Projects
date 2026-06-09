using FinanceProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepo) : ControllerBase
    {
        private readonly ICommentRepository _commentRepo = commentRepo;

        [HttpGet]
        public async Task<IActionResult> GetAllCommentAsync()
        {
            return Ok(await _commentRepo.GetAllCommentAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentByIdAsync([FromRoute] int id)
        {
            try
            {
                return Ok(await _commentRepo.GetCommentByIdAsync(id));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
