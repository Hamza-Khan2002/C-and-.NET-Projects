using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignUp___SignIn_Form.DTOs.Request;
using SignUp___SignIn_Form.Interfaces;

namespace SignUp___SignIn_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        private readonly IAuthService _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            try
            {
                return Ok(await _service.RegisterAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(await _service.LoginAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("user/{id:int}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] int id)
        {
            try
            {
                return Ok(await _service.GetUserByIdAsync(id));
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
