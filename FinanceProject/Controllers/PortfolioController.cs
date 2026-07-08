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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStockToPortfolio(string companyName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetStockByCompanyNameAsync(companyName);

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser!);
            if (userPortfolio.Any(cn => cn.CompanyName == companyName)) return BadRequest("Same Stock can not be added");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser!.Id,
                StockId = stock.Id
            };

            var addedPortfolio = await _portfolioRepo.AddStockToPortfolio(portfolioModel);
            return addedPortfolio != null ? Created() : BadRequest("Failed to add stock to portfolio");
        }
    }
}
