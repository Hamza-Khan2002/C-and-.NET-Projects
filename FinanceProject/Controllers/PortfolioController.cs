using FinanceProject.Extensions;
using FinanceProject.Interfaces;
using FinanceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _stockRepo = stockRepo;
        private readonly IPortfolioRepository _portfolioRepo = portfolioRepo;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            return Ok(await _portfolioRepo.GetUserPortfolio(appUser!));
        }

        [HttpPost("{symbol:alpha}")]
        [Authorize]
        public async Task<IActionResult> AddStockToPortfolio([FromRoute]string symbol)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var username = User.GetUsername();

                var addedPortfolio = await _portfolioRepo.AddStockToPortfolio(username, symbol);
                return addedPortfolio != null ? Created() : BadRequest("Failed to add stock to portfolio");
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string companyName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepo.GetUserPortfolio(appUser!);

            var filteredPortfolio = portfolio.Where(p => p.CompanyName.ToLower() == companyName.ToLower());

            if(filteredPortfolio.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser!, companyName);
            }
            else return BadRequest("Stock not found in portfolio");

            return Ok();
        }
    }
}
