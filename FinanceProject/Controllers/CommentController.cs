using FinanceProject.DTO.Comment;
using FinanceProject.Extensions;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [ActionName(nameof(GetCommentByIdAsync))]
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

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateCommentAsync([FromRoute] int stockId, [FromBody] CreateCommentDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var username = User.GetUsername();
                var comment = await _commentRepo.CreateCommentAsync(stockId, data, username);
                
                return CreatedAtAction("GetCommentByIdAsync", new { id = comment.Id }, comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id, [FromBody] UpdateCommentDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var comment = await _commentRepo.UpdateCommentAsync(id, data);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
        {
            try
            {
                await _commentRepo.DeleteCommentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
