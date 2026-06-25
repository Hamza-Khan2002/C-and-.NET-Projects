using FinanceProject.DTO.Account;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == data.Username);
                if (user == null) return Unauthorized("Ivalid Username or Password");

                var password = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);
                
                return (password.Succeeded) ? Ok(new NewUser { Username = user.UserName!, Email = user.Email!, Token = _tokenService.CreateToken(user)}):Unauthorized("Ivalid Username or Password");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                    return (roleResult.Succeeded) ? Ok(new NewUser { Username = user.UserName, Email = user.Email, Token = _tokenService.CreateToken(user)}) : BadRequest(roleResult.Errors);
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
