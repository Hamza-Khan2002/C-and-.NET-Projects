using FinanceProject.DTO.Stock;
using FinanceProject.Models;

namespace FinanceProject.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetUserPortfolio(AppUser user);
        Task<Portfolio> AddStockToPortfolio(Portfolio portfolio);
    }
}
