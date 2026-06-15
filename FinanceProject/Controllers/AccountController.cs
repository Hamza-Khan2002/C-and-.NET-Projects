using FinanceProject.DTO.Account;
using FinanceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = new AppUser {UserName = data.UserName, Email = data.Email};

                var createdUser = await _userManager.CreateAsync(user, data.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    return (roleResult.Succeeded) ? Ok("User registered successfully.") : BadRequest(roleResult.Errors);
                }
                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
